﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FRA_INFO
{
    public class MyProgressBar: ProgressBar
    {
        public MyProgressBar()
        {
            base.SetStyle(ControlStyles.UserPaint, true);
        }

        //重写OnPaint方法
        protected override void OnPaint(PaintEventArgs e)
        {
            SolidBrush brush = null;
            Rectangle bounds = new Rectangle(0, 0, base.Width, base.Height);
            bounds.Height -= 4;
            bounds.Width = ((int)(bounds.Width * (((double)base.Value) / ((double)base.Maximum)))) - 4;
            brush = new SolidBrush(Color.Coral);
            e.Graphics.FillRectangle(brush, 2, 2, bounds.Width, bounds.Height);
       
            //base.OnPaint(e);
        }
    }
}
