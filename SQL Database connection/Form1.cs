using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SQL_Database_connection
{
    public partial class Form1 : Form
    {
        string connectionString = "Data Source=LC21207XX\\SQLEXPRESS; Initial Catalog=Tasks;User ID=sa;Password=sa2023"; //Data source = Name of the location, Initial Catalog is the location of the data you want to connect to, then you enter your details to log in upon accessing it
        SqlConnection cnn; //Variables
        SqlCommand command; //Variables
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Size = new Size(187, 135);
        }

        //Opening the database with a button
        private void button1_Click(object sender, EventArgs e)
        {
            cnn = new SqlConnection(connectionString); //Set the cnn variable to connect to the database vairable
            cnn.Open(); //Open the data base

            cnn.Close(); //Close the data base for security

            button4.Visible = true;
            button5.Visible = true;

            button1.Visible = false;
        }

        //Runs the query
        private void button2_Click(object sender, EventArgs e)
        {
            SqlDataReader dataReader; //Inserts the data reader
            String queryString, Output = "";

            queryString = "SELECT * FROM dbo.TaskList1"; //Runs the code in the SQL database

            command = new SqlCommand(queryString, cnn);
            dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                Output = Output + dataReader.GetValue(0);
            }
            MessageBox.Show(Output);
            command.Dispose();
            cnn.Close();
        }

        //Inserts new items to a table
        private void button3_Click(object sender, EventArgs e)
        {
            //Does not work
            //cnn.Open();

            //SqlDataAdapter adapter = new SqlDataAdapter();
            //String Query = "";

            //Query = "INSERT INTO dbo.TaskList1(TaskID,TaskName,PersonInCharge,Deadline) values(" + ")";

            //command = new SqlCommand(Query, cnn);
            //adapter.InsertCommand = command;
            //adapter.InsertCommand.ExecuteNonQuery();
            //command.Dispose();
            //cnn.Close();

            //Works
            Random rnd = new Random();
            int TaskID = rnd.Next(0,100);

            if (String.IsNullOrEmpty(textBox2.Text) || String.IsNullOrEmpty(textBox3.Text) || String.IsNullOrEmpty(dateTimePicker1.Text))
            {
                MessageBox.Show("Some details are missing");
            }
            else
            {
                string Query = $"INSERT INTO TaskList1(TaskID, TaskName, PersonInCharge, Deadline) VALUES('{TaskID}','{textBox2.Text}','{textBox3.Text}','{dateTimePicker1.Text}')"; //Query too add your data 
                using (cnn = new SqlConnection(connectionString))
                {
                    cnn.Open();
                    command = new SqlCommand(Query, cnn);
                    int rows = command.ExecuteNonQuery();

                }
                command.Dispose();
                cnn.Close();
            }
            textBox2.Text = null;
            textBox3.Text = null;
            dateTimePicker1.Text = null;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Size = new Size(355, 256);

            button3.Visible = true;
            button4.Visible = false; 
            button5.Visible = false;

            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;

            textBox2.Visible = true;
            textBox3.Visible = true;
            dateTimePicker1.Visible = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Size = new Size(355, 256);

            button2.Visible = true;
            button4.Visible = false;
            button5.Visible = false;

            label4.Visible = true;
            label1.Visible = true;
            label2.Visible = true;
            label3.Visible = true;

            textBox1.Visible = true;
            textBox2.Visible = true;
            textBox3.Visible = true;
            dateTimePicker1.Visible = true;

            textBox2.Enabled = false;
            textBox3.Enabled = false;
            dateTimePicker1.Enabled = false;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            cnn.Open();

            SqlCommand cmm = new SqlCommand("Select TaskID, TaskName, PersonInCharge, Deadline from TaskList1 where TaskID =@TaskID", cnn);
            cmm.Parameters.AddWithValue("TaskID", textBox1.Text);
            SqlDataReader reader1;
            reader1 = cmm.ExecuteReader();
            if (reader1.Read())
            {
                textBox2.Text = reader1["TaskName"].ToString();
                textBox3.Text = reader1["PersonInCharge"].ToString();
                dateTimePicker1.Text = reader1["Deadline"].ToString();
            }
            else
            {
                MessageBox.Show("No data found");
            }
            cnn.Close();
        }
    }
}