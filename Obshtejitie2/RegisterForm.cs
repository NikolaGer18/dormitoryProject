using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics.CodeAnalysis;

namespace Obshtejitie2
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
            
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            // Validate user inputs
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text) || string.IsNullOrWhiteSpace(textBox3.Text) || string.IsNullOrWhiteSpace(comboBox1.SelectedItem.ToString()) || string.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("Please fill all the required fields!");
                return;
            }

            // Store the user data in the database
            if (RegisterUser(textBox1.Text, textBox2.Text, comboBox1.SelectedItem.ToString(), textBox4.Text))
            {
                MessageBox.Show("Registration successful!");
                Hide();
                new LoginForm().Show(); 
            }
            else
            {
                MessageBox.Show("Registration failed!");
                Hide();
                new LoginForm().Show();
            }
        }

        private bool RegisterUser(string username, string password, string securityQuestion, string securityAnswer)
        {
            if (textBox2.Text == textBox3.Text)
            {
                SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
                SqlCommand cmd = new("INSERT INTO dbo.profile (profile_user, profile_pass, profile_s_q, profile_s_a) VALUES (@User, @Pass, @Q, @A)", con);
                cmd.Parameters.AddWithValue("@User", username);
                cmd.Parameters.AddWithValue("@Pass", password);
                cmd.Parameters.AddWithValue("@Q", securityQuestion);
                cmd.Parameters.AddWithValue("@A", securityAnswer);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            else
            {
                MessageBox.Show("The passwords do not match. Please try again.");
                return false;
            }
        }

        private void RegisterForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            new LoginForm().Show();
        }

        private void RegisterForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!comboBox1.SelectedItem.Equals("Choose a question:"))
            {
                comboBox1.Items.Remove("Choose a question:");
                textBox4.ReadOnly = false;
                textBox4.BackColor = SystemColors.Window;
            }
        }

      
    }
}
