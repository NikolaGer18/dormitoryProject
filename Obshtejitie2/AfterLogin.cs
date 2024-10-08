﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Obshtejitie2
{
    public partial class AfterLogin : Form
    {
        public AfterLogin()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Hide();
            new LoginForm().Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Hide();
            new Create().Show();
        }

        private void AfterLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
            new View().Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Hide();
            new Update().Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Hide();
            new Terminate().Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Hide();
            new Payment().Show();
        }
    }
}
