using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHotel
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        int StartP = 1;
        private void timer1_Tick(object sender, EventArgs e)
        {
            StartP += 1;
            MyProgress.Value = StartP;
            if(MyProgress.Value == 100)
            {
                progressBar1.Value = 0;
                timer1.Stop();
                Login obj = new Login();
                this.Hide();
                obj.Show();
            }
        }

        private void Splash_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }
    }
}
