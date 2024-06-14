using Guna.UI2.WinForms;
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
using System.Security.Cryptography;
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
        //кнопки для управления состоянием окна
        private void btClose_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void btMinimize_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
        //вход и регистрация
        public void btLogin_Click(object sender, EventArgs e)
        {
            if (!isRegistration)
            {
                using (var context = new DataBaseContext())
                {
                    UserName = tbxLogin.Text;
                    string password = tbxPassword.Text;

                    var user = context.Users.SingleOrDefault(u => u.Nume == tbxLogin.Text);

                    if (user != null && VerifyPassword(tbxPassword.Text, user.Parola))
                    {
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        lUserNF.Visible = true;
                        lUserNF.Text = "User not found";
                    }
                }
            }
            else
            {
                using (var context = new DataBaseContext())
                {
                    if (!context.Users.Any(u => u.Nume == tbxLogin.Text))
                    {
                        UserName = tbxLogin.Text;
                        Users user = new Users()
                        {
                            Id = context.Users.OrderBy(u => u.Id).Last().Id + 1,
                            Nume = tbxLogin.Text,
                            Parola = HashPassword(tbxPassword.Text),
                            Rol = "user"
                        };
                        context.Users.Add(user);
                        DialogResult = DialogResult.OK;
                        Playlists playlists = new Playlists()
                        {
                            Id = context.Playlists.OrderBy(p => p.Id).Last().Id + 1,
                            Denumirea = "Favorites",
                            IdUser = user.Id,
                        };
                        context.Playlists.Add(playlists);
                        context.SaveChanges();
                    }
                    else
                    {
                        lUserNF.Visible = true;
                        lUserNF.Text = "Username is taken";
                    }
                }
            }
        }
        //шифровка пароля
        static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        //расшифровка пароля
        static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
        //переключение на регистрацию
        private void label1_Click(object sender, EventArgs e)
        {
            if (!isRegistration)
            {
                label1.Text = "Log in";
                btLogin.Text = "Registration";
                isRegistration = true;
                label1.Location = new Point(140, 408);
            }
            else
            {
                btLogin.Text = "Log in";
                label1.Text = "Registration";
                isRegistration = false;
                label1.Location = new Point(92, 408);
            }
        }
    }
}
