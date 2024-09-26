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
    public partial class Payment : Form
    {
        
        public Payment()
        {
            InitializeComponent();
        }

        private void Payment_FormClosed(object sender, FormClosedEventArgs e)
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
                if (CheckID(textBox1.Text))
                {
                    Random r = new Random();
                    textBox3.Text = r.Next(1, 10).ToString();
                    textBox2.Text = r.Next(5,25).ToString();
                    textBox5.Text = (Convert.ToInt32(textBox2.Text) + Convert.ToInt32(textBox3.Text) + Convert.ToInt32(textBox4.Text)).ToString();
                    MessageBox.Show("Found record!");
                    SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
                    string cmd = "SELECT *  FROM dbo.card WHERE card_personalID = " + textBox1.Text;
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
            cmd.Parameters.AddWithValue("@ID", PersonalID);
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
        private void RentPay(string pay, string ID)
        {
            SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
            SqlCommand cmd = new("SELECT card_Payment FROM dbo.card WHERE card_personalID = @ID ", con);
            cmd.Parameters.AddWithValue("@ID", textBox1.Text);
            con.Open();
            string rent1 = cmd.ExecuteScalar().ToString();
            con.Close();
            string rent = (Convert.ToInt32(rent1) + Convert.ToInt32(pay)).ToString();
            cmd = new("UPDATE dbo.card SET card_Payment = @P WHERE card_personalID = @ID", con);
            cmd.Parameters.AddWithValue("@P", rent);
            cmd.Parameters.AddWithValue("@ID", ID);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
                RentPay(textBox5.Text, textBox1.Text);
                MessageBox.Show("Data Entered Successfully!");
                Hide();
                new AfterLogin().Show();
        }
    }
}
