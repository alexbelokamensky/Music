using Music.View;
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
        public string User;
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
            using (var context = new AllMusicContext())
            {
                User = tbxLogin.Text;
                bool userExists = context.Users.Any(u => u.Nume == tbxLogin.Text && u.Parola == tbxPassword.Text);
                if (userExists) DialogResult = DialogResult.OK;
                else lUserNF.Visible = true;
            }
        }
    }
}
