using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RobotCtrl;

namespace RobotView
{
    public partial class LedView : UserControl
    {

        private Led led;

        private bool state;

        public LedView()
        {
            
            InitializeComponent();
            State = false;
        }

        public Led Led
        {
            get { return led; }
            set
            {
                led = value;
                if(led != null)
                {
                    led.LedStateChanged -= LedStateChanged;
                }
                if(led != null)
                {
                    this.led.LedStateChanged += LedStateChanged;
                }
            } 
        }

        public bool State
        {
            get { return state; }
            set
            {
                state = value;
                pictureBox1.Image = (value ? Resource1.LedOn : Resource1.LedOff);
            }
        }

        private void LedStateChanged(object sender, LedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<LedEventArgs>(LedStateChanged), sender, e);
            }
            else
            {
                State = e.LedEnabled;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
          
        }
    }
}