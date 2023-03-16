using DiscordRPC;
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
    public partial class Form1 : Form
    {
        string RsIdeVersion = "v" + Application.ProductVersion.Replace("0.", "");
        string PathToFile = "";
        string RPC;
        public Form1(string param)
        {
            InitializeComponent();
            this.KeyPreview = true;
            try
            {

                RegistryKey reg = Registry.CurrentUser.CreateSubKey("RenuxShellIDE\\settings\\", true);



                var value = reg.GetValue("currentLocale");
                if(value == null)
                {
                    SetLocale("russian.locale");
                }
                else
                {
                    SetLocale(value.ToString());
                }

            }
            catch
            {

            }



            this.Text = this.Text + " " + RsIdeVersion;
            if (param == "")
            {

            }
            else
            {

                var fileContent = string.Empty;
                var filePath = string.Empty;





                try
                {
                    using (StreamReader reader = new StreamReader(param, System.Text.Encoding.GetEncoding(1251)))
                    {
                        fastColoredTextBox1.Text = reader.ReadToEnd();
                        PathToFile = param;


                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Не удалось прочитать файл сценария. {ex.Message}");
                }
            }           
                    
                
            
        }

        private void button1_Click(object sender, EventArgs e)
        {

            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void saveFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void fastColoredTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ColorInit();
            DiscordInit();
            AutocompleteInit();
        }
        public void SetLocale(string locale)
        {
            try
            {
                
                string[] local = { };
                autocompleteMenu1.Items = local;
                string[] data = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\locales\\" + locale, Encoding.GetEncoding(1251));
                foreach (string d in data)
                {
                    if (d.Contains("RSHELL_VARNAME_") || d.Contains("RSHELL_COMMAND_NAME_"))
                    {
                        string[] parsed = d.Split(new char[] { '=' });
                        autocompleteMenu1.AddItem(parsed[1]);


                    }
                }

            }
            catch (Exception ex) { MessageBox.Show($"Невозможно загрузить файлы локализации. Программа будет запущена без подсказок кода.\n {ex.Message}", "Renux Shell IDE", MessageBoxButtons.OK, MessageBoxIcon.Error); }

        }
        void AutocompleteInit()
        {
            
            try
            {

                RegistryKey reg = Registry.CurrentUser.CreateSubKey("RenuxShellIDE\\settings\\", true);
               
                

                    var value = reg.GetValue("AutocompleteInterval");
                    int v = Convert.ToInt32(value);
                    autocompleteMenu1.MinFragmentLength = v;
                
                
             
                
                

                
            }
            catch
            {

            }
        }

        private void DiscordInit()
        {
            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("RenuxShellIDE\\RPC");
                if (reg != null)
                {

                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey helloKey = currentUserKey.OpenSubKey("RenuxShellIDE\\RPC");
                    RPC = (string)helloKey.GetValue("enabled");

                    if (RPC == "True")
                    {
                        try
                        {
                            DiscordRpcClient rpc = new DiscordRpcClient("#placeholder#");
                            rpc.Initialize();
                            rpc.SetPresence(new RichPresence()
                            {
                                Details = "RShell IDE " + RsIdeVersion,
                                State = "itstd.org, Все права защищены",

                                Assets = new Assets()
                                {
                                    LargeImageKey = "renux",
                                    LargeImageText = "itstd.org",
                                    SmallImageKey = "renux",





                                }
                            });
                        }
                        catch
                        {
                            MessageBox.Show("Не удалось запустить функцию активного статуса в Discord");
                        }

                    }
                    else 
                    {
                        
                    }

                    
                }










            }
            catch
            {

            }
        }

        public void ColorInit()
        {
            string clrR;
            string clrG;
            string clrB;
            string TR;
            string TG;
            string TB;

            try
            {
                RegistryKey reg = Registry.CurrentUser.OpenSubKey("RenuxShellIDE\\colors");
                if (reg != null)
                {

                    RegistryKey currentUserKey = Registry.CurrentUser;
                    RegistryKey helloKey = currentUserKey.OpenSubKey("RenuxShellIDE\\colors");
                    clrR = (string)helloKey.GetValue("BackR");
                    clrG = (string)helloKey.GetValue("BackG");
                    clrB = (string)helloKey.GetValue("BackB");

                    int R = Convert.ToInt32(clrR);
                    int G = Convert.ToInt32(clrG);
                    int B = Convert.ToInt32(clrB);
                    fastColoredTextBox1.BackColor = Color.FromArgb(R, G, B);
                    TR = (string)helloKey.GetValue("TextR");
                    TG = (string)helloKey.GetValue("TextG");
                    TB = (string)helloKey.GetValue("TextB");

                    int AR = Convert.ToInt32(TR);
                    int AG = Convert.ToInt32(TG);
                    int AB = Convert.ToInt32(TB);
                    fastColoredTextBox1.ForeColor = Color.FromArgb(AR, AG, AB);




                    // string clrR = helloKey.GetValue("ColorR").ToString();
                    // string clrG = helloKey.GetValue("ColorG").ToString();
                    //string clrB = helloKey.GetValue("ColorB").ToString();
                }
                else
                {

                }
                //   RegistryKey currentUserKey = Registry.CurrentUser;
                //   RegistryKey helloKey = currentUserKey.OpenSubKey("RenuxShellIDE");

                ///  string clrR = helloKey.GetValue("ColorR").ToString();
                ///  string clrG = helloKey.GetValue("ColorG").ToString();
                ///  string clrB = helloKey.GetValue("ColorB").ToString();

                //   clrR = (string)helloKey.GetValue("Password");

                //    helloKey.Close();
                //     int R = Convert.ToInt32(clrR);
                // int G = Convert.ToInt32(clrG);
                //  int B = Convert.ToInt32(clrB);
                // fastColoredTextBox1.BackColor = Color.FromArgb(R, G, B);
            }
            catch { }


        }

        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            OpenFile();
        }
        public void OpenFile()
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                // openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Renux Shell Script (*.rs)|*.rs|TXT (*.txt)|*.txt";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();
                    try
                    {
                        using (StreamReader reader = new StreamReader(fileStream, System.Text.Encoding.GetEncoding(1251)))
                        {
                            fastColoredTextBox1.Text = reader.ReadToEnd();
                            PathToFile = openFileDialog.FileName;


                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось прочитать файл сценария. {ex.Message}");
                    }

                }
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe"))
            {

                StreamWriter SW;
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.FileName = fastColoredTextBox1.Text;
                SFD.FileName = "NewScript";
                SFD.Filter = "Renux Shell Script (*.rs)|*.rs|TXT (*.txt)|*.txt";
                if (label1.Visible == false)
                {
                    if (SFD.ShowDialog() == DialogResult.OK)
                    {

                        SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                        SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                        SW.Close();
                        label1.Text = SFD.FileName;
                    }
                    SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                    SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                    SW.Close();

                }
            }
            else
            {
                MessageBox.Show("Внимание! На вашем ПК не установлена консоль rshell. Запустите установщик консоли и выполните команду 'консоль установить'.");
            }

        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            StreamWriter SW;
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.FileName = fastColoredTextBox1.Text;
            SFD.FileName = "NewScript";
            SFD.Filter = "Renux Shell Script (*.rs)|*.rs|TXT (*.txt)|*.txt";
            if (label1.Visible == false)
            {
                if (SFD.ShowDialog() == DialogResult.OK)
                {

                    SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                    SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                    SW.Close();
                    label1.Visible = true;
                    label1.Text = SFD.FileName;
                    Process.Start(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe", SFD.FileName);
                }
            }
            else
            {
                SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                SW.Close();
                Process.Start(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe", SFD.FileName);
            }

            

        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
           

            
        }

        private void toolStripButton3_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            SaveFile();

        } 
        public void SaveFile()
        {
            StreamWriter SW;
            SaveFileDialog SFD = new SaveFileDialog();
            SFD.FileName = fastColoredTextBox1.Text;
            SFD.FileName = "NewScript";
            SFD.Filter = "Renux Shell Script (*.rs)|*.rs|TXT (*.txt)|*.txt";
            if (PathToFile == "")
            {
                if (SFD.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                        SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                        SW.Close();
                        PathToFile = SFD.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Не удалось произвести запись в файл. {ex.Message}");
                    }

                }



            }
            else
            {
                SW = new StreamWriter(PathToFile, false, System.Text.Encoding.GetEncoding(1251));
                SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                SW.Close();
            }

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            RunScript();
            
        }
        public void RunScript()
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe"))
            {
                StreamWriter SW;
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.FileName = fastColoredTextBox1.Text;
                SFD.FileName = "NewScript";
                SFD.Filter = "Renux Shell Script (*.rs)|*.rs|TXT (*.txt)|*.txt";
                if (PathToFile == "")
                {
                    if (SFD.ShowDialog() == DialogResult.OK)
                    {

                        SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                        SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                        SW.Close();
                        PathToFile = SFD.FileName;
                        Process.Start(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe", SFD.FileName);
                    }
                }
                else
                {
                    SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                    SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                    SW.Close();
                    Process.Start(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe", SFD.FileName);
                }


            }
            else
            {
                MessageBox.Show("Внимание! На вашем ПК не установлена консоль rshell. Запустите установщик консоли и выполните команду 'консоль установить'.");
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form2 compiler = new Form2(PathToFile);
            compiler.ShowDialog();
        }

        private void toolStripButton3_Click_2(object sender, EventArgs e)
        {
            About about = new About();
            about.ShowDialog();
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe"))
            {
                StreamWriter SW;
                SaveFileDialog SFD = new SaveFileDialog();
                SFD.FileName = fastColoredTextBox1.Text;
                SFD.FileName = "NewScript";
                SFD.Filter = "Renux Shell Script (*.rs)|*.rs|TXT (*.txt)|*.txt";
                if (PathToFile == "")
                {
                    if (SFD.ShowDialog() == DialogResult.OK)
                    {

                        SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                        SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                        SW.Close();
                        PathToFile = SFD.FileName;
                        Process.Start(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe", SFD.FileName + " /debug");
                    }
                }
                else
                {
                    SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                    SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                    SW.Close();
                    Process.Start(@Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe", SFD.FileName + " /debug");
                }


            }
            else
            {
                MessageBox.Show("Внимание! На вашем ПК не установлена консоль rshell. Запустите установщик консоли и выполните команду 'консоль установить'.");
            }
           
        }

        private void toolStripButton6_Click(object sender, EventArgs e)
        {

                report report = new report();
                report.ShowDialog();

            

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            //settings settings = new settings();
            
          //  settings.Show();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe"))
            {
                Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe");
            }
            else
            {
                MessageBox.Show("Внимание! На вашем ПК не установлена консоль rshell. Запустите установщик консоли и выполните команду 'консоль установить'.");
            }
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe"))
            {
                //Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe");
                try
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe";
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.Verb = "runas";
                    proc.Start();
                }
                catch { }

            }
            else
            {
                MessageBox.Show("Внимание! На вашем ПК не установлена консоль rshell. Запустите установщик консоли и выполните команду 'консоль установить'.");
            }
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe"))
                {
                    StreamWriter SW;
                    SaveFileDialog SFD = new SaveFileDialog();
                    SFD.FileName = fastColoredTextBox1.Text;
                    SFD.FileName = "NewScript";
                    SFD.Filter = "Renux Shell Script (*.rs)|*.rs|TXT (*.txt)|*.txt";
                    if (PathToFile == "")
                    {
                        if (SFD.ShowDialog() == DialogResult.OK)
                        {

                            SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                            SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                            SW.Close();
                            PathToFile = SFD.FileName;
                            Process proc = new Process();
                            proc.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe";
                            proc.StartInfo.Arguments = SFD.FileName;
                            proc.StartInfo.UseShellExecute = true;
                            proc.StartInfo.Verb = "runas";
                            proc.Start();
                        }
                    }
                    else
                    {
                        SW = new StreamWriter(SFD.FileName, false, System.Text.Encoding.GetEncoding(1251));
                        SW.Write(fastColoredTextBox1.Text.ToString(), Encoding.Default);
                        SW.Close();
                        Process proc = new Process();
                        proc.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe";
                        proc.StartInfo.Arguments = SFD.FileName;
                        proc.StartInfo.UseShellExecute = true;
                        proc.StartInfo.Verb = "runas";
                        proc.Start();
                    }


                }
                else
                {
                    MessageBox.Show("Внимание! На вашем ПК не установлена консоль rshell. Запустите установщик консоли и выполните команду 'консоль установить'.");
                }

            }
            catch { }

        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            settings settings = new settings();
            settings.ShowDialog();
        }

        private void toolStripButton1_Click_2(object sender, EventArgs e)
        {
            settings settings = new settings();
            settings.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if(new Form2(PathToFile).ShowDialog() == DialogResult.OK)
            {

            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
         
        }

        private void toolStripComboBox1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton3_Click(object sender, EventArgs e)
        {

        }

        private void toolStripButton2_Click_2(object sender, EventArgs e)
        {
 
        }

        private void toolStripButton2_Click_3(object sender, EventArgs e)
        {
            prp prp = new prp();
            prp.ShowDialog();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //base.OnKeyDown(e);
            if (e.KeyCode == Keys.O && e.Control)
            {
                OpenFile();
                e.Handled = true;
            }else if(e.KeyCode == Keys.S && e.Control)
            {
                SaveFile();
                e.Handled = true;
            }else if(e.KeyCode == Keys.R && e.Control)
            {
                RunScript();
            }
            
        }

        private void toolStripButton7_Click(object sender, EventArgs e)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe";
            proc.StartInfo.Arguments = "вывод привет";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.StandardOutputEncoding = Encoding.Default;
            proc.Start();
            proc.WaitForExit();
            fastColoredTextBox1.Text = proc.StandardOutput.ReadToEnd();

        }
    }
}

