﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace dz4_1
{
    public partial class Form2 : Form
    {
        Form1 form;
        public Form2(Form1 form)
        {
            InitializeComponent();
            this.form = form;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                form.changeFontColor(colorDialog1.Color);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                form.changeBackgroundColor(colorDialog1.Color);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            fontDialog1.ShowColor = true;

            fontDialog1.Font = this.form.getTextBox().Font;
            fontDialog1.Color = this.form.getTextBox().ForeColor;

            if (fontDialog1.ShowDialog() != DialogResult.Cancel)
            {
                this.form.getTextBox().Font = fontDialog1.Font;
                this.form.getTextBox().ForeColor = fontDialog1.Color;
            }
        }
    }
}
