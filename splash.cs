using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RSHELL_IDE
{
    public partial class splash : Form
    {
       public bool LockClose = true;
        public splash()
        {
            InitializeComponent();
        }

        private void splash_Load(object sender, EventArgs e)
        {


        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            switch (e.CloseReason)
            {
                case CloseReason.UserClosing:

                
                        e.Cancel = LockClose;
                   
                    
                    break;
            }

            base.OnFormClosing(e);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
         //   Form1 form = new Form1();
            timer1.Enabled = false;
            //form.Show();
            LockClose = false;
            this.Close();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if(this.Opacity == 1.0)
            {
                timer2.Enabled = false;
            }
            else
            {
                this.Opacity = this.Opacity + 0.01;
            }
        }
    }
}
