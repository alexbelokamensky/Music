using Microsoft.EntityFrameworkCore;
using Music.Model;
using Music.View;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Music
{
    public partial class MainForm : Form
    {
        public string user = "user";
        bool isMusic = true;
        public MainForm()
        {
            InitializeComponent();
            using (Login login = new Login())
            {
                if (login.ShowDialog() == DialogResult.OK) user = login.User;
                else Close();
            }
            btUser.Text = user;
        }

        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btFavorites_Click(object sender, EventArgs e)
        {
            dgvPlaylists.DataSource = "";
            pMusic.Hide();
            pNewSongs.Hide();
            pHomeFavorites.Hide();
            pControlPanel.Hide();
            pPlaylists.Show();
            label3.Text = "Favorites";
            AllMusicContext allMusicContext = new AllMusicContext();
            using (allMusicContext)
            {
                var query = allMusicContext.AllMusic.FromSql($"SELECT AllMusic.* FROM AllMusic JOIN Playlist_Music ON AllMusic.id = Playlist_Music.IdMusic JOIN Playlists ON Playlist_Music.IdPlaylist = Playlists.Id JOIN Users ON Playlists.IdUser = Users.Id WHERE Playlists.Denumirea = 'fff' AND Users.Nume = {user}");
                var allMusicList = query.ToList();
                dgvPlaylists.DataSource = allMusicList;

            }
            dgvPlaylists.Columns[0].Visible = false;
            dgvPlaylists.Columns[6].Visible = false;
        }

        private void btPlaylists_Click(object sender, EventArgs e)
        {
            pMusic.Hide();
            pNewSongs.Hide();
            pHomeFavorites.Hide();
            pControlPanel.Hide();
            pPlaylists.Show();
            label3.Text = "Playlists";
            dgvPlaylists.DataSource = "";
        }

        private void btHome_Click(object sender, EventArgs e)
        {
            pMusic.Hide();
            pNewSongs.Show();
            pHomeFavorites.Show();
            pControlPanel.Hide();
            pPlaylists.Hide();
        }

        private void btMusic_Click(object sender, EventArgs e)
        {
            pNewSongs.Hide();
            pHomeFavorites.Hide();
            pPlaylists.Hide();
            pControlPanel.Hide();
            pMusic.Show();
        }

        private void tbxFind_TextChanged(object sender, EventArgs e)
        {
            if (tbxFind.Text != "")
            {
                dgvAllMusic.Hide();
            }
            else
            {
                dgvAllMusic.Show();
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadInformation();
        }

        private void LoadInformation()
        {
            pPlaylists.Hide();
            pMusic.Hide();
            pControlPanel.Hide();
            btControlPanel.Hide();
            Random random = new Random();
            using (var allMusicContext = new AllMusicContext())
            {
                var allMusic = allMusicContext.AllMusic.FromSql($"SELECT * FROM AllMusic ORDER BY Denumirea ASC").ToList();
                dgvAllMusic.DataSource = allMusic;

                List<AllMusic> randomMusic = new List<AllMusic>();
                for (int i = 0; i < 6; i++) randomMusic.Add(allMusic[random.Next(1, allMusic.Count)]);
                dgvNewSongs.DataSource = randomMusic;

                var musicPlaylist = allMusicContext.AllMusic.FromSql($"SELECT AllMusic.* FROM AllMusic JOIN Playlist_Music ON AllMusic.id = Playlist_Music.IdMusic JOIN Playlists ON Playlist_Music.IdPlaylist = Playlists.Id JOIN Users ON Playlists.IdUser = Users.Id WHERE Playlists.Denumirea = 'fff' AND Users.Nume = {user} LIMIT 5").ToList();
                dgvHomeFavorites.DataSource = musicPlaylist;

                var users1 = allMusicContext.Users.FromSql($"Select * FROM Users WHERE Nume = {user}");
                Users users = users1.First();
                if (users.Rol == "admin") btControlPanel.Visible = true;
            }
            dgvAllMusic.Columns[0].Visible = false;
            dgvAllMusic.Columns[6].Visible = false;
            dgvNewSongs.Columns[0].Visible = false;
            dgvNewSongs.Columns[6].Visible = false;
            dgvHomeFavorites.Columns[0].Visible = false;
            dgvHomeFavorites.Columns[6].Visible = false;
        }

        private void btUser_Click(object sender, EventArgs e)
        {
            using (Login login = new Login())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    user = login.User;
                    LoadInformation();
                }
            }
            btUser.Text = user;
        }

        private void btControlPanel_Click(object sender, EventArgs e)
        {
            pControlPanel.Show();
            using (var allMusicContext = new AllMusicContext())
            {
                if(isMusic) dgvControlPanel.DataSource = allMusicContext.AllMusic.FromSql($"SELECT * FROM AllMusic").ToList();
                else dgvControlPanel.DataSource = allMusicContext.Users.FromSql($"SELECT * FROM Users").ToList();
            }
            dgvControlPanel.ClearSelection();
            tbx1.Text = "";
            tbx2.Text = "";
            tbx3.Text = "";
            tbx4.Text = "";
            tbx5.Text = "";
            tbx6.Text = "";
        }

        private void btControlsMusic_Click(object sender, EventArgs e)
        {
            isMusic = true;
            tbx4.Show();
            tbx5.Show();
            tbx6.Show();
            tbx1.PlaceholderText = "Denumirea:";
            tbx2.PlaceholderText = "Grupa:";
            tbx3.PlaceholderText = "Album:";
            using (var allMusicContext = new AllMusicContext())
            {
                var allMusic = allMusicContext.AllMusic.FromSql($"SELECT * FROM AllMusic").ToList();
                dgvControlPanel.DataSource = allMusic;
            }
            dgvControlPanel.ClearSelection();
            tbx1.Text = "";
            tbx2.Text = "";
            tbx3.Text = "";
            tbx4.Text = "";
            tbx5.Text = "";
            tbx6.Text = "";
        }

        private void btControlsUsers_Click(object sender, EventArgs e)
        {
            isMusic = false;
            tbx4.Hide();
            tbx5.Hide();
            tbx6.Hide();
            tbx1.PlaceholderText = "Nume";
            tbx2.PlaceholderText = "Parola";
            tbx3.PlaceholderText = "Rol";
            using (var allMusicContext = new AllMusicContext())
            {
                var allUsers = allMusicContext.Users.FromSql($"SELECT * FROM Users").ToList();
                dgvControlPanel.DataSource = allUsers;
            }
            dgvControlPanel.ClearSelection();
            tbx1.Text = "";
            tbx2.Text = "";
            tbx3.Text = "";
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            using (var allMusicContext = new AllMusicContext())
            {
                if (isMusic)
                {

                    AllMusic lastMusic = allMusicContext.AllMusic.OrderBy(m => m.Id).Last();

                    AllMusic allMusic = new AllMusic()
                    {
                        Id = lastMusic.Id + 1,
                        Denumirea = tbx1.Text,
                        Grupa = tbx2.Text,
                        Album = tbx3.Text,
                        Gen = tbx4.Text,
                        An = Convert.ToInt32(tbx5.Text),
                        NumDeAscultari = Convert.ToInt32(tbx6.Text)
                    };
                    allMusicContext.AllMusic.Add(allMusic);
                    allMusicContext.SaveChanges();
                    dgvControlPanel.DataSource = allMusicContext.AllMusic.FromSql($"SELECT * FROM AllMusic").ToList();
                }
                else
                {
                    Users lastUser = allMusicContext.Users.OrderBy(u => u.Id).Last();

                    Users users = new Users()
                    {
                        Id = lastUser.Id + 1,
                        Nume = tbx1.Text,
                        Parola = tbx2.Text,
                        Rol = tbx3.Text,
                    };
                    allMusicContext.Users.Add(users);
                    allMusicContext.SaveChanges();
                    dgvControlPanel.DataSource = allMusicContext.Users.FromSql($"SELECT * FROM Users").ToList();
                }
                dgvControlPanel.ClearSelection();
            }
        }
        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dgvControlPanel.SelectedRows.Count > 0)
            {
                var row = dgvControlPanel.SelectedRows[0];
                int id = (int)row.Cells["Id"].Value; 
                using (var allMusicContext = new AllMusicContext())
                {
                    if (isMusic)
                    {
                        AllMusic allMusic = allMusicContext.AllMusic.FirstOrDefault(m => m.Id == id);
                        if (allMusic != null)
                        {
                            allMusicContext.AllMusic.Remove(allMusic);
                            allMusicContext.SaveChanges();
                        }
                        dgvControlPanel.DataSource = allMusicContext.AllMusic.FromSql($"SELECT * FROM AllMusic").ToList();
                    }
                    else
                    {
                        Users users = allMusicContext.Users.FirstOrDefault(u => u.Id == id);
                        if (users != null)
                        {
                            allMusicContext.Users.Remove(users);
                            allMusicContext.SaveChanges();
                        }
                        dgvControlPanel.DataSource = allMusicContext.Users.FromSql($"SELECT * FROM Users").ToList();
                    }
                    dgvControlPanel.ClearSelection();
                }
            }
        }

        private void dgvControlPanel_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvControlPanel.SelectedRows.Count > 0)
            {
                var row = dgvControlPanel.SelectedRows[0];
                tbx1.Text = row.Cells[1].Value.ToString();
                tbx2.Text = row.Cells[2].Value.ToString();
                tbx3.Text = row.Cells[3].Value.ToString();
                if (isMusic)
                {
                    tbx4.Text = row.Cells[4].Value.ToString();
                    tbx5.Text = row.Cells[5].Value.ToString();
                    tbx6.Text = row.Cells[6].Value.ToString();
                }
            }
            else
            {
                tbx1.Text = "";
                tbx2.Text = "";
                tbx3.Text = "";
                tbx4.Text = "";
                tbx5.Text = "";
                tbx6.Text = "";
            }
        }

        private void btModify_Click(object sender, EventArgs e)
        {
            var row = dgvControlPanel.SelectedRows[0];
            int id = (int)row.Cells["Id"].Value;
            using (var allMusicContext = new AllMusicContext())
            {
                if (isMusic)
                {
                    AllMusic allMusic = allMusicContext.AllMusic.FirstOrDefault(m => m.Id == id);
                    if (allMusic != null)
                    {
                        allMusic.Denumirea = tbx1.Text;
                        allMusic.Grupa = tbx2.Text;
                        allMusic.Album = tbx3.Text;
                        allMusic.Gen = tbx4.Text;
                        allMusic.An = Convert.ToInt32(tbx5.Text);
                        allMusic.NumDeAscultari = Convert.ToInt32(tbx6.Text);
                        allMusicContext.SaveChanges();
                    }
                    dgvControlPanel.DataSource = allMusicContext.AllMusic.FromSql($"SELECT * FROM AllMusic").ToList();
                }
                else
                {
                    Users users = allMusicContext.Users.FirstOrDefault(u => u.Id == id);
                    if (users != null)
                    {
                        users.Nume = tbx1.Text;
                        users.Parola = tbx2.Text;
                        users.Rol = tbx3.Text;
                        allMusicContext.SaveChanges();
                    }
                    dgvControlPanel.DataSource = allMusicContext.Users.FromSql($"SELECT * FROM Users").ToList();
                }
                dgvControlPanel.ClearSelection();
            }
        }
    }
}
