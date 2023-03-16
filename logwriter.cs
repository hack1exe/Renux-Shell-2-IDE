using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace RSHELL_IDE
{
    class logwriter
    {
        public static void Write(string text)
        {

                string prefix = "[" + DateTime.Now + "] ";
                
                File.AppendAllText(Application.StartupPath + "\\REPORT_" + DateTime.Now + ".log", prefix + text);


        }
    }
}
