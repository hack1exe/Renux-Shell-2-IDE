using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSHELL_IDE
{

    public partial class prp : Form
    {
        string RPC;
        public prp()
        {
            InitializeComponent();
            try
            {
                string[] files = Directory.GetFiles(Environment.GetEnvironmentVariable("appdata") + "\\by.strdev\\locales", "*.locale");
                foreach (string i in files)
                {
                    comboBox1.Items.Add(Path.GetFileName(i));
                }
                locInit();
            }
            catch { }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            RegistryKey currentUserKey = Registry.CurrentUser;
            RegistryKey helloKey = currentUserKey.CreateSubKey("RenuxShellIDE\\RPC");
            helloKey.SetValue("enabled", checkBox1.Checked.ToString(), RegistryValueKind.String);

            helloKey.Close();
        }

        private void prp_Load(object sender, EventArgs e)
        {
            AutocompleteInit();
            RegistryKey reg = Registry.CurrentUser.OpenSubKey("RenuxShellIDE\\RPC");
            if (reg != null)
            {

                RegistryKey currentUserKey = Registry.CurrentUser;
                RegistryKey helloKey = currentUserKey.OpenSubKey("RenuxShellIDE\\RPC");
                RPC = (string)helloKey.GetValue("enabled");
                if(RPC == "True")
                {
                    checkBox1.Checked = true;
                }else if(RPC == "False")
                {
                    checkBox1.Checked = false;
                }

            }
        }
        void locInit()
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser.CreateSubKey("RenuxShellIDE\\settings\\");
                var val = rk.GetValue("currentLocale");
                if(val == null)
                {
                    comboBox1.SelectedItem = "russian.locale";
                }
                else
                {
                    comboBox1.SelectedItem = val;
                }
                

            }
            catch
            {

            }
        }
        void AutocompleteInit()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("RenuxShellIDE\\settings\\");
                
                string value = (string)reg.GetValue("AutocompleteInterval");

                int v = Convert.ToInt32(value);
                trackBar1.Value = v;
                label3.Text = trackBar1.Value.ToString();
            }
            catch
            {

            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Process install = new Process();
                install.StartInfo.FileName = Application.ExecutablePath;
                install.StartInfo.Arguments = "/contextadd";
                install.StartInfo.Verb = "runas";
                install.Start();
                install.WaitForExit();
                




            }
            catch
            {

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Process install = new Process();
                install.StartInfo.FileName = Application.ExecutablePath;
                install.StartInfo.Arguments = "/contextdel";
                install.StartInfo.Verb = "runas";
                install.Start();
                install.WaitForExit();


            }
            catch
            {

            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            label3.Text = trackBar1.Value.ToString();
            try
            {
                RegistryKey rk = Registry.CurrentUser.CreateSubKey("RenuxShellIDE\\settings\\");
                rk.SetValue("AutocompleteInterval", trackBar1.Value.ToString(), RegistryValueKind.String);
                Form1 frm = (Form1)Application.OpenForms["Form1"];
                frm.autocompleteMenu1.MinFragmentLength = trackBar1.Value;
                
            }
            catch { }

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                RegistryKey rk = Registry.CurrentUser.CreateSubKey("RenuxShellIDE\\settings\\");
                rk.SetValue("currentLocale", comboBox1.SelectedItem, RegistryValueKind.String);
                Form1 frm = (Form1)Application.OpenForms["Form1"];
                frm.SetLocale(comboBox1.SelectedItem.ToString());
            }
            catch
            {

            }

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
