using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace ojt
{
    public partial class Form4 : Form
    {
        int myId;
        string connectString;
        TimeSpan consume2 = TimeSpan.Zero;
        Double aw2;

        public Form4(int id)
        {
            InitializeComponent();
            this.myId = id;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.loadDataTable();
            this.loadFullName();
            this.sumHours();

        }

        public void loadDataTable()
        {
            connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";

            MySqlConnection conDb = new MySqlConnection(connectString);
            MySqlCommand cmdDb = new MySqlCommand("select date,timein,timeout,consume from ojt.dtr where student_id = '"+this.myId+"';", conDb);


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

        public void loadFullName()
        {
            connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";

            MySqlConnection conDb = new MySqlConnection(connectString);
            MySqlCommand cmdDb = new MySqlCommand("select first_name,last_name from ojt.student where id = '" + this.myId + "';", conDb);
            MySqlDataReader myreader;

            try
            {
                conDb.Open();
                myreader = cmdDb.ExecuteReader();

                while (myreader.Read())
                {
                    String f_name = myreader.GetString("first_name");
                    String l_name = myreader.GetString("last_name");
                    this.lblName.Text = f_name + " " + l_name;
                }

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }

        }

        public void sumHours()
        {
            connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";

            MySqlConnection conDb = new MySqlConnection(connectString);
            MySqlCommand cmdDb = new MySqlCommand("select sum(consume) from ojt.dtr where student_id = '" + this.myId + "';", conDb);
            MySqlDataReader myreader;
            try
            {
                conDb.Open();
                int aw = Int32.Parse(cmdDb.ExecuteScalar().ToString());
                int hours;
                int minutes;
                int seconds;

                seconds = aw;
                hours = (int)(Math.Floor((double)(seconds / 3600)));
                seconds = seconds % 3600;
                minutes = (int)(Math.Floor((double)(seconds / 60)));
                seconds = seconds % 60;
                string time = hours + ":" + minutes + ":" + seconds;

                this.lblSum.Text = time;



            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
           
           
            
            Bitmap bm = new Bitmap(this.dataGridView1.Width, this.dataGridView1.Height);
            dataGridView1.DrawToBitmap(bm, new Rectangle(0, 0, this.dataGridView1.Width, this.dataGridView1.Height));

            e.Graphics.DrawImage(bm, 200, 150);
           


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            printPreviewDialog1.Document = printDocument1;
            printPreviewDialog1.ShowDialog();
            //printDocument1.Print();
        }
    }
}
