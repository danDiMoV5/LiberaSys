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

namespace LibraryManagementSystem
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            panel2.Width += 6;

            if(panel2.Width >= 575)
            {
                timer1.Stop();

                LoginForm lForm = new LoginForm();
                lForm.Show();
                this.Hide();

            }
        }

        private void EnsureDatabase()
        {
            string masterConnectionString =
                @"Data Source=(LocalDB)\MSSQLLocalDB;
          Initial Catalog=master;
          Integrated Security=True;";

            using (SqlConnection connection = new SqlConnection(masterConnectionString))
            {
                connection.Open();

                string query = @"
        IF DB_ID('LiberaSysDB') IS NULL
        BEGIN
            CREATE DATABASE LiberaSysDB;
        END";

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            using (SqlConnection dbConnection = new SqlConnection(
                @"Data Source=(LocalDB)\MSSQLLocalDB;
          Initial Catalog=LiberaSysDB;
          Integrated Security=True;"))
            {
                dbConnection.Open();

                string tablesQuery = @"

        IF OBJECT_ID('users', 'U') IS NULL
        CREATE TABLE users 
        ( 
            id INT PRIMARY KEY IDENTITY(1,1), 
            email VARCHAR(MAX) NULL, 
            username VARCHAR(MAX) NULL, 
            password VARCHAR(MAX) NULL, 
            date_register DATE NULL 
        )

        IF OBJECT_ID('books', 'U') IS NULL
        CREATE TABLE books 
        ( 
            id INT PRIMARY KEY IDENTITY(1,1), 
            book_title VARCHAR(MAX) NULL, 
            author VARCHAR(MAX) NULL, 
            published_date DATE NULL, 
            status VARCHAR(MAX) NULL, 
            date_insert DATE NULL, 
            date_update DATE NULL, 
            date_delete DATE NULL, 
            image VARCHAR(MAX) NULL
        )

        IF OBJECT_ID('issues', 'U') IS NULL
        CREATE TABLE issues 
        ( 
            id INT PRIMARY KEY IDENTITY(1,1), 
            issue_id VARCHAR(MAX) NULL, 
            full_name VARCHAR(MAX) NULL, 
            contact VARCHAR(MAX) NULL, 
            email VARCHAR(MAX) NULL, 
            book_title VARCHAR(MAX) NULL, 
            author VARCHAR(MAX) NULL, 
            status VARCHAR(MAX) NULL, 
            issue_date DATE NULL, 
            return_date DATE NULL, 
            date_insert DATE NULL, 
            date_update DATE NULL, 
            date_delete DATE NULL 
        )";

                using (SqlCommand cmd = new SqlCommand(tablesQuery, dbConnection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EnsureDatabase();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
