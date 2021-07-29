using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TurtleGame
{
    public partial class SplashScreen : Form
    {
        int value;
        Random random;
        public SplashScreen()
        {
            InitializeComponent();
            random = new Random();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timerProgress.Start();
        }

        private void timerProgress_Tick(object sender, EventArgs e)
        {
            value += random.Next(1, 10);
            if(value > 100)
            {
                value = 100;
            }
            progressBar.Value = value;
            if( value == 100 )
            {
                timerProgress.Stop();
                Form1 form = new Form1();
                form.Show();
                Hide();
            }
        }
    }
}
