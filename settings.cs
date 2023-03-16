using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSHELL_IDE
{
    public partial class settings : Form
    {
        string BackR;
        string BackG;
        string BackB;
        string TextR;
        string TextG;
        string TextB;


        public settings()
        {
            InitializeComponent();
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {

                fastColoredTextBox1.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
                BackR = trackBar1.Value.ToString();
                BackG = trackBar2.Value.ToString();
                BackB = trackBar3.Value.ToString();
          

            
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {

            fastColoredTextBox1.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            BackR = trackBar1.Value.ToString();
            BackG = trackBar2.Value.ToString();
            BackB = trackBar3.Value.ToString();
        }

        private void trackBar3_Scroll(object sender, EventArgs e)
        {

            fastColoredTextBox1.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            BackR = trackBar1.Value.ToString();
            BackG = trackBar2.Value.ToString();
            BackB = trackBar3.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = (Form1)Application.OpenForms["Form1"];
            frm.fastColoredTextBox1.BackColor = Color.FromArgb(trackBar1.Value, trackBar2.Value, trackBar3.Value);
            frm.fastColoredTextBox1.ForeColor = Color.FromArgb(trackBar4.Value, trackBar5.Value, trackBar6.Value);
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("RenuxShellIDE\\colors");
            helloKey.SetValue("BackR", trackBar1.Value, RegistryValueKind.String);
            helloKey.SetValue("BackG", trackBar2.Value, RegistryValueKind.String);
            helloKey.SetValue("BackB", trackBar3.Value, RegistryValueKind.String);
            helloKey.SetValue("TextR", trackBar4.Value, RegistryValueKind.String);
            helloKey.SetValue("TextG", trackBar5.Value, RegistryValueKind.String);
            helloKey.SetValue("TextB", trackBar6.Value, RegistryValueKind.String);
            helloKey.Close();
        }

        private void settings_Load(object sender, EventArgs e)
        {

            Form1 frm = (Form1)Application.OpenForms["Form1"];
            this.fastColoredTextBox1.BackColor = frm.fastColoredTextBox1.BackColor;
            this.fastColoredTextBox1.ForeColor = frm.fastColoredTextBox1.ForeColor;
            trackBar1.Value = frm.fastColoredTextBox1.BackColor.R;
            trackBar2.Value = frm.fastColoredTextBox1.BackColor.G;
            trackBar3.Value = frm.fastColoredTextBox1.BackColor.B;
            trackBar4.Value = frm.fastColoredTextBox1.ForeColor.R;
            trackBar5.Value = frm.fastColoredTextBox1.ForeColor.G;
            trackBar6.Value = frm.fastColoredTextBox1.ForeColor.B;

        }

        private void trackBar4_Scroll(object sender, EventArgs e)
        {

            fastColoredTextBox1.ForeColor = Color.FromArgb(trackBar4.Value, trackBar5.Value, trackBar6.Value);
            TextR = trackBar4.Value.ToString();
            TextG = trackBar5.Value.ToString();
            TextB = trackBar6.Value.ToString();
        }

        private void trackBar5_Scroll(object sender, EventArgs e)
        {
            fastColoredTextBox1.ForeColor = Color.FromArgb(trackBar4.Value, trackBar5.Value, trackBar6.Value);
            TextR = trackBar4.Value.ToString();
            TextG = trackBar5.Value.ToString();
            TextB = trackBar6.Value.ToString();
        }

        private void trackBar6_Scroll(object sender, EventArgs e)
        {
            fastColoredTextBox1.ForeColor = Color.FromArgb(trackBar4.Value, trackBar5.Value, trackBar6.Value);
            TextR = trackBar4.Value.ToString();
            TextG = trackBar5.Value.ToString();
            TextB = trackBar6.Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.fastColoredTextBox1.BackColor = Color.White;
            this.fastColoredTextBox1.ForeColor = Color.Black;
            trackBar1.Value = 255;
            trackBar2.Value = 255;
            trackBar3.Value = 255;
            trackBar4.Value = 0;
            trackBar5.Value = 0;
            trackBar6.Value = 0;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.fastColoredTextBox1.BackColor = Color.FromArgb(0, 0, 27);
            this.fastColoredTextBox1.ForeColor = Color.FromArgb(150, 255, 230);
            trackBar1.Value = 0;
            trackBar2.Value = 0;
            trackBar3.Value = 27;
            trackBar4.Value = 150;
            trackBar5.Value = 255;
            trackBar6.Value = 230;

        }
    }
}
