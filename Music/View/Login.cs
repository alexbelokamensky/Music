using Microsoft.VisualBasic.ApplicationServices;
using Music.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Music
{
    public partial class Login : Form
    {
        public string UserName;
        bool isRegistration = false;
        public Login()
        {
            InitializeComponent();
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        public void btLogin_Click(object sender, EventArgs e)
        {
            if (!isRegistration)
            {
                using (var context = new DataBaseContext())
                {
                    UserName = tbxLogin.Text;
                    bool userExists = context.Users.Any(u => u.Nume == tbxLogin.Text && u.Parola == tbxPassword.Text);
                    if (userExists) DialogResult = DialogResult.OK;
                    else lUserNF.Visible = true;
                }
            }
            else
            {
                using(var context = new DataBaseContext())
                {
                    UserName = tbxLogin.Text;
                    Users user = new Users()
                    {
                        Id = context.Users.OrderBy(u => u.Id).Last().Id + 1,
                        Nume = tbxLogin.Text,
                        Parola = tbxPassword.Text,
                        Rol = "user"
                    };
                    context.Users.Add(user);
                    context.SaveChanges();
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (!isRegistration)
            {
                label1.Text = "Log in";
                btLogin.Text = "Registration";
                isRegistration = true;
            }
            else
            {
                btLogin.Text = "Log in";
                label1.Text = "Registration";
                isRegistration = false;
            }
        }
    }
}
