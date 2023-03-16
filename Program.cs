using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSHELL_IDE
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);




            try
            {
                string[] args = Environment.GetCommandLineArgs();
                if(args.Length > 1)
                {
                    if(args[1] == "/contextadd")
                    {
                        try
                        {
                            RegistryKey currentUserKey = Registry.ClassesRoot;
                            RegistryKey helloKey = currentUserKey.CreateSubKey(".rs\\ShellNew");
                            helloKey.SetValue("NullFile", "", RegistryValueKind.String);
                            helloKey.Close();

                            helloKey = currentUserKey.CreateSubKey("renux-file\\shell\\RenuxShellIDE\\");
                            helloKey.SetValue("Icon", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\products\\Renux Shell IDE\\app.exe\"", RegistryValueKind.String);
                            helloKey.SetValue("MUIVerb", "Редактировать в Renux Shell IDE", RegistryValueKind.String);
                            helloKey = currentUserKey.CreateSubKey("renux-file\\shell\\RenuxShellIDE\\command");
                            helloKey.SetValue("", "\"" + Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\by.strdev\\products\\Renux Shell IDE\\app.exe\" \"%1\"", RegistryValueKind.String);
                            helloKey.Close();



                      

                                Process upd = new Process();
                                upd.StartInfo.FileName = "cmd";
                                upd.StartInfo.Arguments = "/c taskkill /f /im explorer.exe & start %windir%\\explorer.exe";
                                upd.StartInfo.UseShellExecute = false;
                                upd.StartInfo.CreateNoWindow = true;
                                upd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                upd.Start();
                                upd.WaitForExit();

                            

                        }
                        catch
                        {
                            MessageBox.Show("Отказано в доступе", "Ошибка установки");
                        }

                    }else if(args[1] == "/contextdel")
                    {
                        try { 

                        
                            RegistryKey currentUserKey = Registry.ClassesRoot;
                            RegistryKey helloKey = currentUserKey.CreateSubKey(".rs\\ShellNew\\");

                                helloKey.DeleteValue("NullFile");





                            helloKey.Close();
                            helloKey = currentUserKey.CreateSubKey("renux-file\\shell\\");
                            helloKey.DeleteSubKeyTree("RenuxShellIDE");


                            Process upd = new Process();
                                upd.StartInfo.FileName = "cmd";
                                upd.StartInfo.Arguments = "/c taskkill /f /im explorer.exe & start %windir%\\explorer.exe";
                                upd.StartInfo.UseShellExecute = false;
                                upd.StartInfo.CreateNoWindow = true;
                                upd.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                upd.Start();
                                upd.WaitForExit();
                            






                        }
                        catch
                        {
                            MessageBox.Show("Отказано в доступе", "Ошибка удаления");
                        }
                    }
                    else if (args[1] == "/skipsplash")
                    {
                        Application.Run(new Form1(""));
                    }
                    else if (args[1] == "/compiler")
                    {
                        Application.Run(new Form2(string.Empty));
                    }
                    else if(args[1].Contains("/"))
                    {
                        MessageBox.Show("Параметры указаны неверно");

                    }else if (args[1].Contains("-"))
                    {
                        MessageBox.Show("Параметры указаны неверно");
                    }

                    else if (args[1].Contains("0x"))
                    {
                        Application.Run(new splash());
                        Application.Run(new Form1(""));
                    }
                    else

                    {

                        Application.Run(new splash());
                        Application.Run(new Form1(args[1].ToString()));
                    }

                }
                else
                {

                    Application.Run(new splash());
                    Application.Run(new Form1(""));
                }
            }
            catch { }





        }

    }

}
