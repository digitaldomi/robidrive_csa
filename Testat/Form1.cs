using RobotCtrl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Testat
{
    public partial class Form1 : Form
    {
        #region members
        private Robot robot;
        RobotConsole rc;
        private bool ledblink;
        private bool currentLedState;
        private bool fileReceived = false;
        private bool destinationReached = false;
        int objectcount;
        bool is_running = false;
        //IPAddress guestIP = IPAddress.Parse("123.456.789.012");
        string[] lines; //contains orders separated 

        enum State { WAIT_FOR_START, WAIT_FOR_START_INIT, DRIVE_FORWARD, DRIVE_FORWARD_INIT, TURN, TURN_INIT, DRIVE_BACK, DRIVE_BACK_INIT, STOP, STOP_INIT};
        private bool trackObject = false;
        #endregion

        #region constructor & destructor
        public Form1()
        {
            InitializeComponent();

            rc = new RobotConsole();
            consoleView1.RobotConsole = rc;

            robot = new Robot();        // neuen Roboter erstellen
            robot.Drive.Power = true;   // Stromversorgung der Motoren (im DriveCtrl) einschalten

            runlineView.Drive = robot.Drive;
            runturnView.Drive = robot.Drive;
            runarcView.Drive = robot.Drive;
            driveView1.Drive = robot.Drive; // DriveView benötigt Drive-Objekt zur Visualisierung


            Init();
            radarView.Radar = robot.Radar;
            consoleView1.RobotConsole = robot.RobotConsole;

            commonRunParameters1.AccelerationChanged += AccelerationChanged;
            commonRunParameters1.SpeedChanged += SpeedChanged;

            SpeedChanged(null, EventArgs.Empty); // Default Wert setzen
            AccelerationChanged(null, EventArgs.Empty); // Default Wert setzen
            this.buttonHalt.Click += ButtonHalt_Click;
            this.buttonStop.Click += ButtonStop_Click;

            //Thread blinkLed = new Thread(new ThreadStart(blink_led));
            Thread sequenceThread = new Thread(new ThreadStart(sequence));
            //Thread object_det = new Thread(new ThreadStart(object_detect));

            sequenceThread.Start();
            //blinkLed.Start();
            //drive.Start();
            //object_det.Start();

            this.ledblink = false;


        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            robot.Drive.Stop();
        }

        private void ButtonHalt_Click(object sender, EventArgs e)
        {
            robot.Drive.Halt();
        }
        #endregion

        private void Init()
        {
            runlineView.Speed = commonRunParameters1.Speed;
            runlineView.Acceleration = commonRunParameters1.Acceleration;
            runturnView.Speed = commonRunParameters1.Speed;
            runturnView.Acceleration = commonRunParameters1.Acceleration;
            runarcView.Speed = commonRunParameters1.Speed;
            runarcView.Acceleration = commonRunParameters1.Acceleration;
        }

        #region methods
        private void SpeedChanged(object sender, EventArgs e)
        {
            runlineView.Speed = commonRunParameters1.Speed;
            runturnView.Speed = commonRunParameters1.Speed;
            runarcView.Speed = commonRunParameters1.Speed;


        }
        private void AccelerationChanged(object sender, EventArgs e)
        {
            runlineView.Acceleration = commonRunParameters1.Acceleration;
            runturnView.Acceleration = commonRunParameters1.Acceleration;
            runarcView.Acceleration = commonRunParameters1.Acceleration;
        }



        #endregion

        private void label1_ParentChanged(object sender, EventArgs e)
        {

        }

        private void blink_led()
        {
            while (true)
            {
                Thread.Sleep(500);
                if (ledblink)
                {
                    currentLedState = !currentLedState;
                    (rc[Leds.Led1]).LedEnabled = currentLedState;
                    (rc[Leds.Led2]).LedEnabled = currentLedState;
                    (rc[Leds.Led3]).LedEnabled = currentLedState;
                    (rc[Leds.Led4]).LedEnabled = currentLedState;
                }
                else
                {
                    (rc[Leds.Led1]).LedEnabled = true;
                    (rc[Leds.Led2]).LedEnabled = true;
                    (rc[Leds.Led3]).LedEnabled = true;
                    (rc[Leds.Led4]).LedEnabled = true;
                }
            }
        }

        //private void object_detect()
        //{

        //    float distance = 0;
        //    int counter= 0;
        //    bool object_detected = false;
        //    while (true)
        //    {
        //        if (trackObject)
        //        {
        //            Thread.Sleep(50);
        //            distance = this.robot.Radar.Distance;
        //            if (distance < 0.7f)
        //            {
        //                counter++;
        //                if (counter == 5 && object_detected == false)
        //                {
        //                    objectcount++;
        //                    object_detected = true;
        //                }
        //            }
        //            else
        //            {
        //                object_detected = false;
        //                counter = 0;
        //            }
        //        } else
        //        {
        //            objectcount = 0;
        //            counter = 0;
        //        }
        //    }
        //}
        

        private void sequence()
        {
            while (true)
            {
                String file = "TrackLine 100\n TurnLeft 90\nStart"; //getFile();
                drive_robot(file);


               

                //while(fileReceived == false){ Thread.Sleep(10);}
                drive_robot(file);
                while (destinationReached == false) { Thread.Sleep(10); }
                sendFile();
                Console.Out.WriteLine("End!");
                while (true) { Thread.Sleep(100); }
            }
        }



        

        private void drive_robot(string text)
        {
            while (true)
            {
                String text1 = "TrackLine 1.0\nTrackTurnLeft 90\nTrackTurnRight 90\nTrackArcLeft 90 1.0\nTrackArcRight 90 1.0\nStart";

                Console.WriteLine("Reading comands from string...");
                string[][] orderList = new string[100][];
                for (int i = 0; i < orderList.GetLength(0); i++)
                {
                    orderList[i] = new string[3];
                }

                string currentLine;
                System.IO.StringReader reader = new System.IO.StringReader(text1); //TODO CHANGE HERE FOR ACTUAL FILE
                int index = 0;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    orderList[index] = currentLine.Split(' ');
                    index++;

                }
                Console.WriteLine("Done reading comands, start driving now...");
                robot.Drive.Position = new PositionInfo(0, 0, 0);
                int nbrOfComands = index;
                index = 0;
                this.robot.Drive.Power = true;
                while(orderList[index] != null)
                {
                    Thread.Sleep(1000);
                    switch (orderList[index][0])
                    {
                        case "TrackLine":
                            this.robot.Drive.RunLine(float.Parse(orderList[index][1]), 0.3f, 0.5f);
                            break;
                        case "TrackTurnRight":
                            this.robot.Drive.RunTurn(float.Parse(orderList[index][1]), 0.3f, 0.5f);
                            break;
                        case "TrackTurnLeft":
                            this.robot.Drive.RunTurn((-1.0f) * float.Parse(orderList[index][1]), 0.3f, 0.5f);
                            break;
                        case "TrackArcLeft":
                            this.robot.Drive.RunArcLeft(float.Parse(orderList[index][1]), float.Parse(orderList[index][2]), 0.3f, 0.5f);
                            break;
                        case "TrackArcRight":
                            this.robot.Drive.RunArcRight(float.Parse(orderList[index][1]),(-1.0f)* float.Parse(orderList[index][2]), 0.3f, 0.5f);
                            break;
                        case "Start":
                            Console.WriteLine("Last command read");
                            this.robot.Drive.Power = false;
                            break;
                        default:
                            Console.Write("!!!Error, invalid comand read!!!");
                            break;

                    }
                    index++;
                    Thread.Sleep(1000);
                    while (!this.robot.Drive.Done) { Thread.Sleep(5); }
                    Thread.Sleep(1000);
                    
                }


                while ((!rc[Switches.Switch2].SwitchEnabled)) { Thread.Sleep(5); }
                trackObject = true;
                objectcount = 0;
                is_running = true;
                ledblink = true;
                this.robot.Drive.RunLine(2.5f, 0.2f, 0.2f);

                while (!this.robot.Drive.Done) { Thread.Sleep(5); }
                Thread.Sleep(1000);
                this.robot.Drive.RunTurn(180, 0.2f, 0.2f);
                Thread.Sleep(3000);

                while (!this.robot.Drive.Done) { Thread.Sleep(5); }

                this.robot.Drive.RunLine(2.5f, 0.2f, 0.2f);
                Thread.Sleep(1000);

                while (!this.robot.Drive.Done) { Thread.Sleep(5); }

                ledblink = false;
                is_running = false;
                trackObject = false;
                while ((rc[Switches.Switch2].SwitchEnabled)) { Thread.Sleep(5); }


            }
        }

        private void getFile()
        {
            //try {
            //    IPAddress ipAddress = Dns.GetHostEntry("localhost").AddressList[0];
            //    TcpListener listen = new TcpListener(ipAddress, 8080);
            //    listen.Start();

            //    Console.WriteLine("Warte auf Verbindung auf Port " +
            //        listen.LocalEndpoint + "...");
            //    TcpClient client = listen.AcceptTcpClient();
            //    Console.WriteLine("Verbindung zu " +
            //        client.Client.RemoteEndPoint);
            //    guestIP = IPAddress.Parse(client.Client.RemoteEndPoint.ToString());
            //    //TextWriter tw = new StreamWriter(client.GetStream());
            //    //tw.Write(DateTime.Now.ToString());
            //    //tw.Flush();

            //    //GET FILE HERE HUERO GERI

            //    client.Close();




            //} catch
            //{
            //    Console.Out.WriteLine("Error while connecting...");
            //    while (true) { }
            //}
        }

        private void sendFile()
        {
            //TcpClient client = new TcpClient(guestIP.ToString(), 8080); // whois von switch
            //StreamWriter outStream = new StreamWriter(client.GetStream());
            ////StreamReader inStream = new StreamReader(client.GetStream());
            //outStream.WriteLine("Hallo das ist ein Test");
            //outStream.Flush();
            ////String line;
            ////while ((line = inStream.ReadLine()) != null)
            ////{
            ////    Console.WriteLine(line);
            ////}

            //// SEND FILE HERE

            //client.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(is_running) labelCount.Text = objectcount.ToString();
        }
    }
}
