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
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;

            // Validate username and password
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter your username and password");
                return;
            }

            // Check if the user exists in the database
            if (CheckUserExistence(username, password))
            {
                
                MessageBox.Show("Login successful!");
                textBox1.Clear();
                textBox2.Clear();
                Hide();
                new AfterLogin().Show();
                
            }
            else
            {
                MessageBox.Show("Invalid username or password!");
                textBox1.Clear();
                textBox2.Clear();
            }
        }

        private bool CheckUserExistence(string username, string password)
        {
            SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
            SqlCommand cmd = new("SELECT profile_user FROM dbo.profile WHERE profile_user = @User AND profile_pass = @Pass", con);
            cmd.Parameters.AddWithValue("@User", username);
            cmd.Parameters.AddWithValue("@Pass", password);
            con.Open();
            object check = cmd.ExecuteScalar();
            con.Close();
            if (check != null)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            new RegisterForm().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            new ForgotPassword().Show();
        }

        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}