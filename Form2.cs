using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vestris.ResourceLib;

namespace RSHELL_IDE
{
    

    public partial class Form2 : Form
    {
        public string shellPAth = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe";

        string script;
        string icon;
        string startParams;
        string outputPath;
        int HideRuntime = 0;
        int UACask = 0;
        string RuntimeArgs = "";
        string SEDPath = Application.StartupPath + "\\iexpress_options.SED";
        public Form2(string file)
        {
            
              

            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (File.Exists("C:\\Windows\\system32\\iexpress.exe"))
            {
                if (File.Exists(shellPAth))
                {
                    if (!File.Exists(textBox1.Text))
                    {


                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "Renux Shell Script (*.rs)|*.rs|TXT (*.txt)|*.txt";
                        openFileDialog.FilterIndex = 1;
                        openFileDialog.RestoreDirectory = true;
                        if (openFileDialog.ShowDialog() == DialogResult.OK)
                        {
                            textBox1.Text = openFileDialog.FileName;
                        }
                        else
                        {
                            return;
                        }


                    }

                        SaveFileDialog sfd = new SaveFileDialog();
                        sfd.Filter = "Executeable (*.exe)|*.exe";
                        sfd.FileName = "";
                        sfd.RestoreDirectory = true;


                        if (sfd.ShowDialog() == DialogResult.OK)
                        {
                            progressBar1.Visible = true;
                            listView1.Items.Clear();
                     


                            Log("Начало сборки", Color.Black);

                            var iexpressSED =
                            $@"
                        [Version]
                        Class=IEXPRESS
                        SEDVersion=3
                        [Options]
                        PackagePurpose=InstallApp
                        ShowInstallProgramWindow={HideRuntime}
                        HideExtractAnimation=1
                        UseLongFileName=1
                        InsideCompressed=0
                        RebootMode=N
                        CheckAdminRights={UACask} 
                        TargetName={sfd.FileName}
                        AppLaunched=cmd /c %FILE0% {RuntimeArgs}%FILE1%
                        PostInstallCmd=<none>
                        SourceFiles=SourceFiles
                        [Strings]
                        FILE0=""rshell.exe""
                        FILE1=""{Path.GetFileName(textBox1.Text)}""
                        [SourceFiles]
                        SourceFiles0={Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\by.strdev
                        SourceFiles1={Path.GetDirectoryName(textBox1.Text)}
                        [SourceFiles0]
                        %FILE0%=
                        [SourceFiles1]
                        %FILE1%=";
                            Log("Создание файла конфигурации", Color.Black);
                            try
                            {

                                File.WriteAllText(SEDPath, iexpressSED);
                            }
                            catch (Exception ex)
                            {
                                Log("Ошибка записи конфигурации: " + ex.Message, Color.Red);
                            }
                            Log("Сборка исполняемого файла", Color.Black);
                            try
                            {
                                Process p = new Process();
                                p.StartInfo.FileName = "C:\\Windows\\system32\\iexpress.exe";
                                p.StartInfo.Arguments = $"/N /Q {SEDPath}";
                                p.StartInfo.CreateNoWindow = true;
                                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                p.Start();
                                p.WaitForExit();
                                File.Delete(SEDPath);
                            }
                            catch (Exception ex)
                            {
                                Log("Ошибка: " + ex.Message, Color.Red);
                            }
                            Log("Добавление иконки", Color.Black);
                            try
                            {
                                MemoryStream ms = new MemoryStream();
                                if (File.Exists(txtIconPath.Text))
                                {
                                    Compiler.IconInjector.InjectIcon(sfd.FileName, txtIconPath.Text);
                                }
                                else
                                {
                                    Properties.Resources.scriptico.Save(ms);
                                    Compiler.IconInjector.InjectIconFromByte(sfd.FileName, ms.ToArray());
                                }


                            }
                            catch (Exception ex)
                            {
                                Log("Ошибка изменения иконки: " + ex.Message, Color.Red);
                            }

                            Log("Добавление AssemblyInfo", Color.Black);
                            try
                            {


                                VersionResource versionResource = new VersionResource();
                                versionResource.LoadFrom(sfd.FileName);

                                versionResource.FileVersion = processVer(v1.Value, v2.Value, v3.Value, v4.Value);
                                versionResource.ProductVersion = processVer(v1.Value, v2.Value, v3.Value, v4.Value);
                                versionResource.Language = 0;

                                StringFileInfo stringFileInfo = (StringFileInfo)versionResource["StringFileInfo"];
                                stringFileInfo["ProductName"] = txtProduct.Text;
                                stringFileInfo["FileDescription"] = txtDescription.Text;
                                stringFileInfo["CompanyName"] = txtCompany.Text;
                                stringFileInfo["LegalCopyright"] = txtCopyright.Text;
                                stringFileInfo["LegalTrademarks"] = txtTrademarks.Text;
                                stringFileInfo["Assembly Version"] = processVer(v1.Value, v2.Value, v3.Value, v4.Value);
                                stringFileInfo["InternalName"] = txtOriginalFilename.Text;
                                stringFileInfo["OriginalFilename"] = txtOriginalFilename.Text;
                                stringFileInfo["ProductVersion"] = processVer(v1.Value, v2.Value, v3.Value, v4.Value);
                                stringFileInfo["FileVersion"] = processVer(fv1.Value, fv2.Value, fv3.Value, fv4.Value);

                                versionResource.SaveTo(sfd.FileName);
                            }
                            catch (Exception ex)
                            {
                                Log("Ошибка изменения AssemblyInfo: " + ex.Message, Color.Red);
                            }

                            progressBar1.Invoke(new Action(() => progressBar1.Visible = false));
                            Log("Сборка завершена!", Color.Green);
                        
                        }
                    
                }
                else
                {
                    MessageBox.Show("Внимание! На вашем ПК не установлена консоль Renux Shell. Запустите последнюю версию установщика консоли и выполните команду 'консоль установить'.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("C:\\Windows\\system32\\iexpress.exe\nФайл не найден!", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
        private string processVer(object vv1, object vv2, object vv3, object vv4)
        {
            return $"{vv1}.{vv2}.{vv3}.{vv4}";
        }
        private void Log(string data, Color color)
        {
            ListViewItem li = new ListViewItem();
            li.ForeColor = color;
            li.Text = data;
 
            

            listView1.Invoke(new Action(() =>
            {
                listView1.Items.Add(li);
            }));
        }
        private void button1_Click_2(object sender, EventArgs e)
        {
            var fileContent = string.Empty;
            var filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                
                openFileDialog.Filter = "Renux Shell Script (*.rs)|*.rs|TXT (*.txt)|*.txt";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {


                    textBox1.Text = openFileDialog.FileName;



                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SaveFileDialog SFD = new SaveFileDialog();
            
            SFD.FileName = "Compiled";
            SFD.Filter = "Executeable (*.exe)|*.exe";

            if (SFD.ShowDialog() == DialogResult.OK)
            {

            }


        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            txtCompany.Enabled = checkBox1.Checked;
            txtCopyright.Enabled = checkBox1.Checked;
            txtDescription.Enabled = checkBox1.Checked;
            txtOriginalFilename.Enabled = checkBox1.Checked;
            txtProduct.Enabled = checkBox1.Checked;
            txtTrademarks.Enabled = checkBox1.Checked;
            v1.Enabled = checkBox1.Checked;
            v2.Enabled = checkBox1.Checked;
            v3.Enabled = checkBox1.Checked;
            v4.Enabled = checkBox1.Checked;
            fv1.Enabled = checkBox1.Checked;
            fv2.Enabled = checkBox1.Checked;
            fv3.Enabled = checkBox1.Checked;
            fv4.Enabled = checkBox1.Checked;
        }

        private void txtOriginalFilename_TextChanged(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                HideRuntime = 1;
            }
            else
            {
                HideRuntime = 0;
            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox3.Checked)
            {
                UACask = 1;
            }
            else
            {
                UACask = 0;
            }
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            textBox6.Enabled = checkBox4.Checked;
            if (!checkBox4.Checked) RuntimeArgs = "";

        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            if(textBox6.Text.Length > 0 && !textBox6.Text.EndsWith(" "))
            {

                RuntimeArgs = textBox6.Text + " ";
            }
            else if(textBox6.Text == " ")
            {
                textBox6.Text = "";
                RuntimeArgs = "";
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            OpenFileDialog o = new OpenFileDialog();
            o.FileName = "";
            o.Filter = "Icon (*.ico)|*.ico|Все файлы (*.*)|*.*";
            o.Multiselect = false;
            if(o.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Image = Bitmap.FromFile(o.FileName);
                txtIconPath.Text = o.FileName;
            }
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Enabled = checkBox5.Checked;
            button6.Enabled = checkBox5.Checked;
            txtIconPath.Enabled = checkBox5.Checked;
        }

        private void копироватьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listView1.SelectedItems[0].Text);
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}

