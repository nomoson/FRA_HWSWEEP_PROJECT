using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FRA_INFO
{
    class Arduino
    {
        private static SerialPort port = null;
        static string[] portName = SerialPort.GetPortNames();
        static string portName2 = "";

        public static string GetPortName()
        {
           
            for (int i = 0; i < portName.Length; i++)
            {
                portName2 += portName[i].ToString();
                Debug.WriteLine("=======portName2=======" + portName2);
            }
            return portName2;
        }

        /// <summary>
        /// 初始化串口实例
        /// </summary>
        public static void InitialSerialPort()
        {
            try
            {
                if (port == null)
                {
                   
                    port = new SerialPort("COM3", 9600);
                    //Debug.WriteLine("=======port=======" + port);
                    port.Encoding = Encoding.ASCII;
                    // port.DataReceived += port_DataReceived;
                    port.Open();
                    ChangeArduinoSendStatus(true);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("初始化串口发生错误：" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 关闭并销毁串口实例
        /// </summary>
        public static void DisposeSerialPort()
        {
            if (port != null)
            {
                try
                {
                    ChangeArduinoSendStatus(false);
                    if (port.IsOpen)
                    {
                        port.Close();
                    }
                    port.Dispose();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("关闭串口发生错误：" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        /// <summary>
        /// 改变Arduino串口的发送状态
        /// </summary>
        /// <param name="allowSend">是否允许发送数据</param>
        public static void ChangeArduinoSendStatus(bool allowSend)
        {
            if (port != null && port.IsOpen)
            {
                if (allowSend)
                {
                    port.WriteLine("serial start");
                }
                else
                {
                    port.WriteLine("serial stop");
                }
            }
        }

        /// <summary>
        /// 从串口读取数据并转换为字符串形式
        /// </summary>
        /// <returns></returns>
        public static string ReadSerialData()
        {
            string value = "";
            try
            {
                if (port != null && port.BytesToRead > 0)
                {
                    value = port.ReadExisting();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("读取串口数据发生错误：" + ex.Message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return value;
        }

        //向串口输出命令字符  
        public static void PortWrite(string message)
        {
            if (port != null && port.IsOpen)
            {
                port.Write(message);
                //port.WriteLine(message);  
            }
        }



        /// <summary>
        /// 在读取到数据时刷新文本框的信息
        /// </summary>
        //private void RefreshInfoTextBox()
        //{
        //    string value = this.ReadSerialData();
        //    Action<string> setValueAction = text => this.txtInfo.Text += text;

        //    if (this.txtInfo.InvokeRequired)
        //    {
        //        this.txtInfo.Invoke(setValueAction, value);
        //    }
        //    else
        //    {
        //        setValueAction(value);
        //    }
        //}

    }
}
