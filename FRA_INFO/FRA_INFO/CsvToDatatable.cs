using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FRA_INFO
{
    class CsvToDatatable
    {
        /// <summary>
        /// 将csv文件的数据转成datatable
        /// </summary>
        /// <param name="csvfilePath">csv文件路径</param>
        /// <param name="firstIsRowHead">是否将第一行作为字段名</param>
        /// <returns></returns>
        public static DataTable CsvToDataTable(string csvfilePath, bool firstIsRowHead)
        {
            Debug.WriteLine("==========CsvToDataTable=======");
            DataTable dtResult = null;
            if (File.Exists(csvfilePath))
            {
                string csvstr = File.ReadAllText(csvfilePath, Encoding.Default);
                if (!string.IsNullOrEmpty(csvstr))
                {
                    dtResult = ToDataTable(csvstr, firstIsRowHead);
                }
            }
            return dtResult;
        }

        /// <summary>
        /// 将CSV数据转换为DataTable
        /// </summary>
        /// <param name="csv">包含以","分隔的CSV数据的字符串</param>
        /// <param name="isRowHead">是否将第一行作为字段名</param>
        /// <returns></returns>
        private static DataTable ToDataTable(string csv, bool isRowHead)
        {
            Debug.WriteLine("==========ToDataTable=======");
            DataTable dt = null;
            if (!string.IsNullOrEmpty(csv))
            {
                dt = new DataTable();
                string[] csvRows = csv.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                string[] csvColumns = null;
                if (csvRows != null)
                {
                    if (csvRows.Length > 0)
                    {
                        //第一行作为字段名,添加第一行记录并删除csvRows中的第一行数据
                        if (isRowHead)
                        {
                            csvColumns = FromCsvLine(csvRows[0]);
                            csvRows[0] = null;
                            for (int i = 0; i < csvColumns.Length; i++)
                            {
                                dt.Columns.Add(csvColumns[i]);
                            }
                        }

                        for (int i = 0; i < csvRows.Length; i++)
                        {
                            if (csvRows[i] != null)
                            {
                                csvColumns = FromCsvLine(csvRows[i]);
                                //检查列数是否足够,不足则补充
                                if (dt.Columns.Count < csvColumns.Length)
                                {
                                    int columnCount = csvColumns.Length - dt.Columns.Count;
                                    for (int c = 0; c < columnCount; c++)
                                    {
                                        dt.Columns.Add();
                                    }
                                }
                                dt.Rows.Add(csvColumns);
                            }
                        }
                    }
                }
            }

            return dt;
        }
        /// <summary>
        /// 解析一行CSV数据
        /// </summary>
        /// <param name="csv">csv数据行</param>
        /// <returns></returns>
        public static string[] FromCsvLine(string csv)
        {
            Debug.WriteLine("==========FromCsvLine=======");
            List<string> csvLiAsc = new List<string>();
            List<string> csvLiDesc = new List<string>();

            if (!string.IsNullOrEmpty(csv))
            {
                //顺序查找
                int lastIndex = 0;
                int quotCount = 0;
                //剩余的字符串
                string lstr = string.Empty;
                for (int i = 0; i < csv.Length; i++)
                {
                    if (csv[i] == '"')
                    {
                        quotCount++;
                    }
                    else if (csv[i] == ',' && quotCount % 2 == 0)
                    {
                        csvLiAsc.Add(ReplaceQuote(csv.Substring(lastIndex, i - lastIndex)));
                        lastIndex = i + 1;
                    }
                    if (i == csv.Length - 1 && lastIndex < csv.Length)
                    {
                        lstr = csv.Substring(lastIndex, i - lastIndex + 1);
                    }
                }
                if (!string.IsNullOrEmpty(lstr))
                {
                    //倒序查找
                    lastIndex = 0;
                    quotCount = 0;
                    string revStr = Reverse(lstr);
                    for (int i = 0; i < revStr.Length; i++)
                    {
                        if (revStr[i] == '"')
                        {
                            quotCount++;
                        }
                        else if (revStr[i] == ',' && quotCount % 2 == 0)
                        {
                            csvLiDesc.Add(ReplaceQuote(Reverse(revStr.Substring(lastIndex, i - lastIndex))));
                            lastIndex = i + 1;
                        }
                        if (i == revStr.Length - 1 && lastIndex < revStr.Length)
                        {
                            csvLiDesc.Add(ReplaceQuote(Reverse(revStr.Substring(lastIndex, i - lastIndex + 1))));
                            lastIndex = i + 1;
                        }

                    }
                    string[] tmpStrs = csvLiDesc.ToArray();
                    Array.Reverse(tmpStrs);
                    csvLiAsc.AddRange(tmpStrs);
                }
            }

            return csvLiAsc.ToArray();
        }
        /// <summary>
        /// 反转字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string Reverse(string str)
        {
            Debug.WriteLine("==========Reverse=======");
            string revStr = string.Empty;
            foreach (char chr in str)
            {
                revStr = chr.ToString() + revStr;
            }
            return revStr;
        }
        /// <summary>
        /// 替换CSV中的双引号转义符为正常双引号,并去掉左右双引号
        /// </summary>
        /// <param name="csvValue">csv格式的数据</param>
        /// <returns></returns>
        private static string ReplaceQuote(string csvValue)
        {
            Debug.WriteLine("==========ReplaceQuote=======");
            string rtnStr = csvValue;
            if (!string.IsNullOrEmpty(csvValue))
            {
                //首尾都是"
                Match m = Regex.Match(csvValue, "^\"(.*?)\"$");
                if (m.Success)
                {
                    rtnStr = m.Result("${1}").Replace("\"\"", "\"");
                }
                else
                {
                    rtnStr = rtnStr.Replace("\"\"", "\"");
                }
            }
            return rtnStr;

        }

        public static DataTable OpenCSV(string fileName)
        {
            DataTable dt = new DataTable();
            FileStream fs = new FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            StreamReader sr = new StreamReader(fs, System.Text.Encoding.Default);
            //记录每次读取的一行记录
            string strLine = "";
            //记录每行记录中的各字段内容
            string[] aryLine;
            //标示列数
            int columnCount = 0;
            //标示是否是读取的第一行
            bool IsFirst = true;

            //逐行读取CSV中的数据
            while ((strLine = sr.ReadLine()) != null)
            {
                aryLine = strLine.Split(',');
                if (IsFirst == true)
                {
                    IsFirst = false;
                    columnCount = aryLine.Length;
                    string ColumnName = "Column ";
                    //创建列
                    for (int i = 0; i < columnCount; i++)
                    {

                        DataColumn dc = new DataColumn(ColumnName + i.ToString());
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }

            sr.Close();
            fs.Close();
            return dt;
        }

        public static void CsvTest(string strpath)
        {

            int intColCount = 0;
            bool blnFlag = true;
            DataTable mydt = new DataTable("myTableName");

            DataColumn mydc;
            DataRow mydr;

            //string strpath = ""; //cvs文件路径
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
                        mydt.Columns.Add(mydc);
                    }
                }

                mydr = mydt.NewRow();
                for (int i = 0; i < intColCount; i++)
                {
                    mydr[i] = aryline[i];
                    mydt.Rows.Add(mydr);

                }
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">file 完整路径</param>
        public void DeletePicture(string filePath)
        {
            Debug.WriteLine("=======DeletePicture======");
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

        /// <summary>  
        /// 将Csv读入DataTable  
        /// </summary>  
        /// <param name="filePath">csv文件路径</param>  
        /// <param name="n">表示第n行是字段title,第n+1行是记录开始</param>  
        /// <param name="k">可选参数表示最后K行不算记录默认0</param>  
        public System.Data.DataTable csv2dt(string filePath, int n, System.Data.DataTable dtb) //这个dt 是个空白的没有任何行列的DataTable  
        {
            Debug.WriteLine("=======csv2dt=====");
            String csvSplitBy = "(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)";
            StreamReader reader = new StreamReader(filePath, System.Text.Encoding.Default, false);
            int i = 0, m = 0;
            reader.Peek();
            while (reader.Peek() > 0)
            {
                m = m + 1;
                string str = reader.ReadLine();
                if (m >= n)
                {
                    if (m == n) //如果是字段行，则自动加入字段。  
                    {
                        MatchCollection mcs = Regex.Matches(str, csvSplitBy);
                        foreach (Match mc in mcs)
                        {
                            dtb.Columns.Add(mc.Value); //增加列标题  
                        }
                    }
                    else
                    {
                        MatchCollection mcs = Regex.Matches(str, "(?<=^|,)(\"(?:[^\"]|\"\")*\"|[^,]*)");
                        i = 0;
                        System.Data.DataRow dr = dtb.NewRow();
                        foreach (Match mc in mcs)
                        {
                            dr[i] = mc.Value;
                            i++;
                        }
                        dtb.Rows.Add(dr);  //DataTable 增加一行   
                        Debug.WriteLine(" dr" + dr.ToString());
                    }
                }
            }
            return dtb;
        }

        public void SaveSerialDataToXLS(string testfilePath)
        {
            Debug.WriteLine(" ===========SaveSerialDataToXLS ===========");
            try
            {

                string[] writeToXLX = { "Hello", "From", "Tutorials", "Point" };
                ExcelClass.SaveXls(testfilePath, writeToXLX, 0, 1);// string[] lines, int rowIndex, int col
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SaveSerialDataToXLS ERROR");
            }
        }
    }
}