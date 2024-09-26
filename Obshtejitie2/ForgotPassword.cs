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
    public partial class ForgotPassword : Form
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        private void ForgotPassword_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void submitButton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
            SqlCommand cmd = new("SELECT profile_user from dbo.profile WHERE profile_user = @User",con);
            cmd.Parameters.AddWithValue("@User", textBox4.Text);
            con.Open();
            object check = cmd.ExecuteScalar();
            con.Close();
            if (check != null)
            {
                cmd = new("SELECT profile_s_q FROM dbo.profile WHERE profile_user = @User", con);
                cmd.Parameters.AddWithValue("@User", textBox4.Text);
                con.Open();
                label1.Text = cmd.ExecuteScalar().ToString();
                con.Close();
                textBox1.ReadOnly = false;
                textBox4.ReadOnly = true;
                submitButton.Dispose();
                button1.Visible = true;
                textBox1.BackColor = SystemColors.Window;
            }
            else
            {
                MessageBox.Show("An account with the given Username does not exist.");
            }
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == textBox2.Text)
            {
                SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
                SqlCommand cmd = new("UPDATE dbo.profile SET profile_pass = @NewPass WHERE profile_user = @User", con);
                cmd.Parameters.AddWithValue("@User", textBox4.Text);
                cmd.Parameters.AddWithValue("@NewPass", textBox3.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("The password has been changed successfully.");
                Hide();
                new LoginForm().Show();
            }
            else
            {
                MessageBox.Show("The passwords do not match. Please try again.");
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection con = new(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Admin\source\repos\Obshtejitie2\Obshtejitie2\Database1.mdf;Integrated Security=True");
            SqlCommand cmd = new("SELECT profile_user,profile_s_a from dbo.profile WHERE profile_user = @User AND profile_s_a = @A", con);
            cmd.Parameters.AddWithValue("@User", textBox4.Text);
            cmd.Parameters.AddWithValue("@A", textBox1.Text);
            con.Open();
            object check = cmd.ExecuteScalar();
            con.Close();
            if (check != null)
            {
                resetButton.Visible = true;
                button1.Dispose();
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox1.ReadOnly = true;
                textBox3.BackColor = SystemColors.Window;
                textBox2.BackColor = SystemColors.Window;

            }
            else
            {
                MessageBox.Show("Incorrect answer.");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            new LoginForm().Show();
        }
    }
}