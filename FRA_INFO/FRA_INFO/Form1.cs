/* Subject: Revised notices, Author: Jay
* [Date:6/23/2018]    
  1.Set loopMax = Math.Round(end_numFreq) to control data amount automatically.
  2.Adjusted "realFreq" format to one fixed-point in ploting and peakData.txt.
  3.DataBindY --> chart1.Series.Points.DataBindXY(x,y)
     AddY --> chart1.Series.Points.AddXY(x,y)
* [Date:7/10/2018]   
  4.Revised functions of fra_confirm_setting() & fra_confirm_exit_setting() by confirmByte(Ox80/0x00)
  5.Set input text_Amp to Byte_Amp      
  6.PeakSeach.cs changed criteria from 3 to (peakgroup < 4)    
* [Date:7/17/2018] 
  7.fra_exit_setting() & fra_confirm_ exit_setting() must to be executed at last after measure completeness
  8.Remain consistent in CSV & txt file format.
  9.Cancel fcn startPeakSearch displays data.   
* [Date:7/22/2018]
  10.Detect-IC button can response AKM7374/7371/7375/NONE.
  11.Employ class Peak_Search constructor to switch search parameters.
* [Date:7/30/2018] 
  12.Activate Amp & Offset TrackBar       
* [Date:8/13/2018]
  13.Enable users to save a result file to the specific destination 
  14.Terminate a thread work on backgroundworker1 to supress "DataTabletoExcel" & "FRA_test.xlsx"
* [Date:9/10/2018]
  15.MS Chart Logarithmic:
  15-1:AxisX.IsLogarithmic = false; Initial logarithmic setting while starting up.
  15-2:AxisX.IsLogarithmic = true; Provide chart X xalues & Series.IsXValueIndexed is set to false beforehand.
  16.fra_confirm_setting():extend the delay 30ms on Thread.sleep(30)
  17.Form1.Designer.cs:Adopt "get { }" to retrieve the private freq. settings, no "Static" variables.
* [Date:10/08/2018]
  18.As 15-1                                                                                         */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using peaklib;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO.Ports;
using System.IO;
using PeakSearch;

namespace FRA_INFO
{
    public partial class Form1 : Form
    {
        static String filePath = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
        //static String fileName = System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "FRA_test.xlsx";
        //static Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
        static DateTime dt = DateTime.Now;
        System.Data.DataTable dtExcel = new System.Data.DataTable();
        System.Data.DataTable dtCsv = new System.Data.DataTable();
        string serialfileName = filePath + dt.ToString("yyyyMMdd-HHmmss-") + strMode[0] + "-FRA.csv"; //路径+时间+close/open     
        public static Form1 form1;
        static ISheet sheet = null;
        IWorkbook workbook = null;
        Peak_Search peakSearch;

        //FRA porting--parameter start
        static int tryMax = 50;
        public static int PID = 0; //Jay[7/20]
        //int DriverType = 1;
        bool isFRA_Open = true; //Jay[6/23] set open loop mode
        bool isFRA_SingleFreq = false;
        bool isSave_Data = false;
        static string[] strMode = { "Open", "Clsoe" }; //Jay[6/23]
        //FRA porting--parameter end

        public Form1()
        {
            InitializeComponent();
            form1 = this;//子线程调用主线程solution
            //backgroundWorker1.WorkerReportsProgress = true;//声明报告进度
        }

        /// <summary>
        /// 关闭程序提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            if (DialogResult.Yes == MessageBox.Show("确定关闭程序吗？", "关闭提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2))
            {
                e.Cancel = false;
                //xls.Quit();//Excel程序退出
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Debug.WriteLine("==========Form1_Load=======");
            Control.CheckForIllegalCrossThreadCalls = false;
            form1.chart1.Visible = true;
            SetChart();
            SetDataGridView();//设置表格属性
            InitializeTrackBar();
            ClearDataTable(dtExcel);
            ClearDataTable(dtCsv);
            peakSearch = new Peak_Search(PID);
        }

        /// <summary>
        /// 点击开始button开启SerialThread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSerialsBegin_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("============btnSerialsBegin_Click ==============");
            //Initial logarithmic setting of starting up
            chart1.ChartAreas[0].AxisX.IsLogarithmic = false;
            chart1.ChartAreas[0].AxisX2.IsLogarithmic = false;

            Thread serialThread = new Thread(fra_Function);
            serialThread.Start();
            if (!serialThread.IsAlive)
            {
                Debug.WriteLine("============SerialThread 未启动 ==============");
            }
            else
            {
                Debug.WriteLine("============SerialThread 启动成功 ==============");
            }
        }

        //点击Open COM Port button Open port
        private void button1_Click(object sender, EventArgs e)
        {
            Debug.WriteLine("==========open COM port button1_Click=======");
            try
            {
                //GetSerialPort();
                fra_serial_open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "fra_seial_open ERROR");
                return;
            }
        }

        /// <summary>
        /// 获取COM Number
        /// </summary>
        public void GetSerialPort()
        {
            Debug.WriteLine("==========GetSerialPort=======");
            this.cmbSerials.Items.AddRange(SerialPort.GetPortNames());
            this.cmbSerials.SelectedIndex = this.cmbSerials.Items.Count - 1;//Arduino一般在最后一个串口
            string portName = this.cmbSerials.SelectedItem.ToString();
            if (portName == "")
            {
                this.cmbSerials.Text = "";
            }
        }

       //20180128, robert liao , test funciton code implement fra_peak class
       //move below fra_function to fra_peak class
        private void fra_serial_open()
        {
            Debug.WriteLine("==========fra_serial_open=========");
            Debug.WriteLine("fra seial open, port:" + serialPort1.PortName.ToString());
            cmbSerials.Text = serialPort1.PortName.ToString();
            try
            {
                if (!serialPort1.IsOpen)
                {
                    serialPort1.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "fra_serial_open", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// 点击开始button thread 画图 & 分析数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        int btncount = 0;
        private void btnBegin_Click(object sender, EventArgs e)
        {
            btncount++;
            if (btncount > 2)
            {
                btncount = 1;
            }
            Debug.WriteLine("=======btncount clike count=====" + btncount.ToString());
            Debug.WriteLine("=======click start button=====");
            switch (btncount)
            {
                case (1):
                    //Initial logarithmic setting of starting up
                    chart1.ChartAreas[0].AxisX.IsLogarithmic = false;
                    chart1.ChartAreas[0].AxisX2.IsLogarithmic = false;
                    chart1.Visible = true;
                    btnBeginChart.Enabled = false;
                    VSdrawChartThread();//启动VS画图Thread                      
                    break;

                case (2):
                    chart1.Visible = true;
                    btnBeginChart.Text = "数据分析中...";
                    btnBeginChart.BackColor = Color.Gray;
                    btnBeginChart.Enabled = false;
                    AnalysisDataThread();//启动数据分析Thread
                    break;

                default: /* 可选的 */
                    VSdrawChartThread();
                    break;
            }
        }

        /// <summary>
        ///VS画图线程thread
        /// </summary>
        public void VSdrawChartThread()
        {
            string filename;
            filename = filePath + "peakData.txt";
            Debug.WriteLine("=======VSdrawChartThread=====");
            try
            {
                if (File.Exists(filename))
                {
                    //string sheetName = GetFirstSheetName(fileName, 1);                  
                    ClearDataTable(dtCsv);
                }
                else
                {
                    MessageBox.Show("文件不存在", "Open File ERROR ");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Open File ERROR");
                return;
            }
            //
            try
            {
                Thread drawChartThread = new Thread(new ThreadStart(DrawChart));
                drawChartThread.Start();
                if (!drawChartThread.IsAlive)
                {
                    Debug.WriteLine("============drawChartThread 未启动 ==============");
                }
                else
                {
                    Debug.WriteLine("============drawChartThread 启动成功 ==============");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Start drawChartThread ERROR");
                return;
            }
            btnBeginChart.Enabled = true;
            btnBeginChart.Text = "分析数据";
        }

        /// <summary>
        /// 分析数据thread
        /// </summary>
        public void AnalysisDataThread()
        {
            Debug.WriteLine("=======analysisDataThread=====");
            try
            {
                Thread analysisDataThread = new Thread(new ThreadStart(AnalysisData));
                analysisDataThread.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Start analysisDataThread ERROR");
                return;
            }
            btnBeginChart.Enabled = true;
        }

        public void AnalysisData()
        {
            string filename;
            filename = filePath + "peakData.txt";
            PeakAnalysis(filename);
        }

        /// <summary>
        /// 分析peak value
        /// </summary>
        /// 
        public void PeakAnalysis(string filename)
        {
            try
            {
                List<List<float>> Plist; //2D list 

                Plist = peakSearch.startPeakSearch(filename);
                //SetDataGridView();
                for (int i =0; i < 6; i++)
                {
                    dataGridView1.Rows[i].Cells[3].Value = null;
                }
                Console.WriteLine("PeakSearch SUMMARY");
                Console.WriteLine("Pkeak # Freq(Hz) Gain(dB) Phase(dg)");
                string ss = "PeakSearch SUMMARY\n";
                ss += "Pkeak # Freq(Hz) Gain(dB) Phase(dg)\n";
                for (int k = 0; k < Plist.Count; k++)
                {
                    // Fit the List data to dataGridView
                    dataGridView1.Rows[2*k].Cells[3].Value = Plist[k][1];
                    dataGridView1.Rows[2*k + 1].Cells[3].Value = Plist[k][2];
                    //
                    for (int j = 0; j < 4; j++)
                    {
                        Console.Write(Plist[k][j] + "      ");
                        ss += Plist[k][j].ToString() + "          ";
                    }
                    ss += "\n";
                    Console.Write('\n');
                }
                MessageBox.Show(ss, "分析结果");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "nalysisData Function ERROR");
                return;
            }
            btnBeginChart.Text = "画图";
            btnBeginChart.BackColor = Color.Aquamarine;
        }

        /// <summary>
        ///  点击Save data checkBox 从csv读取数据到datatable Thread
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("=======checkBox1_CheckedChanged=====");
            ClearDataTable(dtCsv);
            if (chkSavedata.Checked == true)
            {               
                isSave_Data = true;
                serialfileName = txtSaveData.Text; // Jay[8/13] overwite default path & filename
                Thread ReadDataFromDTthread = new Thread(ReadDataFromDatatable);//saveDataToCSV
                ReadDataFromDTthread.Start();
                
                if (!ReadDataFromDTthread.IsAlive)
                {
                    Debug.WriteLine("============ReadDataFromDTthread 未启动 ==============");
                }
                else
                {
                    Debug.WriteLine("============ReadDataFromDTthread 启动成功 ==============");
                }
            }
            else
            {
                //progressBar.Value = 0;
                isSave_Data = false;
                txtSaveData.Text = "";
            }
            Debug.WriteLine(" isSave_Data=" + isSave_Data);
        }

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    Debug.WriteLine(" backgroundWorker1_DoWork" );
        //    try
        //    {
        //        DataTabletoExcel(dtCsv, fileName);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString(), "backgroundWorker1_DoWork ");
        //        return;
        //    }           
        //}

        //private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    Debug.WriteLine(" backgroundWorker1_ProgressChanged");
        //    this.progressBar.Value = e.ProgressPercentage;
        //}

        //private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    Thread.Sleep(50);
        //    Debug.WriteLine(" backgroundWorker1_RunWorkerCompleted");
        //    MessageBox.Show("保存成功", "backgroundWorker提示");
        //}

        /// <summary>
        ///清除table data
        /// </summary>
        public void ClearDataTable(System.Data.DataTable dataTableName)
        {
            Debug.WriteLine("======ClearDataTable======");
            dataTableName.Rows.Clear();
            dataTableName.Columns.Clear();
            dataTableName.Clear();
        }

        /// <summary>
        /// 数据保存转换serial port data--->csv--->datatable-->excel
        /// </summary>
        public void ReadDataFromDatatable()
        {

            Debug.WriteLine("==========ReadDataFromDatatable=======");
            ClearDataTable(dtCsv);//清空datatable
            try
            {
                //string serialData = "120,16.1,-2";
                string serialData = ""; // Jay[8/13]
                SaveSerialPortData(serialfileName, serialData);//保存serial port data to csv
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SaveSerialPortData ERROR");
                return;
            }
        }

        /// <summary>
        /// StreamWriter类写入serial port data to csv file
        /// </summary>
        public void SaveSerialPortData(string csvPath, string serialData)
        {
            Debug.WriteLine("==========SaveSerialPortData=======");
            try
            {
                if (!File.Exists(csvPath))
                {
                    //File.Create(csvPath); // Jay[8/13]
                    FileInfo finfo = new FileInfo(csvPath);
                    string path = finfo.DirectoryName; // get directory path
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                }
                //serialData = "10,16.1,-2";
                if (serialData != null)
                {
                    try
                    {
                        FileStream fs = new FileStream(csvPath, FileMode.Create, FileAccess.ReadWrite);//flieShare方式为ReadWrite。因为随时有其他程序对其进行写操作
                        StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                        sw.WriteLine(serialData);
                        sw.Flush();
                        sw.Close();
                        fs.Close();
                        //DialogResult result = MessageBox.Show("CSV文件保存成功！");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "SaveSerialPortData ERROR");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("数据为空", "SaveSerialPortData ERROR");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SaveSerialPortData ERROR");
                return;
            }
        }

        /// <summary>
        /// 设置chart属性
        /// </summary>
        public static void SetChart()
        {
            Debug.WriteLine("==========setChart=======");
            form1.chart1.ChartAreas[0].AxisX.MajorGrid.LineDashStyle = ChartDashStyle.Dash;//网格刻线为虚线、白色
            form1.chart1.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Gray;
            form1.chart1.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            form1.chart1.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            //图形颜色 
            form1.chart1.Series[0].Color = Color.Blue;
            form1.chart1.Series[1].Color = Color.Orange;
            //设置x轴标题
            form1.chart1.ChartAreas[0].AxisX.Title = "Frequency[Hz]";
            //设置y轴标题
            form1.chart1.ChartAreas[0].AxisY.Title = "Gain[dB]";
            form1.chart1.ChartAreas[0].AxisY2.Title = "Phase[deg]"; //Jay[6/23]
            // Line Type
            form1.chart1.Series[0].ChartType = SeriesChartType.Spline; //Jay[6/23]
            form1.chart1.Series[1].ChartType = SeriesChartType.Spline;
            form1.chart1.Legends[0].Enabled = true;
            // Set the measured freq range
            form1.chart1.ChartAreas[0].AxisX.Minimum = int.Parse(form1.GetStartFreq);
            form1.chart1.ChartAreas[0].AxisX.Maximum = int.Parse(form1.GetEndFreq);
        }
        /// <summary>
        /// VS画chart
        /// </summary>
        public void DrawChart()
        {
            Debug.WriteLine("=======DrawChart===========");
            fake_drawData();
        }

        private void fake_drawData()
        {
            List<double> llx = new List<double>();
            List<double> lly = new List<double>();
            List<double> llz = new List<double>();
            fake_getData(llx, lly, llz);
            chart1.Series["Gain"].Points.DataBindXY(llx, lly); //Jay[6/23]
            chart1.Series["Phase"].Points.DataBindXY(llx, llz); //Jay[6/23]
            // Two items need valifation: provide X xalues & Series.IsXValueIndexed is set to false
            chart1.ChartAreas[0].AxisX.IsLogarithmic = true;
        }

        private void fake_getData(List<double> x, List<double> y, List<double> z)
        {
            string filename;
            filename = filePath + "peakData.txt";
            string[] lines = System.IO.File.ReadAllLines(filename);
            for (int i = 0; i < lines.Length; i++) //Jay[6/23]
            {
                // Use a tab to indent each line of the file.
                string[] myStrA = lines[i].Split(' ');
                x.Add(Double.Parse((myStrA[0])));
                y.Add(Double.Parse((myStrA[1])));
                z.Add(Double.Parse((myStrA[2])));
            }
        }

        /// <summary>
        /// 设置Datagridvew属性
        /// </summary>
        public void SetDataGridView()
        {
            //设置表单不可编辑
            this.dataGridView1.Enabled = false;
            //表格内容设置；新增4行 Jay[7/3]
            int index1 = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index1].Cells[0].Value = "f1";
            this.dataGridView1.Rows[index1].Cells[4].Value = "Hz";
            int index2 = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index2].Cells[0].Value = "Q1";
            this.dataGridView1.Rows[index2].Cells[4].Value = "dB";
            int index3 = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index3].Cells[0].Value = "f2";
            this.dataGridView1.Rows[index3].Cells[4].Value = "Hz";
            int index4 = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index4].Cells[0].Value = "Q2";
            this.dataGridView1.Rows[index4].Cells[4].Value = "dB";
            int index5 = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index5].Cells[0].Value = "f3";
            this.dataGridView1.Rows[index5].Cells[4].Value = "Hz";
            int index6 = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[index6].Cells[0].Value = "Q3";
            this.dataGridView1.Rows[index6].Cells[4].Value = "dB";
            //设置不显示最后一行
            this.dataGridView1.AllowUserToAddRows = true;
            //设置不显示滚动条
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            //设置只读属性
            this.dataGridView1.ReadOnly = true;
            //禁止用户改变列和行的resize
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            //设置填充整个控件；根据column[0]的headercell的width计算整个datagridview的宽度
            this.dataGridView1.Width = this.dataGridView1.Columns[0].HeaderCell.Size.Width * 5;
            this.dataGridView1.Height = this.dataGridView1.Rows[0].HeaderCell.Size.Height * 7;
            this.dataGridView1.ColumnHeadersDefaultCellStyle.BackColor = Color.CadetBlue;
        }

        /// <summary>
        /// 保存datatable数据到excel
        /// </summary>
        /// <param name="tmpDataTable">datatable</param>
        /// <param name="strFileName">excel路径</param>
        public void DataTabletoExcel(System.Data.DataTable tmpDataTable, string strFileName)
        {
            if (tmpDataTable == null)
                return;
            int rowNum = tmpDataTable.Rows.Count;
            int columnNum = tmpDataTable.Columns.Count;
            int rowIndex = 0;
            int columnIndex = 0;

            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
            xlApp.DefaultFilePath = "";
            xlApp.DisplayAlerts = true;
            xlApp.SheetsInNewWorkbook = 1;
            Workbook xlBook = xlApp.Workbooks.Add(true);

            //将DataTable的列名导入Excel表第一行
            //  foreach (DataColumn dc in tmpDataTable.Columns)
            // {
            //     columnIndex++;
            //     xlApp.Cells[rowIndex, columnIndex] = dc.ColumnName;
            //  }

            //将DataTable中的数据导入Excel中
            //  for(int k = 0; k<= 100;k++)
            // {
            for (int i = 0; i < rowNum; i++)
            {
                rowIndex++;
                columnIndex = 0;
                for (int j = 0; j < columnNum; j++)
                {
                    columnIndex++;
                    xlApp.Cells[rowIndex, columnIndex] = tmpDataTable.Rows[i][j].ToString();
                }
                // Thread.Sleep(20);
                // backgroundWorker1.ReportProgress((100 * i) / rowNum, "Working...");
                for (int k = 0; k <= 100; k++)
                {
                    Thread.Sleep(20);
                    backgroundWorker1.ReportProgress(k);
                }

            }
            backgroundWorker1.ReportProgress(100, "Complete!");
            xlBook.SaveCopyAs(strFileName);
            //  }       
        }

        /// <summary>
        /// 读取Datatabel某列数据
        /// </summary>
        /// <param name="mytb">Datatable</param>
        /// <param name="rowNumber">行号</param>
        /// <param name="column">列号</param>
        /// <returns></returns>
        public List<double> ReadTableRowData(System.Data.DataTable mytb, int rowNumber, int column)
        {
            Debug.WriteLine("==========ReadTableRowData=======");
            List<double> line1 = new List<double>();
            DataRow myDr2 = mytb.Rows[rowNumber];
            string titleName = myDr2[column].ToString();
            Debug.WriteLine("==========tableName1=======" + titleName);
            List<object> ls = TransCSVData(mytb, titleName);
            foreach (string dr in ls)
            {
                double x = double.NegativeInfinity;
                bool isSuccessful = double.TryParse(dr, out x);
                line1.Add(x);
                //Debug.WriteLine("==========x=======" + x.ToString());
            }
            return line1;
        }

        /// <summary>
        /// 获取Table某列的值
        /// </summary>
        /// <param name="mytb">Datatable</param>
        /// <param name="titleName">tabal title</param>
        /// <returns>返回数组</returns>
        public List<object> TransCSVData(System.Data.DataTable mytb, string titleName)
        {
            Debug.WriteLine("==========TransData=======");
            List<object> ls = new List<object>();
            //存放你一整列所有的值                    
            foreach (DataRow drr in mytb.Rows)
            {
                ls.Add(drr[titleName]);
            }
            return ls;
        }

        /// <summary>
        /// Csv读取数据到datatable
        /// </summary>
        /// <param name="strpath">csv完整路径</param>
        public void CsvToDatatable(string strpath)
        {
            Debug.WriteLine("==========CsvToDatatable=======");
            int intColCount = 0;
            bool blnFlag = true;
            DataColumn mydc;
            DataRow mydr;
            string strline;
            string[] aryline;
            System.IO.StreamReader mysr = new System.IO.StreamReader(strpath);
            while ((strline = mysr.ReadLine()) != null)
            {
                aryline = strline.Split(new char[] { ',' });
                if (blnFlag)
                {
                    blnFlag = false;
                    intColCount = aryline.Length;
                    for (int i = 0; i < aryline.Length; i++)
                    {
                        mydc = new DataColumn(aryline[i]);
                        dtCsv.Columns.Add(mydc);
                    }
                }
                mydr = dtCsv.NewRow();
                for (int i = 0; i < intColCount; i++)
                {
                    mydr[i] = aryline[i];
                }
                dtCsv.Rows.Add(mydr);
            }
            mysr.Close();
        }
        
        /// <summary>
        /// 获取EXCEL sheet name
        /// </summary>
        /// <param name="filepath"></param>
        /// <param name="numberSheetID"></param>
        /// <returns></returns>
        public string GetFirstSheetName(string filepath, int numberSheetID)
        {
            Debug.WriteLine("==========GetFirstSheetName=======");
            if (!File.Exists(filepath))
            {
                return "File not Exist";
            }
            if (numberSheetID <= 1) { numberSheetID = 1; }
            try
            {
                Microsoft.Office.Interop.Excel.Application xls = new Microsoft.Office.Interop.Excel.Application();
                Workbook xBook = xls.Workbooks.Add(Missing.Value);
                xls.Visible = false;//设置Excel后台运行  
                xls.DisplayAlerts = false;
                string strFirstSheetName = null;
                xBook = xls.Workbooks.Open(filepath, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                    Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                strFirstSheetName = ((Microsoft.Office.Interop.Excel.Worksheet)xBook.Worksheets[1]).Name;
                xBook.Close(Type.Missing, Type.Missing, Type.Missing);
                xBook = null;
                xls.Quit();
                xls = null;
                // KillExecl("xls");
                return strFirstSheetName;
            }
            catch (Exception Err)
            {
                return Err.Message;
            }
        }

        /// <summary>
        /// 读取excel数据到DataTable中
        /// </summary>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>       
        public System.Data.DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn)
        {
            Debug.WriteLine("========ExcelToDataTable=========");
            FileStream fs;
            int startRow = 0;//数据开始行(排除标题行)
            try
            {
                fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);//根据文件流创建excel数据结构
                if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName.IndexOf(".xls") > 0) // 2003版本
                    workbook = new HSSFWorkbook(fs);
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);//第1行
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数
                    if (isFirstRowColumn == true)  //如果第一行是标题列名
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.ToString();
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    dtExcel.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum; //如果第一行是标题列名,数据开始行+1
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum + 1;//如果第一行是标题列名,从第一行开始
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);//第i行
                        if (row == null)
                            continue; //没有数据的行默认是null　
                        DataRow dataRow = dtExcel.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                            //  Debug.WriteLine("========dataRow[j]=========" + dataRow[j]);
                        }
                        dtExcel.Rows.Add(dataRow);
                    }
                }
                return dtExcel;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "提示");
                return null;
            }
        }

        public static void KillExecl(string excelProcessName)
        {
            Debug.WriteLine("========KillExecl=========");
            Process[] procs = Process.GetProcessesByName(excelProcessName);
            foreach (Process pro in procs)
            {
                pro.Kill();//没有更好的方法,只有杀掉进程
            }
            GC.Collect();
        }

        //RS23:9600,8,None,1
        public void fra_Function()
        {
            Debug.WriteLine("==========fra_Function=========");
            fra_serial_open();
            bool status = false;
            int retry = 0;
            int maxRetry = 5;
            //fra_getID(); //robert, analysis not need to get pid.; Jay[7/04]
            fra_change_setting_mode();
            fra_confirm_setting();//Jay[7/04]
            while (status == false && retry < maxRetry)
            {
                status = fra_initialize();
                //Thread.Sleep(1000); // Jay[7/17]
            }
            //isFRA_Open = status; // Jay[7/06] indicate initial status
            string data = fra_measure_freq();
            //Debug.WriteLine("retrun data  =  ",data);
            fra_disable();
            fra_exit_setting();  //Jay[7/16]
            //fra_confirm_exit_setting(); //Jay[8/13] may induce serial port warning
            serialPort1.Close();
        }

        /// <summary>
        /// 读取driver IC Product 
        /// </summary>
        private int fra_getID()
        {
            try
            {
                Debug.WriteLine("==========fra_getID=========");
                //发送数据
                byte[] buffer = new byte[] { 0x03, 0x03, 0x00, 0x00 };//读取driver IC Product command 4bytes,确认IC是否正确
                serialPort1.Write(buffer, 0, 4);//使用缓冲区的数据将指定数量的字符写入串行端口
                serialPort1.DiscardOutBuffer();//清除串行驱动程序发送缓冲区的数据
                Thread.Sleep(10);
                int bufSize = serialPort1.BytesToRead;//得到 接收到数据的字节数
                byte[] rbuffer = new byte[bufSize];//动态创建一个byte[]
                serialPort1.Read(rbuffer, 0, bufSize);//读取数据
                if (rbuffer[0] == 0x00 & rbuffer[1] == 0x00 & rbuffer[3] == 0x00)
                {
                    PID = (int)rbuffer[2];
                    Debug.WriteLine("fra PID:" + PID.ToString());
                    return PID;
                }
                else
                {
                    Debug.WriteLine("fra get PID fail");
                    return PID;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化串口发生错误：" + ex.Message, "fra_getID", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return PID;
            }
        }

        /// <summary>
        /// Change to setting mode(AKM7371不需要）
        /// </summary>
        private void fra_change_setting_mode()
        {
            Debug.WriteLine("==========fra_change_setting_mode=========");
            byte[] buffer = new byte[] { 0x01, 0xAE, 0x3B, 0x00 };
            try
            {
                serialPort1.Write(buffer, 0, 4);
                serialPort1.DiscardOutBuffer();
                Thread.Sleep(10);
                bool status = true;
                Debug.WriteLine("change setting mode status = " + status.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化串口发生错误：" + ex.Message, "fra_change_setting_mode", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        ///confirm whether into setting mode
        ///AKM7371不需要
        ///if data bit7=1 success
        /// </summary>
        private void fra_confirm_setting()
        {
            Debug.WriteLine("==========fra_confirm_setting=========");
            byte[] buffer = new byte[] { 0x03, 0x02, 0x0, 0x0 };
            byte confirmByte = 0x80; // Jay[7/06]
            try
            {
                serialPort1.Write(buffer, 0, 4);
                serialPort1.DiscardOutBuffer();
                Thread.Sleep(30);
                int bufSize = serialPort1.BytesToRead;
                byte[] rbuffer = new byte[bufSize];
                serialPort1.Read(rbuffer, 0, bufSize);//读取数据 Jay[7/04]
                bool status = false;
                if (rbuffer[0] == 0x00 & rbuffer[1] == 0x00 & rbuffer[3] == 0x00)
                {// bit 7=1 success
                    if ((rbuffer[2] & 0x80) == confirmByte)//10000000 Jay[7/06]
                        status = true;
                }
                Debug.WriteLine("Confirm setting status = " + status.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化串口发生错误：" + ex.Message, "fra_confirm_setting", MessageBoxButtons.OK, MessageBoxIcon.Information);//Jay[7/04]
                return;
            }
        }

        /// <summary>
        /// 设置振幅波
        /// Initialize FRA function and set wave amplitude -Open loop mode
        /// data means aplitude,default 60
        /// need wait 1000ms to check return
        /// return "1" means OK
        /// </summary>
        /// <returns></returns>
        private bool fra_initialize()
        {
            Debug.WriteLine("==========fra_initialize=========");
            bool status = false;
            byte Ampbyte = 60; // default value; Jay[7/10]
            try
            {
                Ampbyte = Convert.ToByte(txtAmp.Text); //Jay[7/10]
            }
            catch (FormatException)
            {
                MessageBox.Show("Retry Integer Value","Incorrect Format",MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (OverflowException)
            {
                MessageBox.Show("Retry Integer Value", "Overflow", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            byte[] buffer = new byte[] { 0x19, Ampbyte, 0x0, 0x0 }; // Open loop Jay[7/06]

            if (isFRA_Open == false)
                buffer = new byte[] { 0x1A, Ampbyte, 0x0, 0x0 }; // Close loop Jay[7/06]
            try
            {
                //receive_clearData();
                serialPort1.Write(buffer, 0, 4);
                serialPort1.DiscardOutBuffer();
                Thread.Sleep(1000);
                //Application.DoEvents();
                int rLength = serialPort1.BytesToRead; //看buffer區有多少bytes
                int tryIndex = 0;
                while (rLength == 0 && tryIndex < tryMax)
                {
                    Thread.Sleep(50);
                    rLength = serialPort1.BytesToRead; //看buffer區有多少bytes //Jay[7/04]
                    tryIndex++;
                }

                if (rLength > 0)
                {
                    byte[] rbuffer = new byte[rLength];
                    serialPort1.Read(rbuffer, 0, rLength);
                    string str1 = System.Text.Encoding.ASCII.GetString(rbuffer);
                    if (str1 == "1")
                    {
                        Debug.WriteLine("FRA Initialize PASS!!!");
                        status = true;
                    }
                }
                else
                {
                    Debug.WriteLine("[Drive] no received data");
                    status = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化串口发生错误：" + ex.Message, "fra_initialize", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return status;
        }

        /// <summary>
        /// Disable FRA function -Open loop mode
        /// </summary>
        private void fra_disable()
        {
            Debug.WriteLine("==========fra_disable=========");
            byte[] buffer = new byte[] { 0x1B, 0x0, 0x0, 0x0 }; // Open loop Jay[7/06]

            if (isFRA_Open == false)
                buffer = new byte[] { 0x1C, 0x0, 0x0, 0x0 }; // Close loop Jay[7/06]
            try
            {
                serialPort1.Write(buffer, 0, 4);
                serialPort1.DiscardOutBuffer();
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化串口发生错误：" + ex.Message, "fra_disable", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //Debug.WriteLine("close FRA");
        }

        /// <summary>
        /// Exit setting mode(AKM7371不需要)
        /// </summary>
        private void fra_exit_setting()
        {
            Debug.WriteLine("==========fra_exit_setting=========");
            byte[] buffer = new byte[] { 0x01, 0xAE, 0x01, 0x0 };
            try
            {
                serialPort1.Write(buffer, 0, 4);
                serialPort1.DiscardOutBuffer();
                Thread.Sleep(30);
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化串口发生错误：" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// command 同fra_confirm_setting???
        /// </summary>
        private void fra_confirm_exit_setting()
        {
            Debug.WriteLine("==========fra_confirm_exit_setting=========");
            byte[] buffer = new byte[] { 0x03, 0x02, 0x00, 0x00 };
            byte confirmByte = 0x00; // Jay[7/10]
            try
            {
                serialPort1.Write(buffer, 0, 4);
                serialPort1.DiscardOutBuffer();
                Thread.Sleep(10);
                int bufSize = serialPort1.BytesToRead;
                byte[] rbuffer = new byte[bufSize];
                serialPort1.Read(rbuffer, 0, bufSize);
                bool status = false;
                if (rbuffer[0] == 0x00 & rbuffer[1] == 0x00 & rbuffer[3] == 0x00)
                {
                    if ((rbuffer[2] & 0x80) == confirmByte)//00000000 Jay[7/10]
                        status = true;
                }
                Debug.WriteLine("Confirm exit setting mode status = " + status.ToString()); //Jay[7/04]
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化串口发生错误：" + ex.Message, "fra_confirm_exit_setting", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        /// <summary>
        /// Set frequency and get Gain and Phase
        /// Remark1:  頻率範圍 10~10000Hz 
        /// Remark2:  NumFreq = (log10(freq.)-1)/0.01 
        /// Remark3:  sleep time = 30 * 1000 / Freq.(ms) + 100(ms)
        /// Remark4:  回傳 17  字元,  格式: “gain,phase ”,其中 gain 與 phase 共 8 字元
        /// Remark5:  注意掃頻頻率間隔需固定,  才能符合 log 座標間隔
        /// 0202 fiona change to return [data] to save 
        /// </summary>
        private string fra_measure_freq()
        {
            string data = null;
            string saveData = null;
            string str1;
            string[] str2;
            List<double> llx = new List<double>();
            List<double> lly = new List<double>();
            List<double> llz = new List<double>();
            int k =0;
            string filename2 = filePath + "peakData.txt";
            StreamWriter writer = new StreamWriter(filename2);
            chart1.Series["Gain"].Points.Clear();
            chart1.Series["Phase"].Points.Clear();
            try
            {
                Debug.WriteLine("==========fra_measure_freq=========");
                //---measure FRA
                uint IDvalue = 0; // singel frequency
                double realFreq = 0f;
                int sleepTime = 0;
                double pF = 0;
                int numFreq = 0;
                int end_numFreq = 0;
                int loopMax = 0; // Jay[6/23]
                //StreamWriter wt = null;
                //string strMode = "Close";

                //if (isFRA_Open == true)
                //    strMode = "Open"; //for printout Jay[7/06]

                //如果isFRA_SingleFreq==true，从IDvalue获取值
                if (isFRA_SingleFreq == true) //---Single freq. (設定freq.再轉換成numFreq)
                {
                    IDvalue = 10;
                    if (IDvalue < 10 || IDvalue > 10000)//判断输入值是否在范围内
                    {
                        MessageBox.Show("freq. must 10~10000");
                        return data;
                    }
                    else
                    {
                        MessageBox.Show("Start Test Freq. : " + IDvalue.ToString() + "Hz");
                        sleepTime = (int)(30 * 1000 / IDvalue);
                        pF = Math.Log10(IDvalue);//Math.log() 计算自然对数。返回指定数字以 10 为底的对数
                        numFreq = 0;
                        pF = (pF - 1f) / 0.01;
                    }
                }

                else // 掃頻mode 變動numFreq
                { // isFRA_SingleFreq == false, 从startFreq & endFreq获取值
              //    MessageBox.Show("Start Test Freq. : 10 ~ 10000 Hz");
                    int startFreq = int.Parse(txtStartFreq.Text);
                    int endFreq = int.Parse(txtEndFreq.Text);
              //    MessageBox.Show("Scan Test Freq. : " + startFreq.ToString() + "~" + endFreq.ToString() + "Hz");
                    if (startFreq < 10)
                    {
                        MessageBox.Show("Scan Start freq. must > 10");
                        return data;
                    }
                    else if (endFreq > 10000)
                    {
                        MessageBox.Show("Scan end freq. must <= 10000");
                        return data;
                    }
                    numFreq = (int)(Math.Round((Math.Log10(startFreq) - 1) / 0.01, 0, MidpointRounding.AwayFromZero));//Math.Round
                    end_numFreq = (int)(Math.Round((Math.Log10(endFreq) - 1) / 0.01, 0, MidpointRounding.AwayFromZero));
                    loopMax = (int)(Math.Round((double)end_numFreq, MidpointRounding.AwayFromZero)); // Jay[6/23]
                    data = "Freq,Gain,Phase";
                    saveData = "Freq,Gain,Phase\n";
                    form1.chart1.ChartAreas[0].AxisX.Minimum = startFreq;
                    form1.chart1.ChartAreas[0].AxisX.Maximum = endFreq;
                }

                //sleepTime = (int)(30 * 1000 / IDvalue);
                //pF = Math.Log10(IDvalue);
                //numFreq = 0;
                //pF = (pF - 1f) / 0.01;
                for (int i = 0; i <= loopMax; i++) // i =0, 初始 10 Hz
                {
                    //????  Application.DoEvents();
                    if (isFRA_SingleFreq == true)
                    {
                        numFreq = (int)Math.Round(pF, 0, MidpointRounding.AwayFromZero);
                    }
                    else
                    {
                        realFreq = 1.0f + numFreq * 0.01;
                        realFreq = Math.Pow(10, realFreq);
                        sleepTime = (int)(30.0 * 1000 / realFreq);
                    }

                    byte numFreq_high = (byte)(numFreq >> 8);
                    byte numFreq_low = (byte)(numFreq & 0xff);

                    int kk = numFreq_high << 8 | numFreq_low;
                    if (kk > end_numFreq)//大于end_numFreq
                        break;
                    //MessageBox.Show(numFreq.ToString() + "," + numFreq_high.ToString() + "," + numFreq_low.ToString() + "," + kk.ToString());

                    //open loop mode command 0x17, 0x0, numFreq_high, numFreq_low
                    byte[] buffer = new byte[] { 0x17, 0x0, numFreq_high, numFreq_low };
                    if (isFRA_Open == false)  
                        buffer = new byte[] { 0x18, 0x0, numFreq_high, numFreq_low };//close loop mode

                    serialPort1.Write(buffer, 0, 4);
                    serialPort1.DiscardOutBuffer();
                    Thread.Sleep(sleepTime + 100); //wait return data ready

                    //----receive return data
                    int rLength = 0;
                    int tryMax = 50;
                    int tryIndex = 0;
                    rLength = serialPort1.BytesToRead; //看buffer區有多少bytes 读取串口数据的长度
                    while (rLength < 17 && tryIndex < tryMax)
                    {
                        Thread.Sleep(50);
                        rLength = serialPort1.BytesToRead; //看buffer區有多少bytes 读取串口数据的长度
                        tryIndex++;
                    }

                    if (rLength >= 17) //有資料才讀，不然會死機
                    {
                        byte[] rbuffer = new byte[rLength]; // 创建动态的数组，数组的长度是从串口中读到
                        serialPort1.Read(rbuffer, 0, rLength);//从串口中读取数据放到数组中
                        str1 = System.Text.Encoding.ASCII.GetString(rbuffer);
                        str2 = str1.Split(',');//单字符切割 

                        double dGain = double.NegativeInfinity;
                        double dPhase = double.NegativeInfinity;
                        double.TryParse(str2[0], out dGain);
                        double.TryParse(str2[1], out dPhase);
                        double realFreq_data = Math.Round(realFreq, 1, MidpointRounding.AwayFromZero);
                        chart1.Series["Gain"].Points.AddXY(realFreq_data, dGain); //Jay[7/04]
                        chart1.Series["Phase"].Points.AddXY(realFreq_data, dPhase); //Jay[7/04]
                        // Two items need valifation: provide X xalues & Series.IsXValueIndexed is set to false
                        chart1.ChartAreas[0].AxisX.IsLogarithmic = true;

                        Console.Write(realFreq_data + "," + dGain + "," + dPhase + "\n"); //Jay[7/04] 
                        //        chart1.Series["Phase"].Points.DataBindY(lly); //Jay[6/23]
                        //        chart1.Series["Gain"].Points.DataBindY(llz); //Jay[6/23]
                        saveData += realFreq.ToString("f1") + "," + str2[0] + "," + str2[1] + '\n';                        
                        data += realFreq.ToString("f1") + "," + str2[0] + "," + str2[1]; //Jay[7/04]
                        writer.WriteLine(realFreq_data + " " + dGain + " " + dPhase);//Jay[6/23]"peakData.txt"                         
                        k++;
                    }
                    else
                    {
                        MessageBox.Show("[Drive] no received data");
                    }
                    numFreq++;
                    //return data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("fra_measure_freq ERROR" + ex.Message, "fra_measure_freq", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            try
            {
                SaveSerialPortData(serialfileName, saveData);//Jay[6/23]保存serial port data to "FRA_test.csv"
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SaveSerialPortData ERROR");
            }
            writer.Close();
            chart1.Visible = true;
            btnBeginChart.Text = "数据分析中...";
            btnBeginChart.BackColor = Color.Gray;
            btnBeginChart.Enabled = false;
            AnalysisDataThread();
            return data;
        }

        private void btnIDvalue_Click(object sender, EventArgs e)
        {
            fra_getID();
            combxIDvalue.Items.Clear();
            try
            {
                switch (PID)
                {
                    case 14:
                        this.combxIDvalue.Text = "AKM7374";
                        break;
                    case 9:
                        this.combxIDvalue.Text = "AKM7371";
                        break;
                    case 15:
                        this.combxIDvalue.Text = "AKM7375";
                        break;
                    default:
                        this.combxIDvalue.Text = "NONE";
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("None PID Return" + ex.Message, "PID Initialization Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            combxIDvalue.Items.Clear();
        }

        private void chk_ch1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void InitializeTrackBar()
        {
            trbAmp.Minimum = 0;
            trbAmp.Maximum = 60;
            trbAmp.TickFrequency = 5;
            trbAmp.Scroll += new EventHandler(trbAmp_Scroll);
            trbOffset.Minimum = 0;
            trbOffset.Maximum = 10;
            trbOffset.TickFrequency = 1;
            trbOffset.Scroll += new EventHandler(trbOffset_Scroll);
        }

        private void trbAmp_Scroll(object sender, EventArgs e)
        {
            txtAmp.Text = "" + trbAmp.Value;
        }

        private void trbOffset_Scroll(object sender, EventArgs e)
        {
            txtOffset.Text = "" + trbOffset.Value;
        }
    }

}