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
    public partial class Dashboard : UserControl
    {
        SqlConnection connect = new SqlConnection(
            @"Data Source=(LocalDB)\MSSQLLocalDB;
            Initial Catalog=LiberaSysDB;
            Integrated Security=True");

        public Dashboard()
        {
            InitializeComponent();

            displayAB();
            displayIB();
            displayRB();
        }

        public void refreshData()
        {

            if (InvokeRequired)
            {
                Invoke((MethodInvoker)refreshData);
                return;
            }

            displayAB();
            displayIB();
            displayRB();
        }

        public void displayAB()
        {
            if(connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM books " +
                        "WHERE status = 'Available' AND date_delete IS NULL";

                    using(SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempAB = 0;

                        if (reader.Read())
                        {
                            tempAB = Convert.ToInt32(reader[0]);

                            dashboard_AB.Text = tempAB.ToString();
                        }
                    }
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Грешка: " + ex, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayIB()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM issues " +
                        "WHERE date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempIB = 0;

                        if (reader.Read())
                        {
                            tempIB = Convert.ToInt32(reader[0]);

                            dashboard_IB.Text = tempIB.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка: " + ex, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        public void displayRB()
        {
            if (connect.State == ConnectionState.Closed)
            {
                try
                {
                    connect.Open();
                    string selectData = "SELECT COUNT(id) FROM issues " +
                        " WHERE status = 'Return' AND date_delete IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();
                        int tempRB = 0;

                        if (reader.Read())
                        {
                            tempRB = Convert.ToInt32(reader[0]);

                            dashboard_RB.Text = tempRB.ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Грешка: " + ex, "Грешка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    connect.Close();
                }
            }
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }

        private void dashboard_RB_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
