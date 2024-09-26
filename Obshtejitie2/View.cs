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
    public partial class View : Form
    {
        public View()
        {
            InitializeComponent();
        }



        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            new AfterLogin().Show();
        }

        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please fill all the required fields!");
                
            }
            else
            { 
                if(CheckID(textBox1.Text))
                {
                    MessageBox.Show("Found record!");
                    SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
                    string cmd = "SELECT *  FROM dbo.card WHERE card_personalID = " + textBox1.Text;
                    textBox1.Clear();
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
        private bool CheckID(string PersonalID)
        {
            SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
            SqlCommand cmd = new("SELECT card_personalID FROM dbo.card WHERE card_personalID = @ID", con);
            cmd.Parameters.AddWithValue("@ID",PersonalID);
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
            dataGridView1.DataSource = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
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
    }
}
