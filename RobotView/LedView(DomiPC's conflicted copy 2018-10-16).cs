using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace RobotView
{
    public partial class LedView : UserControl
    {

        private bool State = false;
        private RobotCtrl.Led led;

        public LedView()
        {
            
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (State)
            {
                State = false;
                pictureBox1.Image = Resource1.SwitchOff;
            }
            else
            {
                State = true;
                pictureBox1.Image = Resource1.SwitchOn;
            }
        }
    }
}