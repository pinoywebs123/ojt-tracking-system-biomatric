using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using libzkfpcsharp;
using MySql.Data.MySqlClient;
namespace ojt
{
    public partial class Form5 : Form
    {
        MySqlConnection con;
        String connectString;
        String myTimein;
        TimeSpan TimeSpan;
        string myDate;
        String empId;
        IntPtr mDevHandle = IntPtr.Zero;
        Form oldform;

        public Form5(Form f1)
        {
            InitializeComponent();
            oldform = f1;
        }
        private void Form5_Load(object sender, EventArgs e)
        {
            this.myDate = DateTime.Today.ToString("dd-MM-yyyy");
            this.loadconnection();
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                this.con.Open();

                MySqlCommand cmdDb5 = new MySqlCommand("select * from ojt.student where emergency = '" + this.txtCode.Text +"' ", this.con);
                MySqlDataReader myreader5;
                myreader5 = cmdDb5.ExecuteReader();

                while (myreader5.Read())
                {
                    this.empId = myreader5.GetString("id");

                }
                this.con.Close();

                string myTime2 = DateTime.Now.ToString("HH:mm:ss");
                this.con.Open();

                MySqlCommand cmdDb2 = new MySqlCommand("select * from ojt.dtr where student_id = '" + this.empId + "' AND date='" + this.myDate + "' AND timeout = ''", this.con);
                MySqlDataReader myreader;
                myreader = cmdDb2.ExecuteReader();

                while (myreader.Read())
                {
                    this.myTimein = myreader.GetString("timein");

                }


                if (myreader.HasRows)
                {


                    this.con.Close();
                    this.con.Open();
                    
                    TimeSpan = Convert.ToDateTime(myTime2) - Convert.ToDateTime(this.myTimein);
                    double secondss = TimeSpan.TotalSeconds;
                    MySqlCommand cmdDb4 = new MySqlCommand("update ojt.dtr set timeout = '" + myTime2 + "',consume = '" + secondss + "' where student_id = '" + this.empId + "' AND timein='" + this.myTimein + "'", this.con);
                    myreader = cmdDb4.ExecuteReader();
                    MessageBox.Show("Time-out Successfully!!");
                    this.con.Close();


                    /**/
                  

                    try
                    {
                       

                       







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
                    MySqlCommand cmdDb3 = new MySqlCommand("insert into ojt.dtr (student_id,date,timein) values('" + this.empId + "','" + this.myDate + "','" + myTime1 + "')", this.con);
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

        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            oldform.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            this.txtCode.Text = "";
        }
    }
}
