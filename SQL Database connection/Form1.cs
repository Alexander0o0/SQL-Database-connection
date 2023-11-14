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

        }

        //Opening the database with a button
        private void button1_Click(object sender, EventArgs e)
        {
            cnn = new SqlConnection(connectionString); //Set the cnn variable to connect to the database vairable
            cnn.Open(); //Open the data base
            MessageBox.Show("Data base open"); //Check if it works

            cnn.Close(); //Close the data base for security
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
        private async void button3_Click(object sender, EventArgs e)
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
            string Query = $"INSERT INTO TaskList1(TaskID, TaskName, PersonInCharge, Deadline) VALUES('{textBox1.Text}','{textBox2.Text}','{textBox3.Text}','{dateTimePicker1.Text}')"; //Query too add your data 
            using (cnn = new SqlConnection(connectionString))
            {
                cnn.Open();
                command = new SqlCommand(Query, cnn);
                int rows = command.ExecuteNonQuery();

            }
            command.Dispose();
            cnn.Close();
        }
    }
}