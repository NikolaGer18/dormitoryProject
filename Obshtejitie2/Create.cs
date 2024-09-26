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
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || comboBox1.SelectedItem.Equals("Select:") || string.IsNullOrWhiteSpace(textBox3.Text) || comboBox2.SelectedItem.Equals("Select:") || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill all the required fields!");
                
            }
            else 
            {
                StudentUser(textBox1.Text, textBox3.Text, comboBox2.SelectedItem.ToString(), textBox4.Text, comboBox1.SelectedItem.ToString(),textBox2.Text);
                MessageBox.Show("Data Entered Successfully!");
                Hide();
                new AfterLogin().Show();
            }
             
         
            
        }
        private void StudentUser(string name, string personalID, string FamilyStatus, string PhoneNumber, string TenantType, string Payment)
        {
            SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
            SqlCommand cmd = new("INSERT INTO dbo.card (card_name, card_personalID, card_FamilyStatus, card_PhoneNumber, card_TenantType, card_Payment) VALUES (@User, @Pass, @Q, @A, @T, @P)", con);
            cmd.Parameters.AddWithValue("@User", name);
            cmd.Parameters.AddWithValue("@Pass", personalID);
            cmd.Parameters.AddWithValue("@Q", FamilyStatus);
            cmd.Parameters.AddWithValue("@A", PhoneNumber);
            cmd.Parameters.AddWithValue("@T", TenantType);
            cmd.Parameters.AddWithValue("@P", Payment);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBox1.SelectedItem.Equals("Select:"))
            {
                comboBox1.Items.Remove("Select:");
                
            }
        }

        private void Student_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            new AfterLogin().Show();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBox2.SelectedItem.Equals("Select:"))
            {
                comboBox2.Items.Remove("Select:");

            }
        }

        private void Create_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
    
}
