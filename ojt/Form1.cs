using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using libzkfpcsharp;
using System.Drawing.Imaging;

using MySql.Data.MySqlClient;

namespace ojt
{
    public partial class Form1 : Form
    {
        MySqlConnection con;
        string connectString;
        string myThumb;
        int count = 0;
        string myTime;
        string myDate;
        int myId;
        int meTime;
        string myTimein;
        string myTimeout;
        


        IntPtr mDevHandle = IntPtr.Zero;
        IntPtr mDBHandle = IntPtr.Zero;
        IntPtr FormHandle = IntPtr.Zero;
        bool bIsTimeToDie = false;

        byte[] FPBuffer;

        const int REGISTER_FINGER_COUNT = 3;

        byte[][] RegTmps = new byte[3][];
        byte[] RegTmp = new byte[2048];
        byte[] CapTmp = new byte[2048];

        int cbCapTmp = 2048;

        private int mfpWidth = 0;
        private int mfpHeight = 0;
        private int mfpDpi = 0;
        private TimeSpan TimeSpan;
        
        const int MESSAGE_CAPTURED_OK = 0x0400 + 100;

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        

        public Form1()
        {
            InitializeComponent();
            FormHandle = this.Handle;
            this.timer1.Start();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            this.myDate = DateTime.Today.ToString("dd-MM-yyyy");
            this.btnDtr.Enabled = false;
            this.txtFullName.Enabled = false;
            this.txtTime.Enabled = false;

            this.loadconnection();

            this.Mor(1);
            this.Morls(1);











        }

       

        public void Mor(int id)
        {
            int ret = zkfperrdef.ZKFP_ERR_OK;

            if (id == 0)
            {

                

               
                return;
            }
            else
            {
               


                if ((ret = zkfp2.Init()) == zkfperrdef.ZKFP_ERR_OK)
                {

                    int nCount = zkfp2.GetDeviceCount();
                    if (nCount > 0)
                    {




                    }
                    else
                    {
                        zkfp2.Terminate();
                        MessageBox.Show("No device connected!");
                    }
                }
                else
                {
                    MessageBox.Show("Initialize fail, ret=" + ret + " !");
                }
            }

            
        }


        public void Morls(int id)
        {
            if(id == 0)
            {
                zkfp2.Terminate();
                zkfp2.CloseDevice(mDevHandle);
               
                return;
            }
            else
            {
               
                int ret = zkfp.ZKFP_ERR_OK;
                if (IntPtr.Zero == (mDevHandle = zkfp2.OpenDevice(0)))
                {
                    MessageBox.Show("OpenDevice fail");
                    return;
                }
                if (IntPtr.Zero == (mDBHandle = zkfp2.DBInit()))
                {
                    MessageBox.Show("Init DB fail");
                    zkfp2.CloseDevice(mDevHandle);
                    mDevHandle = IntPtr.Zero;
                    return;
                }








                for (int i = 0; i < 3; i++)
                {
                    RegTmps[i] = new byte[2048];
                }
                byte[] paramValue = new byte[4];
                int size = 4;
                zkfp2.GetParameters(mDevHandle, 1, paramValue, ref size);
                zkfp2.ByteArray2Int(paramValue, ref mfpWidth);

                size = 4;
                zkfp2.GetParameters(mDevHandle, 2, paramValue, ref size);
                zkfp2.ByteArray2Int(paramValue, ref mfpHeight);

                FPBuffer = new byte[mfpWidth * mfpHeight];

                size = 4;
                zkfp2.GetParameters(mDevHandle, 3, paramValue, ref size);
                zkfp2.ByteArray2Int(paramValue, ref mfpDpi);

                //textRes.AppendText("reader parameter, image width:" + mfpWidth + ", height:" + mfpHeight + ", dpi:" + mfpDpi + "\n");


                Thread captureThread = new Thread(new ThreadStart(DoCapture));
                captureThread.IsBackground = true;
                captureThread.Start();
                bIsTimeToDie = false;
                //textRes.AppendText("Open succ\n");
                this.labelStatus.Text = "Status: Ready";
            }
            

           

        }


        public void DoCapture()
        {
           


            while (!bIsTimeToDie)
            {
                
                cbCapTmp = 2048;

                int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);

                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
                    this.loadconnection();
                    this.loadThumb();
                    this.count = 0;
                }
                Thread.Sleep(200);

            }

            zkfp2.DBClear(mDevHandle);




        }

        protected override void DefWndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case MESSAGE_CAPTURED_OK:
                    {
                       
                        MemoryStream ms = new MemoryStream();
                        BitmapFormat.GetBitmap(FPBuffer, mfpWidth, mfpHeight, ref ms);
                        Bitmap bmp = new Bitmap(ms);
                        this.picFPImg.Image = bmp;


                        String strShow = zkfp2.BlobToBase64(CapTmp, cbCapTmp);
                        //txtbase1.Text = strShow;
                        this.myThumb = strShow;
                        //textRes.AppendText("capture template data:" + strShow + "\n");

                       
                        
                       





                    }
                    break;

                default:
                    base.DefWndProc(ref m);

                    break;
            }

        }



        private void button1_Click(object sender, EventArgs e)
        {

            

        }

        private void btnAdminLogin_Click(object sender, EventArgs e)
        {
            bIsTimeToDie = true;
            zkfp2.DBClear(mDevHandle);
            zkfp2.Terminate();
            this.Hide();
            Form2 form2 = new Form2(this);
            form2.Show();
            
        }

       
       
        public void loadThumb()
        {
            
            //connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";
            //MySqlConnection conDb = new MySqlConnection(connectString);

            MySqlCommand cmdDb = new MySqlCommand("select id,thumb,first_name,time,last_name from ojt.student;", this.con);
           
            MySqlDataReader myreader;
           

            try
            {

                this.con.Open();
                myreader = cmdDb.ExecuteReader();
               

                while (myreader.Read())
                {

                   this.myId = Convert.ToInt32(myreader["id"]);
                    String thumb = myreader.GetString("thumb");
                    String f_name = myreader.GetString("first_name");
                    String l_name = myreader.GetString("last_name");
                    this.meTime = Int32.Parse(myreader.GetString("time"));
                    int timefinal = this.meTime;

                    byte[] blob2 = Convert.FromBase64String(thumb);
                    byte[] blob1 = Convert.FromBase64String(this.myThumb);

                    int ret = zkfp2.DBMatch(mDBHandle, blob1, blob2);
                    if (ret > 40)
                    {
                        
                        int hours;
                        int minutes;
                        int seconds;

                        seconds = timefinal;
                        hours = (int)(Math.Floor((double)(seconds / 3600)));
                        seconds = seconds % 3600;
                        minutes = (int)(Math.Floor((double)(seconds / 60)));
                        seconds = seconds % 60;
                        string time2 = hours + ":" + minutes + ":" + seconds;

                        this.txtFullName.Invoke(new Action(() => this.txtFullName.Text = f_name +" "+ l_name));
                        this.txtTime.Invoke(new Action(() => this.txtTime.Text = time2));

                        myreader.Close();   
                        this.con.Close();
                       
                        
                        try
                        {

                            this.con.Open();
                           
                            MySqlCommand cmdDb2 = new MySqlCommand("select * from ojt.dtr where student_id = '"+this.myId+"' AND date='"+this.myDate+"' AND timeout = ''", this.con);
                            myreader = cmdDb2.ExecuteReader();

                            while (myreader.Read())
                            {
                                this.myTimein = myreader.GetString("timein");
                               
                            }
                            
                                
                            if (myreader.HasRows)
                            {
                                
                                this.btnDtr.Invoke(new Action(() => this.btnDtr.Enabled = true));
                                
                                this.con.Close();
                                this.con.Open();
                                string myTime2 = DateTime.Now.ToString("HH:mm:ss");
                                TimeSpan = Convert.ToDateTime(myTime2) - Convert.ToDateTime(this.myTimein);
                                double secondss = TimeSpan.TotalSeconds;
                                MySqlCommand cmdDb4 = new MySqlCommand("update ojt.dtr set timeout = '" + myTime2 + "',consume = '" + secondss + "' where student_id = '" + this.myId+"' AND timein='"+this.myTimein+"'", this.con);
                                myreader = cmdDb4.ExecuteReader();
                                MessageBox.Show("Time-out Successfully!!");
                                this.con.Close();


                                /**/
                               connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";

                                MySqlConnection conDb3 = new MySqlConnection(connectString);
                                MySqlCommand cmdDb3 = new MySqlCommand("select sum(consume) from ojt.dtr where student_id = '" + this.myId + "';", conDb3);
                               
                                try
                                {
                                    conDb3.Open();
                                    int aw2 = Int32.Parse(cmdDb3.ExecuteScalar().ToString());
                                    int hours2;
                                    int minutes2;
                                    int seconds2;

                                    
                                    int remainTime = this.meTime - aw2;
                                    seconds2 = remainTime;
                                    hours2 = (int)(Math.Floor((double)(seconds2 / 3600)));
                                    seconds2 = seconds2 % 3600;
                                    minutes2 = (int)(Math.Floor((double)(seconds2 / 60)));
                                    seconds2 = seconds2 % 60;
                                    string time3 = hours2 + ":" + minutes2 + ":" + seconds2;
                                   
                                    this.txtTime.Invoke(new Action(() => this.txtTime.Text = time3.ToString()));

                                   





                                }
                                catch (MySql.Data.MySqlClient.MySqlException ex)
                                {

                                    MessageBox.Show(ex.Message);
                                }
                               /**/
                               


                                return;

                            }
                            else
                            {
                               
                                this.con.Close();
                                this.con.Open();
                                string myTime1 = DateTime.Now.ToString("HH:mm:ss");
                                MySqlCommand cmdDb3 = new MySqlCommand("insert into ojt.dtr (student_id,date,timein) values('"+this.myId+"','"+this.myDate+"','"+ myTime1 + "')", this.con);
                                myreader = cmdDb3.ExecuteReader();
                                MessageBox.Show("Time-in Successfully!");
                                this.con.Close();
                                return;
                               

                            }


                         




                        }
                        catch (MySql.Data.MySqlClient.MySqlException ex)
                        {

                            MessageBox.Show(ex.Message);
                            
                        }


                        this.count = this.count + 1;
                        
                        //code query to time in here
                    }                 
                }

                if(count == 0)
                {
                    this.btnDtr.Invoke(new Action(() => this.btnDtr.Enabled = false));
                    MessageBox.Show("No equivalient user!");
                    this.txtFullName.Invoke(new Action(() => this.txtFullName.Text = ""));
                    this.txtTime.Invoke(new Action(() => this.txtTime.Text = ""));
                }
               

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }

           
        }



        
        public void loadconnection()
        {
            try
            {
                this.connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";
                this.con = new MySqlConnection(this.connectString);
               
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime dtime = DateTime.Now;
            this.lblTime.Text = dtime.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {

           
        }

        private void btnDtr_Click(object sender, EventArgs e)
        {

            Form4 f4 = new Form4(this.myId);
            f4.Show();
        }

        private void btnEmergency_Click(object sender, EventArgs e)
        {

            this.Hide();
            Form5 f5 = new Form5(this);
            f5.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            zkfp2.Terminate();
        }
    }
}
