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
    public partial class Form3 : Form
    {
        MySqlConnection con;
        string connectString;
        String strShow;
        String id;

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

        const int MESSAGE_CAPTURED_OK = 0x0400 + 6;

        [DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form1 f1 = new Form1();
            
            FormHandle = this.Handle;
           
            this.FormClosed += new FormClosedEventHandler(closed);
            this.dbConnection();
            this.loadDataTable();

           
            this.Mor2();
            this.Morls2();

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {
            FormHandle = this.Handle;
           
            
        }



        public void closed(object sender, FormClosedEventArgs e)
        {
           
            Application.ExitThread();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            this.clearText();
        }

        void clearText()
        {
            fname.Text = "";
            lname.Text = "";
            gender.Text = "";
            contact.Text = "";
            time.Text = "";
            picFPImg2.Image = null;
            txtCode.Text = "";
        }

        public void loadDataTable()
        {
            connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";
            
            MySqlConnection conDb = new MySqlConnection(connectString);
            MySqlCommand cmdDb = new MySqlCommand("select first_name,last_name,gender,contact,time from ojt.student;", conDb);
            

            try
            {

                MySqlDataAdapter sda = new MySqlDataAdapter();
                sda.SelectCommand = cmdDb;
                DataTable dbdataset = new DataTable();
                sda.Fill(dbdataset);
                BindingSource bsource = new BindingSource();

                bsource.DataSource = dbdataset;
                dataGridView1.DataSource = bsource;
                sda.Update(dbdataset);

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
        public void dbConnection()
        {
            connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = connectString;
                con.Open();
                MessageBox.Show("You have succesfully Login!!!");
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        public void Mor2()
        {

            int ret = zkfperrdef.ZKFP_ERR_OK;
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


        public void Morls2()
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


        private void DoCapture()
        {
            while (!bIsTimeToDie)
            {
                cbCapTmp = 2048;
                int ret = zkfp2.AcquireFingerprint(mDevHandle, FPBuffer, CapTmp, ref cbCapTmp);
                if (ret == zkfp.ZKFP_ERR_OK)
                {
                    SendMessage(FormHandle, MESSAGE_CAPTURED_OK, IntPtr.Zero, IntPtr.Zero);
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
                        this.picFPImg2.Image = bmp;


                       this.strShow = zkfp2.BlobToBase64(CapTmp, cbCapTmp);
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
            double dtime = double.Parse(this.time.Text) * 3600;
            connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";
            string Query = "insert into ojt.student (first_name,last_name,gender,contact,time,thumb,emergency) values('" + this.fname.Text + "','" + this.lname.Text + "','" + this.gender.Text + "', '" + this.contact.Text + "','" + dtime + "','"+ this.strShow + "','" + this.txtCode.Text + "')";
            MySqlConnection conDb = new MySqlConnection(connectString);
            MySqlCommand cmdDb = new MySqlCommand(Query, conDb);
            MySqlDataReader reader;
  
            try
            {
                
                conDb.Open();
                reader = cmdDb.ExecuteReader();
                this.clearText();
                MessageBox.Show("New Student has been Succesfully Save!");
                this.loadDataTable();
                conDb.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
            if(e.RowIndex >= 0)
            {
                DataGridViewRow row = this.dataGridView1.Rows[e.RowIndex];

              
                this.txtFname.Text = row.Cells["first_name"].Value.ToString();
                this.txtLastname.Text = row.Cells["last_name"].Value.ToString();
                this.txtGender.Text = row.Cells["gender"].Value.ToString();
                this.txtContact.Text = row.Cells["contact"].Value.ToString();
                this.txtTime.Text = row.Cells["time"].Value.ToString();
                

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
           
            connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";
            string Query = "delete from ojt.student where contact='"+this.txtContact.Text+"'";
            MySqlConnection conDb = new MySqlConnection(connectString);
            MySqlCommand cmdDb = new MySqlCommand(Query, conDb);
            MySqlDataReader reader;

            try
            {

                conDb.Open();
                reader = cmdDb.ExecuteReader();
                this.clearText();
                MessageBox.Show("Deleted Successfully!");
                this.loadDataTable();
                conDb.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
           
        }

        private void Form3_FormClosed(object sender, FormClosedEventArgs e)
        {
            zkfp2.Terminate();
        }

        private void gender_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.password.Text = "";
            this.repeat_password.Text = "";
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            string password = this.password.Text;
            string repeat_password = this.repeat_password.Text;

            if(password == repeat_password)
            {

                connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";

                MySqlConnection conDb = new MySqlConnection(connectString);
                MySqlCommand cmdDb = new MySqlCommand("update users SET password= '"+ repeat_password + "' ;", conDb);

                MySqlDataReader myReader;

                conDb.Open();
                myReader = cmdDb.ExecuteReader();
                MessageBox.Show("Password has been updated succesfully!");
            }
            else
            {
                MessageBox.Show("Password not Match!");
            }
        }
    }
}
