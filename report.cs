using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace RSHELL_IDE
{
    public partial class report : Form
    {
        public report()
        {
            InitializeComponent();
        }
        static string GetWinName()
        {
            string key = @"SOFTWARE\Microsoft\Windows NT\CurrentVersion";
            using (RegistryKey regKey = Registry.LocalMachine.OpenSubKey(key))
            {
                if (regKey != null)
                {
                    try
                    {
                        string name = regKey.GetValue("ProductName").ToString();
                        if (name == "") return "значение отсутствует";
                        if (name.Contains("XP"))
                            return "XP";
                        else if (name.Contains("7"))
                            return "Windows 7";
                        else if (name.Contains("8"))
                            return "Windows 8";
                        else if (name.Contains("10"))
                            return "Windows 10";
                        else
                            return "Неизвестная версия Windows";
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
                else
                    return "Не удалось получить значение ключа в реестре";
            }
        }







        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "" || richTextBox1.Text == "")
            {
                MessageBox.Show("Заполните оба поля в окне баг-репортера", "Renux Shell IDE Bug Reporter", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                System.OperatingSystem osInfo = System.Environment.OSVersion;
                string RshellVersion = "Консоль не установлена";
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe"))
                {
                    RshellVersion = RshellV();
                }






                string osBit = "Архитектура_не_определена";
                if (Environment.Is64BitOperatingSystem)
                {
                    osBit = "x64";
                }
                else
                {
                    osBit = "x32";
                }

                richTextBox1.Text.Replace("\n", Environment.NewLine);



                //string message = $"\n Системная информация: " + " \n" + GetWinName() + " (" + osInfo.ToString() + "_" + osBit + ")" + "\n Версия rshell: " + RshellVersion + "\n" + "\n" + "Контакты пользователя: " + "\n" + textBox1.Text + "\n Сообщение пользователя: " + "\n" + "\n" + richTextBox1.Text;
                string message = $"\n{DateTime.Now}\n" +
                    $"Системная информация:\n{GetWinName()} ({osInfo}_{osBit})\n" +
                    $"Версия Renux Shell: {RshellVersion}\n" +
                    $"Пользователь: {Environment.UserDomainName}\\{Environment.UserName}\n" +
                    $"Версия IDE: {Application.ProductVersion}\n" +
                    $"Контакты пользователя:\n{textBox1.Text}\n" +
                    $"Сообщение пользователя:\n\n{richTextBox1.Text}\n\n";
                SendToDebugger(message);

            }

        }
        private string SystemHwID()
        {
            var mbs = new ManagementObjectSearcher("Select ProcessorId From Win32_processor");
            ManagementObjectCollection mbsList = mbs.Get();
            string id = "";
            foreach (ManagementObject mo in mbsList)
            {
                id = mo["ProcessorId"].ToString();
                break;
            }
            return id;
        }


        private string RshellV()
        {
            FileVersionInfo RshellV = FileVersionInfo.GetVersionInfo(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\rshell.exe");
            return RshellV.FileVersion;
        }

        private void SendToDebugger(string message)
        {
            string url = "#placeholder#" + message;


            using (var webClient = new WebClient())
            {
                try
                {
                    var response = webClient.DownloadString(url);
                    if (response == label1.Text)
                    {
                        MessageBox.Show("Сообщение успешно отправленно! Ждите ответа");
                        this.Close();
                    }
                    else
                    {
                        if(response == label4.Text)
                        {
                            MessageBox.Show("Вы были заблокированы в системе отчета об ошибках за нарушение Соглашения");
                        }
                        else
                        {
                            MessageBox.Show("Ошибка отправки сообщения, повторите попытку позже.");
                        }
                        


                    }
                }
                catch
                {
                    MessageBox.Show("Нет интернет-подключения либо сервер временно недоступен.");
                }
                
                

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == label1.Text)
            {
                MessageBox.Show("Отправка успешна!");
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
           
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            EULA eula = new EULA();
            eula.ShowDialog();
        }

        private void button2_Click_2(object sender, EventArgs e)
        {

        }

        private void button2_Click_3(object sender, EventArgs e)
        {
            
        }

        private void report_Load(object sender, EventArgs e)
        {

        }
        private void SelfDel()
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd";
            process.StartInfo.Arguments = "/c timeout /t 1 /nobreak & del " + Application.StartupPath + "\\ /q /s";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.Start();
            Application.Exit();
        }
    }
}
