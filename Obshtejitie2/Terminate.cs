using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
namespace Obshtejitie2
{
    public partial class Terminate : Form
    {
        SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
        public Terminate()
        {
            InitializeComponent();
        }

        private void Terminate_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            new AfterLogin().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please fill all the required fields!");

            }
            else
            {
                if (TerminateUser(textBox1.Text))
                {
                    MessageBox.Show("Record terminated!");
                   
                    string cmd = "INSERT INTO dbo.archive(archive_name, archive_pID, archive_PhoneNumber, archive_FamilyStatus, archive_TenantType) SELECT card_name, card_personalID, card_PhoneNumber, card_FamilyStatus, card_TenantType FROM dbo.card WHERE card_id = " + textBox1.Text;

                    
                    con.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd, con))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                    con.Close();
                    DeleteUser(textBox1.Text);
                    textBox1.Clear();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            string cmd = "SELECT *  FROM dbo.card";

            con.Open();

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd, con))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            con.Close();
        }

        private void DeleteUser(string ID)
        {
            SqlCommand dcmd = new("DELETE FROM dbo.card WHERE card_id =@ID", con);
            dcmd.Parameters.AddWithValue("@ID", ID );
            con.Open();
            dcmd.ExecuteNonQuery();
            con.Close();
        }
        private bool TerminateUser(string pID)
        {

            
            SqlCommand cmd = new("SELECT card_personalID FROM dbo.card WHERE card_id = @ID", con);
            cmd.Parameters.AddWithValue("@ID", pID);
            con.Open();
            object check = cmd.ExecuteScalar();
            con.Close();
            if (check != null)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Such record was not found!");
                return false;
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string cmd = "SELECT *  FROM dbo.archive";

            con.Open();

            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd, con))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            con.Close();
        }
    }
}
