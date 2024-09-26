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
    public partial class Update : Form
    {
        public Update()
        {
            InitializeComponent();
        }

        private bool CheckID(string ID)
        {
            SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
            SqlCommand cmd = new("SELECT card_personalID FROM dbo.card WHERE card_personalID = @ID", con);
            cmd.Parameters.AddWithValue("@ID", ID);
            con.Open();
            object check = cmd.ExecuteScalar();
            con.Close();
            if (check != null)
            {
                MessageBox.Show("Record was found!");
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                button1.Enabled = true;
                textBox5.Enabled = false;
                button3.Enabled = false;
                con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
                string newcmd = "SELECT card_name, card_personalID, card_PhoneNumber  FROM dbo.card WHERE card_personalID = '" + ID +"'";

                con.Open();

                using (SqlDataAdapter adapter = new SqlDataAdapter(newcmd, con))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
                con.Close();
                dataGridView1.Columns[0].HeaderText = "Name";
                dataGridView1.Columns[1].HeaderText = "Id";
                dataGridView1.Columns[2].HeaderText = "Phone Number";
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
            CheckID(textBox5.Text);
        }

        private void Update_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            UpdateSystem(textBox1.Text,textBox2.Text,textBox3.Text,textBox5.Text);
        }

        private void UpdateSystem(string name,string id,string phone,string oldId)
        {
            SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
            SqlCommand cmd = new("UPDATE dbo.card SET card_name = @N, card_personalID=@ID, card_PhoneNumber = @P WHERE card_personalID = @OldID", con);
            cmd.Parameters.AddWithValue("@N", name);
            cmd.Parameters.AddWithValue("@ID", id);
            cmd.Parameters.AddWithValue("@OldID", oldId);
            cmd.Parameters.AddWithValue("@P", phone);
            con.Open();
            cmd.ExecuteNonQuery();
            cmd = new ("SELECT card_name, card_personalID, card_PhoneNumber  FROM dbo.card WHERE card_personalID = @Id",con);
            cmd.Parameters.AddWithValue("@Id", id);
            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
            {
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridView1.DataSource = dataTable;
            }
            con.Close();
            dataGridView1.Columns[0].HeaderText = "Name";
            dataGridView1.Columns[1].HeaderText = "Id";
            dataGridView1.Columns[2].HeaderText = "Phone Number";
            con.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            new AfterLogin().Show();
        }
    }
}
