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
    public partial class Form2 : Form
    {

        Form oldform;
        MySqlConnection con;
        string connectString;
        public Form2(Form f1)
        {
            InitializeComponent();

            oldform = f1;
        }

        /*
        private void Form2_Load(object sender, EventArgs e)
        {
           
            this.FormClosed += new FormClosedEventHandler(closed);
           
        }
       
        public void closed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
        */

        private void button2_Click(object sender, EventArgs e)
        {
            this.txtUsername.Text = "";
            this.txtPassword.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = this.txtUsername.Text;
            string password = this.txtPassword.Text;

            connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";

            MySqlConnection conDb = new MySqlConnection(connectString);
            MySqlCommand cmdDb = new MySqlCommand("select * from ojt.users where username='"+username+"' AND password='"+password+"' ;", conDb);
           
            MySqlDataReader myReader;

            conDb.Open();
            myReader = cmdDb.ExecuteReader();
            int count = 0;

            while (myReader.Read())
            {
                count += 1;

            }
            if(count == 1)
            {
                Form3 form3 = new Form3();
                form3.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Incorect Username and Password!");
            }

            conDb.Close();




        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            var main_form = new Form1();
            main_form.Show();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.dbConnection();

        }
        public void dbConnection()
        {
            connectString = "SERVER=localhost ;PORT=3306;DATABASE=ojt;UID=root;PASSWORD='';";
            try
            {
                con = new MySqlConnection();
                con.ConnectionString = connectString;
                con.Open();
               
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
