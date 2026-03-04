using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace LibraryManagementSystem
{
    public partial class RegisterForm : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=LiberaSysDB;Integrated Security=True");

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void signIn_btn_Click(object sender, EventArgs e)
        {
            LoginForm lForm = new LoginForm();
            lForm.Show();
            this.Hide();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void register_btn_Click(object sender, EventArgs e)
        {
            if(register_email.Text == "" || register_username.Text == "" || register_password.Text == "")
            {
                MessageBox.Show("Моля попълнете всички полета", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if(connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();

                        String checkUsername = "SELECT COUNT(*) FROM users WHERE username = @username";

                        using(SqlCommand checkCMD = new SqlCommand(checkUsername, connect))
                        {
                            checkCMD.Parameters.AddWithValue("@username", register_username.Text.Trim());
                            int count = (int)checkCMD.ExecuteScalar();

                            if(count >= 1)
                            {
                                MessageBox.Show(register_username.Text.Trim() 
                                    + " е вече взето", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DateTime day = DateTime.Today;

                                String insertData = "INSERT INTO users (email, username, password, date_register) " +
                                    "VALUES(@email, @username, @password, @date)";

                                using (SqlCommand insertCMD = new SqlCommand(insertData, connect))
                                {
                                    insertCMD.Parameters.AddWithValue("@email", register_email.Text.Trim());
                                    insertCMD.Parameters.AddWithValue("@username", register_username.Text.Trim());
                                    insertCMD.Parameters.AddWithValue("@password", register_password.Text.Trim());
                                    insertCMD.Parameters.AddWithValue("@date", day);

                                    insertCMD.ExecuteNonQuery();

                                    MessageBox.Show("Успешна регистрация!", "Информация"
                                        , MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    LoginForm lForm = new LoginForm();
                                    lForm.Show();
                                    this.Hide();
                                }
                            }
                        }

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Грешка при свързване с Базата данни: " + ex, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }

        private void register_showPass_CheckedChanged(object sender, EventArgs e)
        {
            register_password.PasswordChar = register_showPass.Checked ? '\0' : '*';
        }

        private void label2_Click(object sender, EventArgs e)
        {
            
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void register_email_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

/* 
sqllocaldb info  in CMD to check if LocalDB exists
"(localdb)\MSSQLLocalDB" - connection string


CREATE DATABASE LiberaSysDB;
 
GO
USE LiberaSysDB;
 
CREATE TABLE users 
( 
id INT PRIMARY KEY IDENTITY(1,1), 
email VARCHAR(MAX) NULL, 
username VARCHAR(MAX) NULL, 
password VARCHAR(MAX) NULL, 
date_register DATE NULL 
) 
SELECT * FROM users 

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
image VARCHAR(MAX) NULL, 
) 
SELECT * FROM books 


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
) 
SELECT * FROM issues
 */
