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
    public partial class SwitchView : UserControl
    {
        private bool state;
        private Switch swi;

        public SwitchView()
        {
            InitializeComponent();
            State = false;
        }

        public Switch Switch {
            get { return swi; }
            set {
                swi = value;
                if (swi != null)
                {
                    swi.SwitchStateChanged -= SwitchStateChanged;
                }
                if (swi != null) {
                    this.swi.SwitchStateChanged += SwitchStateChanged;
                }
            }
        }

        private void SwitchStateChanged(object sender, SwitchEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<SwitchEventArgs>(SwitchStateChanged), sender, e);
            }
            else
            {
                State = e.SwitchEnabled;
            }
        }

        public bool State
        {
            get { return state; }
            set {
                state = value;
                pictureBox1.Image = (value ? Resource1.SwitchOn : Resource1.SwitchOff);
            }
        }



        private void pictureBox1_Click(object sender, EventArgs e)
        {
       
        }
    }
}