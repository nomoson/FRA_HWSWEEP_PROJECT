namespace FRA_INFO
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.SETTING = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cmbSerials = new System.Windows.Forms.ComboBox();
            this.combxIDvalue = new System.Windows.Forms.ComboBox();
            this.txtCheckslvAddres = new System.Windows.Forms.TextBox();
            this.btnIDvalue = new System.Windows.Forms.Button();
            this.btnCheckslvAddres = new System.Windows.Forms.Button();
            this.btSetCOMport = new System.Windows.Forms.Button();
            this.gbxLoopTransferFunction = new System.Windows.Forms.GroupBox();
            this.txtStartFreq = new System.Windows.Forms.TextBox();
            this.txtEndFreq = new System.Windows.Forms.TextBox();
            this.lblStartFreq = new System.Windows.Forms.Label();
            this.lblEndFreq = new System.Windows.Forms.Label();
            this.gbxActuatorFrequency = new System.Windows.Forms.GroupBox();
            this.chk_ch3 = new System.Windows.Forms.CheckBox();
            this.chk_ch2 = new System.Windows.Forms.CheckBox();
            this.chk_ch1 = new System.Windows.Forms.CheckBox();
            this.gbxSettingFraInt = new System.Windows.Forms.GroupBox();
            this.trbAmp = new System.Windows.Forms.TrackBar();
            this.trbOffset = new System.Windows.Forms.TrackBar();
            this.txtWaitCycle = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmp = new System.Windows.Forms.TextBox();
            this.txtOffset = new System.Windows.Forms.TextBox();
            this.btnAmp = new System.Windows.Forms.Button();
            this.btnOffset = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkSavedata = new System.Windows.Forms.CheckBox();
            this.txtSaveData = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmMax = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cluValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmUnit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnSerialsBegin = new System.Windows.Forms.Button();
            this.btnNumbValue = new System.Windows.Forms.Button();
            this.btnLineNumbValue = new System.Windows.Forms.Button();
            this.btnDateValue = new System.Windows.Forms.Button();
            this.btnBeginChart = new System.Windows.Forms.Button();
            this.btnNumb = new System.Windows.Forms.Button();
            this.btnLineNumb = new System.Windows.Forms.Button();
            this.btnDate = new System.Windows.Forms.Button();
            this.btnTypeValue = new System.Windows.Forms.Button();
            this.btnTypeName = new System.Windows.Forms.Button();
            this.lblResult = new System.Windows.Forms.Label();
            this.btnSpec = new System.Windows.Forms.Button();
            this.btnModel = new System.Windows.Forms.Button();
            this.lbtext = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.SETTING.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbxLoopTransferFunction.SuspendLayout();
            this.gbxActuatorFrequency.SuspendLayout();
            this.gbxSettingFraInt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbAmp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbOffset)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // SETTING
            // 
            this.SETTING.Controls.Add(this.tabPage2);
            this.SETTING.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SETTING.Location = new System.Drawing.Point(12, 209);
            this.SETTING.Name = "SETTING";
            this.SETTING.SelectedIndex = 0;
            this.SETTING.Size = new System.Drawing.Size(563, 352);
            this.SETTING.TabIndex = 6;
            this.SETTING.Tag = "";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.progressBar);
            this.tabPage2.Controls.Add(this.picLogo);
            this.tabPage2.Controls.Add(this.groupBox1);
            this.tabPage2.Controls.Add(this.gbxLoopTransferFunction);
            this.tabPage2.Controls.Add(this.gbxActuatorFrequency);
            this.tabPage2.Controls.Add(this.gbxSettingFraInt);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(555, 326);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "FRAInt";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(13, 201);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(184, 15);
            this.progressBar.TabIndex = 10;
            // 
            // picLogo
            // 
            this.picLogo.AccessibleRole = System.Windows.Forms.AccessibleRole.TitleBar;
            this.picLogo.ErrorImage = global::FRA_INFO.Properties.Resources.idologo;
            this.picLogo.InitialImage = null;
            this.picLogo.Location = new System.Drawing.Point(213, 149);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(125, 74);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLogo.TabIndex = 11;
            this.picLogo.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cmbSerials);
            this.groupBox1.Controls.Add(this.combxIDvalue);
            this.groupBox1.Controls.Add(this.txtCheckslvAddres);
            this.groupBox1.Controls.Add(this.btnIDvalue);
            this.groupBox1.Controls.Add(this.btnCheckslvAddres);
            this.groupBox1.Controls.Add(this.btSetCOMport);
            this.groupBox1.Location = new System.Drawing.Point(7, 229);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(535, 103);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comm.";
            // 
            // cmbSerials
            // 
            this.cmbSerials.FormattingEnabled = true;
            this.cmbSerials.Location = new System.Drawing.Point(14, 59);
            this.cmbSerials.Name = "cmbSerials";
            this.cmbSerials.Size = new System.Drawing.Size(132, 21);
            this.cmbSerials.TabIndex = 7;
            // 
            // combxIDvalue
            // 
            this.combxIDvalue.FormattingEnabled = true;
            this.combxIDvalue.Location = new System.Drawing.Point(375, 57);
            this.combxIDvalue.Name = "combxIDvalue";
            this.combxIDvalue.Size = new System.Drawing.Size(140, 21);
            this.combxIDvalue.TabIndex = 6;
            this.combxIDvalue.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txtCheckslvAddres
            // 
            this.txtCheckslvAddres.Location = new System.Drawing.Point(196, 59);
            this.txtCheckslvAddres.Name = "txtCheckslvAddres";
            this.txtCheckslvAddres.Size = new System.Drawing.Size(132, 20);
            this.txtCheckslvAddres.TabIndex = 4;
            // 
            // btnIDvalue
            // 
            this.btnIDvalue.Location = new System.Drawing.Point(375, 19);
            this.btnIDvalue.Name = "btnIDvalue";
            this.btnIDvalue.Size = new System.Drawing.Size(140, 23);
            this.btnIDvalue.TabIndex = 2;
            this.btnIDvalue.Text = "Detect IC";
            this.btnIDvalue.UseVisualStyleBackColor = true;
            this.btnIDvalue.Click += new System.EventHandler(this.btnIDvalue_Click);
            // 
            // btnCheckslvAddres
            // 
            this.btnCheckslvAddres.Location = new System.Drawing.Point(196, 20);
            this.btnCheckslvAddres.Name = "btnCheckslvAddres";
            this.btnCheckslvAddres.Size = new System.Drawing.Size(132, 23);
            this.btnCheckslvAddres.TabIndex = 1;
            this.btnCheckslvAddres.Text = "Check Slv Addr";
            this.btnCheckslvAddres.UseVisualStyleBackColor = true;
            // 
            // btSetCOMport
            // 
            this.btSetCOMport.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btSetCOMport.Location = new System.Drawing.Point(14, 19);
            this.btSetCOMport.Name = "btSetCOMport";
            this.btSetCOMport.Size = new System.Drawing.Size(132, 23);
            this.btSetCOMport.TabIndex = 0;
            this.btSetCOMport.Text = "Open COM Port";
            this.btSetCOMport.UseVisualStyleBackColor = true;
            this.btSetCOMport.Click += new System.EventHandler(this.button1_Click);
            // 
            // gbxLoopTransferFunction
            // 
            this.gbxLoopTransferFunction.Controls.Add(this.txtStartFreq);
            this.gbxLoopTransferFunction.Controls.Add(this.txtEndFreq);
            this.gbxLoopTransferFunction.Controls.Add(this.lblStartFreq);
            this.gbxLoopTransferFunction.Controls.Add(this.lblEndFreq);
            this.gbxLoopTransferFunction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxLoopTransferFunction.Location = new System.Drawing.Point(350, 121);
            this.gbxLoopTransferFunction.Name = "gbxLoopTransferFunction";
            this.gbxLoopTransferFunction.Size = new System.Drawing.Size(192, 102);
            this.gbxLoopTransferFunction.TabIndex = 2;
            this.gbxLoopTransferFunction.TabStop = false;
            this.gbxLoopTransferFunction.Text = "Sweep Frequency";
            // 
            // txtStartFreq
            // 
            this.txtStartFreq.Location = new System.Drawing.Point(70, 40);
            this.txtStartFreq.Name = "txtStartFreq";
            this.txtStartFreq.Size = new System.Drawing.Size(100, 20);
            this.txtStartFreq.TabIndex = 3;
            this.txtStartFreq.Text = "10";
            // 
            // txtEndFreq
            // 
            this.txtEndFreq.Location = new System.Drawing.Point(70, 66);
            this.txtEndFreq.Name = "txtEndFreq";
            this.txtEndFreq.Size = new System.Drawing.Size(100, 20);
            this.txtEndFreq.TabIndex = 2;
            this.txtEndFreq.Text = "2000";
            // 
            // lblStartFreq
            // 
            this.lblStartFreq.AutoSize = true;
            this.lblStartFreq.Location = new System.Drawing.Point(9, 40);
            this.lblStartFreq.Name = "lblStartFreq";
            this.lblStartFreq.Size = new System.Drawing.Size(59, 13);
            this.lblStartFreq.TabIndex = 1;
            this.lblStartFreq.Text = "StartFreq";
            // 
            // lblEndFreq
            // 
            this.lblEndFreq.AutoSize = true;
            this.lblEndFreq.Location = new System.Drawing.Point(9, 71);
            this.lblEndFreq.Name = "lblEndFreq";
            this.lblEndFreq.Size = new System.Drawing.Size(54, 13);
            this.lblEndFreq.TabIndex = 0;
            this.lblEndFreq.Text = "EndFreq";
            // 
            // gbxActuatorFrequency
            // 
            this.gbxActuatorFrequency.Controls.Add(this.chk_ch3);
            this.gbxActuatorFrequency.Controls.Add(this.chk_ch2);
            this.gbxActuatorFrequency.Controls.Add(this.chk_ch1);
            this.gbxActuatorFrequency.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxActuatorFrequency.Location = new System.Drawing.Point(350, 7);
            this.gbxActuatorFrequency.Name = "gbxActuatorFrequency";
            this.gbxActuatorFrequency.Size = new System.Drawing.Size(192, 100);
            this.gbxActuatorFrequency.TabIndex = 1;
            this.gbxActuatorFrequency.TabStop = false;
            this.gbxActuatorFrequency.Text = "Channel";
            // 
            // chk_ch3
            // 
            this.chk_ch3.AutoSize = true;
            this.chk_ch3.Location = new System.Drawing.Point(136, 42);
            this.chk_ch3.Name = "chk_ch3";
            this.chk_ch3.Size = new System.Drawing.Size(50, 17);
            this.chk_ch3.TabIndex = 4;
            this.chk_ch3.Text = "CH3";
            this.chk_ch3.UseVisualStyleBackColor = true;
            // 
            // chk_ch2
            // 
            this.chk_ch2.AutoSize = true;
            this.chk_ch2.Location = new System.Drawing.Point(72, 42);
            this.chk_ch2.Name = "chk_ch2";
            this.chk_ch2.Size = new System.Drawing.Size(50, 17);
            this.chk_ch2.TabIndex = 3;
            this.chk_ch2.Text = "CH2";
            this.chk_ch2.UseVisualStyleBackColor = true;
            this.chk_ch2.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged_1);
            // 
            // chk_ch1
            // 
            this.chk_ch1.AutoSize = true;
            this.chk_ch1.Checked = true;
            this.chk_ch1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chk_ch1.Location = new System.Drawing.Point(12, 42);
            this.chk_ch1.Name = "chk_ch1";
            this.chk_ch1.Size = new System.Drawing.Size(50, 17);
            this.chk_ch1.TabIndex = 1;
            this.chk_ch1.Text = "CH1";
            this.chk_ch1.UseVisualStyleBackColor = true;
            this.chk_ch1.CheckedChanged += new System.EventHandler(this.chk_ch1_CheckedChanged);
            // 
            // gbxSettingFraInt
            // 
            this.gbxSettingFraInt.Controls.Add(this.trbAmp);
            this.gbxSettingFraInt.Controls.Add(this.trbOffset);
            this.gbxSettingFraInt.Controls.Add(this.txtWaitCycle);
            this.gbxSettingFraInt.Controls.Add(this.label2);
            this.gbxSettingFraInt.Controls.Add(this.txtAmp);
            this.gbxSettingFraInt.Controls.Add(this.txtOffset);
            this.gbxSettingFraInt.Controls.Add(this.btnAmp);
            this.gbxSettingFraInt.Controls.Add(this.btnOffset);
            this.gbxSettingFraInt.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbxSettingFraInt.Location = new System.Drawing.Point(7, 7);
            this.gbxSettingFraInt.Name = "gbxSettingFraInt";
            this.gbxSettingFraInt.Size = new System.Drawing.Size(337, 134);
            this.gbxSettingFraInt.TabIndex = 0;
            this.gbxSettingFraInt.TabStop = false;
            this.gbxSettingFraInt.Text = "Drive I/P";
            // 
            // trbAmp
            // 
            this.trbAmp.Location = new System.Drawing.Point(203, 65);
            this.trbAmp.Name = "trbAmp";
            this.trbAmp.Size = new System.Drawing.Size(125, 45);
            this.trbAmp.TabIndex = 9;
            // 
            // trbOffset
            // 
            this.trbOffset.Location = new System.Drawing.Point(203, 14);
            this.trbOffset.Name = "trbOffset";
            this.trbOffset.Size = new System.Drawing.Size(125, 45);
            this.trbOffset.TabIndex = 8;
            // 
            // txtWaitCycle
            // 
            this.txtWaitCycle.Location = new System.Drawing.Point(90, 100);
            this.txtWaitCycle.Name = "txtWaitCycle";
            this.txtWaitCycle.Size = new System.Drawing.Size(100, 20);
            this.txtWaitCycle.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Wait cycle";
            // 
            // txtAmp
            // 
            this.txtAmp.Location = new System.Drawing.Point(90, 19);
            this.txtAmp.Name = "txtAmp";
            this.txtAmp.Size = new System.Drawing.Size(100, 20);
            this.txtAmp.TabIndex = 3;
            this.txtAmp.Text = "60";
            // 
            // txtOffset
            // 
            this.txtOffset.Location = new System.Drawing.Point(90, 63);
            this.txtOffset.Name = "txtOffset";
            this.txtOffset.Size = new System.Drawing.Size(100, 20);
            this.txtOffset.TabIndex = 2;
            // 
            // btnAmp
            // 
            this.btnAmp.Location = new System.Drawing.Point(8, 16);
            this.btnAmp.Name = "btnAmp";
            this.btnAmp.Size = new System.Drawing.Size(75, 23);
            this.btnAmp.TabIndex = 1;
            this.btnAmp.Text = "Amp";
            this.btnAmp.UseVisualStyleBackColor = true;
            // 
            // btnOffset
            // 
            this.btnOffset.Location = new System.Drawing.Point(9, 60);
            this.btnOffset.Name = "btnOffset";
            this.btnOffset.Size = new System.Drawing.Size(75, 23);
            this.btnOffset.TabIndex = 0;
            this.btnOffset.Text = "Offset";
            this.btnOffset.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkSavedata);
            this.groupBox3.Controls.Add(this.txtSaveData);
            this.groupBox3.Location = new System.Drawing.Point(7, 148);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(200, 75);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Storage";
            // 
            // chkSavedata
            // 
            this.chkSavedata.AutoSize = true;
            this.chkSavedata.Location = new System.Drawing.Point(8, 19);
            this.chkSavedata.Name = "chkSavedata";
            this.chkSavedata.Size = new System.Drawing.Size(82, 17);
            this.chkSavedata.TabIndex = 6;
            this.chkSavedata.Text = "save data";
            this.chkSavedata.UseVisualStyleBackColor = true;
            this.chkSavedata.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // txtSaveData
            // 
            this.txtSaveData.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSaveData.Location = new System.Drawing.Point(96, 14);
            this.txtSaveData.Multiline = true;
            this.txtSaveData.Name = "txtSaveData";
            this.txtSaveData.Size = new System.Drawing.Size(94, 33);
            this.txtSaveData.TabIndex = 7;
            // 
            // chart1
            // 
            chartArea1.BorderColor = System.Drawing.Color.White;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.DockedToChartArea = "ChartArea1";
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(581, 229);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.LabelForeColor = System.Drawing.Color.Chartreuse;
            series1.Legend = "Legend1";
            series1.Name = "Phase";
            series1.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series2.Legend = "Legend1";
            series2.Name = "Gain";
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(701, 328);
            this.chart1.TabIndex = 8;
            this.chart1.Text = "chart1";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.CausesValidation = false;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.clmMax,
            this.cluValue,
            this.clmUnit});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(11, 3);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(545, 173);
            this.dataGridView1.TabIndex = 9;
            // 
            // Column1
            // 
            this.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column1.HeaderText = "Item name";
            this.Column1.Name = "Column1";
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Min";
            this.Column2.Name = "Column2";
            // 
            // clmMax
            // 
            this.clmMax.HeaderText = "Max";
            this.clmMax.Name = "clmMax";
            // 
            // cluValue
            // 
            this.cluValue.HeaderText = "Value";
            this.cluValue.Name = "cluValue";
            // 
            // clmUnit
            // 
            this.clmUnit.HeaderText = "Unit";
            this.clmUnit.Name = "clmUnit";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dataGridView1);
            this.panel1.Location = new System.Drawing.Point(12, 14);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(559, 189);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnSerialsBegin);
            this.panel2.Controls.Add(this.btnNumbValue);
            this.panel2.Controls.Add(this.btnLineNumbValue);
            this.panel2.Controls.Add(this.btnDateValue);
            this.panel2.Controls.Add(this.btnBeginChart);
            this.panel2.Controls.Add(this.btnNumb);
            this.panel2.Controls.Add(this.btnLineNumb);
            this.panel2.Controls.Add(this.btnDate);
            this.panel2.Controls.Add(this.btnTypeValue);
            this.panel2.Controls.Add(this.btnTypeName);
            this.panel2.Controls.Add(this.lblResult);
            this.panel2.Controls.Add(this.btnSpec);
            this.panel2.Controls.Add(this.btnModel);
            this.panel2.Location = new System.Drawing.Point(581, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(703, 210);
            this.panel2.TabIndex = 12;
            // 
            // btnSerialsBegin
            // 
            this.btnSerialsBegin.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btnSerialsBegin.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSerialsBegin.Location = new System.Drawing.Point(197, 142);
            this.btnSerialsBegin.Name = "btnSerialsBegin";
            this.btnSerialsBegin.Size = new System.Drawing.Size(177, 65);
            this.btnSerialsBegin.TabIndex = 12;
            this.btnSerialsBegin.Text = "开始";
            this.btnSerialsBegin.UseVisualStyleBackColor = false;
            this.btnSerialsBegin.Click += new System.EventHandler(this.btnSerialsBegin_Click);
            // 
            // btnNumbValue
            // 
            this.btnNumbValue.Location = new System.Drawing.Point(468, 101);
            this.btnNumbValue.Name = "btnNumbValue";
            this.btnNumbValue.Size = new System.Drawing.Size(228, 34);
            this.btnNumbValue.TabIndex = 11;
            this.btnNumbValue.UseVisualStyleBackColor = true;
            this.btnNumbValue.Click += new System.EventHandler(this.btnNumbValue_Click);
            // 
            // btnLineNumbValue
            // 
            this.btnLineNumbValue.Location = new System.Drawing.Point(468, 67);
            this.btnLineNumbValue.Name = "btnLineNumbValue";
            this.btnLineNumbValue.Size = new System.Drawing.Size(228, 34);
            this.btnLineNumbValue.TabIndex = 10;
            this.btnLineNumbValue.UseVisualStyleBackColor = true;
            // 
            // btnDateValue
            // 
            this.btnDateValue.Location = new System.Drawing.Point(468, 34);
            this.btnDateValue.Name = "btnDateValue";
            this.btnDateValue.Size = new System.Drawing.Size(228, 34);
            this.btnDateValue.TabIndex = 9;
            this.btnDateValue.UseVisualStyleBackColor = true;
            // 
            // btnBeginChart
            // 
            this.btnBeginChart.BackColor = System.Drawing.Color.Aquamarine;
            this.btnBeginChart.Font = new System.Drawing.Font("Microsoft Sans Serif", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBeginChart.Location = new System.Drawing.Point(380, 142);
            this.btnBeginChart.Name = "btnBeginChart";
            this.btnBeginChart.Size = new System.Drawing.Size(316, 65);
            this.btnBeginChart.TabIndex = 8;
            this.btnBeginChart.Text = "画图";
            this.btnBeginChart.UseVisualStyleBackColor = false;
            this.btnBeginChart.Click += new System.EventHandler(this.btnBegin_Click);
            // 
            // btnNumb
            // 
            this.btnNumb.Location = new System.Drawing.Point(195, 101);
            this.btnNumb.Name = "btnNumb";
            this.btnNumb.Size = new System.Drawing.Size(267, 34);
            this.btnNumb.TabIndex = 7;
            this.btnNumb.Text = "流水号";
            this.btnNumb.UseVisualStyleBackColor = true;
            // 
            // btnLineNumb
            // 
            this.btnLineNumb.Location = new System.Drawing.Point(195, 67);
            this.btnLineNumb.Name = "btnLineNumb";
            this.btnLineNumb.Size = new System.Drawing.Size(267, 34);
            this.btnLineNumb.TabIndex = 6;
            this.btnLineNumb.Text = "线别";
            this.btnLineNumb.UseVisualStyleBackColor = true;
            // 
            // btnDate
            // 
            this.btnDate.Location = new System.Drawing.Point(195, 34);
            this.btnDate.Name = "btnDate";
            this.btnDate.Size = new System.Drawing.Size(267, 34);
            this.btnDate.TabIndex = 5;
            this.btnDate.Text = "年月日";
            this.btnDate.UseVisualStyleBackColor = true;
            // 
            // btnTypeValue
            // 
            this.btnTypeValue.BackColor = System.Drawing.Color.Transparent;
            this.btnTypeValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTypeValue.Location = new System.Drawing.Point(468, -1);
            this.btnTypeValue.Name = "btnTypeValue";
            this.btnTypeValue.Size = new System.Drawing.Size(228, 34);
            this.btnTypeValue.TabIndex = 4;
            this.btnTypeValue.UseVisualStyleBackColor = false;
            // 
            // btnTypeName
            // 
            this.btnTypeName.Location = new System.Drawing.Point(195, 1);
            this.btnTypeName.Name = "btnTypeName";
            this.btnTypeName.Size = new System.Drawing.Size(267, 34);
            this.btnTypeName.TabIndex = 3;
            this.btnTypeName.Text = "机种名称";
            this.btnTypeName.UseVisualStyleBackColor = true;
            // 
            // lblResult
            // 
            this.lblResult.BackColor = System.Drawing.Color.Green;
            this.lblResult.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.lblResult.Location = new System.Drawing.Point(3, 142);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(187, 74);
            this.lblResult.TabIndex = 2;
            this.lblResult.Text = "PASS";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnSpec
            // 
            this.btnSpec.BackColor = System.Drawing.Color.Blue;
            this.btnSpec.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSpec.ForeColor = System.Drawing.Color.Yellow;
            this.btnSpec.Location = new System.Drawing.Point(0, 67);
            this.btnSpec.Name = "btnSpec";
            this.btnSpec.Size = new System.Drawing.Size(192, 69);
            this.btnSpec.TabIndex = 1;
            this.btnSpec.Text = "Spec Ini";
            this.btnSpec.UseVisualStyleBackColor = false;
            // 
            // btnModel
            // 
            this.btnModel.BackColor = System.Drawing.Color.Blue;
            this.btnModel.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModel.ForeColor = System.Drawing.Color.Yellow;
            this.btnModel.Location = new System.Drawing.Point(0, 0);
            this.btnModel.Name = "btnModel";
            this.btnModel.Size = new System.Drawing.Size(193, 65);
            this.btnModel.TabIndex = 0;
            this.btnModel.Text = "Model";
            this.btnModel.UseVisualStyleBackColor = false;
            // 
            // lbtext
            // 
            this.lbtext.AutoSize = true;
            this.lbtext.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbtext.Location = new System.Drawing.Point(602, 532);
            this.lbtext.Name = "lbtext";
            this.lbtext.Size = new System.Drawing.Size(0, 42);
            this.lbtext.TabIndex = 14;
            // 
            // serialPort1
            // 
            this.serialPort1.PortName = "COM4";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1300, 566);
            this.Controls.Add(this.lbtext);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.SETTING);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Demo";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SETTING.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbxLoopTransferFunction.ResumeLayout(false);
            this.gbxLoopTransferFunction.PerformLayout();
            this.gbxActuatorFrequency.ResumeLayout(false);
            this.gbxActuatorFrequency.PerformLayout();
            this.gbxSettingFraInt.ResumeLayout(false);
            this.gbxSettingFraInt.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbAmp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trbOffset)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl SETTING;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox gbxLoopTransferFunction;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox gbxActuatorFrequency;
        private System.Windows.Forms.GroupBox gbxSettingFraInt;
        private System.Windows.Forms.TrackBar trbAmp;
        private System.Windows.Forms.TrackBar trbOffset;
        private System.Windows.Forms.TextBox txtSaveData;
        private System.Windows.Forms.CheckBox chkSavedata;
        private System.Windows.Forms.TextBox txtWaitCycle;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmp;
        private System.Windows.Forms.TextBox txtOffset;
        private System.Windows.Forms.Button btnAmp;
        private System.Windows.Forms.Button btnOffset;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Button btnSpec;
        private System.Windows.Forms.Button btnModel;
        private System.Windows.Forms.Button btnTypeValue;
        private System.Windows.Forms.Button btnTypeName;
        private System.Windows.Forms.Button btnNumbValue;
        private System.Windows.Forms.Button btnLineNumbValue;
        private System.Windows.Forms.Button btnDateValue;
        private System.Windows.Forms.Button btnBeginChart;
        private System.Windows.Forms.Button btnNumb;
        private System.Windows.Forms.Button btnLineNumb;
        private System.Windows.Forms.Button btnDate;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtCheckslvAddres;
        private System.Windows.Forms.Button btnIDvalue;
        private System.Windows.Forms.Button btnCheckslvAddres;
        private System.Windows.Forms.Button btSetCOMport;
        private System.Windows.Forms.ComboBox combxIDvalue;
        private System.Windows.Forms.Label lbtext;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ComboBox cmbSerials;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.TextBox txtStartFreq;
        private System.Windows.Forms.TextBox txtEndFreq;
        private System.Windows.Forms.Label lblStartFreq;
        private System.Windows.Forms.Label lblEndFreq;
        private System.Windows.Forms.CheckBox chk_ch1;
        private System.Windows.Forms.Button btnSerialsBegin;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.CheckBox chk_ch2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMax;
        private System.Windows.Forms.DataGridViewTextBoxColumn cluValue;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmUnit;
        private System.Windows.Forms.CheckBox chk_ch3;
    }
}

