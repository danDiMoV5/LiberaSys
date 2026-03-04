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
    public partial class LoginForm : Form
    {
        SqlConnection connect = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;
            Initial Catalog=LiberaSysDB;
            Integrated Security=True; TrustServerCertificate=True");

        public LoginForm()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void signupBtn_Click(object sender, EventArgs e)
        {
            RegisterForm rForm = new RegisterForm();
            rForm.Show();
            this.Hide();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            login_password.PasswordChar = login_showPass.Checked ? '\0' : '*';
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (login_username.Text == "" || login_password.Text == "")
            {
                MessageBox.Show("Моля попълнете всички полета", "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (connect.State != ConnectionState.Open)
                {
                    try
                    {
                        connect.Open();

                        String selectData
                            = "SELECT * FROM users WHERE username = @username AND password = @password";
                        using (SqlCommand cmd = new SqlCommand(selectData, connect))
                        {
                            cmd.Parameters.AddWithValue("@username", login_username.Text.Trim());
                            cmd.Parameters.AddWithValue("@password", login_password.Text.Trim());

                            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                            DataTable table = new DataTable();
                            adapter.Fill(table);

                            if (table.Rows.Count >= 1)
                            {

                                MainForm mForm = new MainForm();
                                mForm.Show();
                                this.Hide();

                            }
                            else
                            {
                                MessageBox.Show("Грешно Потребителско име/Парола", "Грешка"
                                    , MessageBoxButtons.OK, MessageBoxIcon.Error);

                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Грешка при свързване с Базата данни: " + ex, "Грешка"
                            , MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        connect.Close();
                    }
                }
            }
        }


        private void LoginForm_Load(object sender, EventArgs e)
        {
            
        }

        private void label1_MouseHover(object sender, EventArgs e)
        {
            
        }
    }
}
