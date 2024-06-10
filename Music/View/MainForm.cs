using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Music.Controllers;
using Music.Model;
using System;
using System.Diagnostics;
using System.Windows.Forms;
using static System.Windows.Forms.DataFormats;

namespace Music
{
    public partial class MainForm : Form
    {
        public string userName = "user";
        public int userId = 0;
        bool isMusic = true;
        AllMusicController allMusicController = new AllMusicController();
        UsersContoller usersContoller = new UsersContoller();
        MusicInPlaylistsController musicInPlaylistsController = new MusicInPlaylistsController();
        PlaylistsController playlistsController = new PlaylistsController();
        PlaylistsNumOfSongsController playlistsNumOfSongsController = new PlaylistsNumOfSongsController();
        PlaylistMusicController playlistMusicController = new PlaylistMusicController();
        public MainForm()
        {
            InitializeComponent();
            using (Login login = new Login())
            {
                if (login.ShowDialog() == DialogResult.OK) { userName = login.UserName; }
                else Close();
            }
            btUser.Text = userName;

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
            dgvPlaylists.Hide();
            dgvPlaylistsSongs.Show();
            btAddPlaylist.Hide();
            btModifyPlaylist.Hide();
            btDeletePlaylist.Hide();
            btDeleteFromPlaylist.Show();
            label3.Text = "Favorites";
            dgvPlaylistsSongs.DataSource = allMusicController.GetFavorites(userName);
            dgvPlaylistsSongs.Columns[0].Visible = false;
            dgvPlaylistsSongs.Columns[6].Visible = false;
            dgvPlaylistsSongs.ClearSelection();
        }
        private void btPlaylists_Click(object sender, EventArgs e)
        {
            pMusic.Hide();
            pNewSongs.Hide();
            pHomeFavorites.Hide();
            pControlPanel.Hide();
            pPlaylists.Show();
            dgvPlaylists.Show();
            dgvPlaylistsSongs.Hide();

            btAddPlaylist.Show();
            btModifyPlaylist.Show();
            btDeletePlaylist.Show();
            btDeleteFromPlaylist.Hide();
            label3.Text = "Playlists";
            dgvPlaylists.DataSource = playlistsNumOfSongsController.GetPlaylistsNumOfSongs(userId);
            dgvPlaylists.Columns[0].Visible = false;
            dgvPlaylists.ClearSelection();

        }
        private void btHome_Click(object sender, EventArgs e)
        {
            pMusic.Hide();
            pNewSongs.Show();
            pHomeFavorites.Show();
            pControlPanel.Hide();
            pPlaylists.Hide();

            using (var dataBaseContext = new DataBaseContext())
            {
                var musicPlaylist = dataBaseContext.AllMusic.FromSql($"SELECT AllMusic.* FROM AllMusic JOIN Playlist_Music ON AllMusic.id = Playlist_Music.IdMusic JOIN Playlists ON Playlist_Music.IdPlaylist = Playlists.Id JOIN Users ON Playlists.IdUser = Users.Id WHERE Playlists.Denumirea = 'Favorites' AND Users.Nume = {userName}").ToList();
                dgvHomeFavorites.DataSource = musicPlaylist;
            }
            dgvHomeFavorites.ClearSelection();
        }
        private void btMusic_Click(object sender, EventArgs e)
        {
            pNewSongs.Hide();
            pHomeFavorites.Hide();
            pPlaylists.Hide();
            pControlPanel.Hide();
            pMusic.Show();
            dgvMusicAlbum.Hide();
            dgvMusicArtist.Hide();
            dgvMusicName.Hide();
            guna2HtmlLabel1.Hide();
            guna2HtmlLabel2.Hide();
            guna2HtmlLabel3.Hide();
            guna2ComboBox1.DataSource = playlistsController.GetAllPlaylisrs(userId);
            guna2ComboBox1.DisplayMember = "Denumirea";
            guna2ComboBox1.ValueMember = "Id";
            dgvAllMusic.ClearSelection();
        }
        private void tbxFind_TextChanged(object sender, EventArgs e)
        {
            if (tbxFind.Text != "")
            {
                dgvAllMusic.Hide();
                dgvMusicAlbum.Show();
                dgvMusicArtist.Show();
                dgvMusicName.Show();
                guna2HtmlLabel1.Show();
                guna2HtmlLabel2.Show();
                guna2HtmlLabel3.Show();
                string find = tbxFind.Text.ToString();
                using (var dataBaseContext = new DataBaseContext())
                {
                    dgvMusicName.DataSource = allMusicController.GetFindedMusic(find, "Denumirea");
                    dgvMusicArtist.DataSource = allMusicController.GetFindedMusic(find, "Grupa");
                    dgvMusicAlbum.DataSource = allMusicController.GetFindedMusic(find, "Album");
                }
                dgvMusicName.Columns[0].Visible = false;
                dgvMusicName.Columns[6].Visible = false;
                dgvMusicArtist.Columns[0].Visible = false;
                dgvMusicArtist.Columns[6].Visible = false;
                dgvMusicAlbum.Columns[0].Visible = false;
                dgvMusicAlbum.Columns[6].Visible = false;
                dgvMusicName.ClearSelection();
                dgvMusicArtist.ClearSelection();
                dgvMusicAlbum.ClearSelection();
            }
            else
            {
                dgvAllMusic.Show();
                dgvMusicAlbum.Hide();
                dgvMusicArtist.Hide();
                dgvMusicName.Hide();
                guna2HtmlLabel1.Hide();
                guna2HtmlLabel2.Hide();
                guna2HtmlLabel3.Hide();
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

            Users user = usersContoller.GetUserByName(userName);
            userId = user.Id;
            dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted();
            dgvNewSongs.DataSource = allMusicController.GetRandomMusics();
            dgvHomeFavorites.DataSource = allMusicController.GetHomeFavorites(userName);            
            if (user.Rol == "admin") btControlPanel.Visible = true;    

            dgvAllMusic.Columns[0].Visible = false;
            dgvAllMusic.Columns[6].Visible = false;
            dgvNewSongs.Columns[0].Visible = false;
            dgvNewSongs.Columns[6].Visible = false;
            dgvHomeFavorites.Columns[0].Visible = false;
            dgvHomeFavorites.Columns[6].Visible = false;

            dgvAllMusic.ClearSelection();
            dgvNewSongs.ClearSelection();
            dgvHomeFavorites.ClearSelection();
        }
        private void btUser_Click(object sender, EventArgs e)
        {
            using (Login login = new Login())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    userName = login.UserName;
                    pMusic.Hide();
                    pNewSongs.Show();
                    pHomeFavorites.Show();
                    pControlPanel.Hide();
                    pPlaylists.Hide();
                    LoadInformation();
                }
            }
            btUser.Text = userName;
        }
        private void btControlPanel_Click(object sender, EventArgs e)
        {
            pHomeFavorites.Hide();
            pNewSongs.Hide();
            pPlaylists.Hide();
            pMusic.Hide();
            pControlPanel.Show();
            if (isMusic) dgvControlPanel.DataSource = allMusicController.GetAllMusic();
            else dgvControlPanel.DataSource = usersContoller.GetAllUsers();
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
            dgvControlPanel.DataSource = usersContoller.GetAllUsers();
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
            dgvControlPanel.DataSource = usersContoller.GetAllUsers();
            dgvControlPanel.ClearSelection();
            tbx1.Text = "";
            tbx2.Text = "";
            tbx3.Text = "";
        }
        private void btAdd_Click(object sender, EventArgs e)
        {
            if (isMusic)
            {
                AllMusic music = new AllMusic()
                {
                    Id = allMusicController.GetLastMusic().Id + 1,
                    Denumirea = tbx1.Text,
                    Grupa = tbx2.Text,
                    Album = tbx3.Text,
                    Gen = tbx4.Text,
                    An = Convert.ToInt32(tbx5.Text),
                    NumDeAscultari = Convert.ToInt32(tbx6.Text)
                };
                allMusicController.AddMusic(music);
                dgvControlPanel.DataSource = allMusicController.GetAllMusic();
            }
            else
            {
                Users users = new Users()
                {
                    Id = usersContoller.GetLastUser().Id + 1,
                    Nume = tbx1.Text,
                    Parola = tbx2.Text,
                    Rol = tbx3.Text,
                };
                usersContoller.AddUser(users);
                dgvControlPanel.DataSource = usersContoller.GetAllUsers();
            }
            dgvControlPanel.ClearSelection();
        }
        private void btDelete_Click(object sender, EventArgs e)
        {
            if (dgvControlPanel.SelectedRows.Count > 0)
            {
                var row = dgvControlPanel.SelectedRows[0];
                int id = (int)row.Cells["Id"].Value;
                using (var dataBaseContext = new DataBaseContext())
                {
                    if (isMusic)
                    {
                        AllMusic music = allMusicController.GetMusicById(id);
                        if (music != null)   allMusicController.DeleteMusic(music);
                        dgvControlPanel.DataSource = allMusicController.GetAllMusic();
                    }
                    else
                    {
                        Users users = usersContoller.GetUserById(id);
                        if (users != null)  usersContoller.DeleteUser(users);
                        dgvControlPanel.DataSource = usersContoller.GetAllUsers();
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
            if (isMusic)
            {
                allMusicController.ModifyMusic(id, tbx1.Text, tbx2.Text, tbx3.Text, tbx4.Text, Convert.ToInt32(tbx5.Text), Convert.ToInt32(tbx6.Text));
                dgvControlPanel.DataSource = allMusicController.GetAllMusic();
            }
            else
            {
                usersContoller.ModifyUser(id, tbx1.Text, tbx2.Text, tbx3.Text);
                dgvControlPanel.DataSource = usersContoller.GetAllUsers();
            }
            dgvControlPanel.ClearSelection();
        }
        private void dgvPlaylists_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            btAddPlaylist.Hide();
            btModifyPlaylist.Hide();
            btDeletePlaylist.Hide();
            btDeleteFromPlaylist.Show();
            var row = dgvPlaylists.SelectedRows[0];
            string denumirea = (string)row.Cells["Denumirea"].Value;
            dgvPlaylists.Hide();
            dgvPlaylistsSongs.Show();
            label3.Text = denumirea;
            dgvPlaylistsSongs.DataSource = musicInPlaylistsController.GetPlaylistSongs(denumirea, userId);
            dgvPlaylistsSongs.Columns[0].Visible = false;
            dgvPlaylistsSongs.ClearSelection();
        }
        private void btAddPlaylist_Click(object sender, EventArgs e)
        {
            using (PlaylistName playlistName = new PlaylistName())
            {
                if (playlistName.ShowDialog() == DialogResult.OK)
                {
                    playlistsController.AddPlaylist(playlistName.playlistName, userId); 
                    dgvPlaylists.DataSource = playlistsNumOfSongsController.GetPlaylistsNumOfSongs(userId);
                    dgvPlaylists.Columns[0].Visible = false;
                    dgvPlaylists.ClearSelection();
                }
            }
        }
        private void guna2Button2_Click(object sender, EventArgs e)
        {
            if (dgvPlaylists.SelectedRows.Count > 0)
            {
                var row = dgvPlaylists.SelectedRows[0];
                int id = (int)row.Cells["Id"].Value;
                using (var dataBaseContext = new DataBaseContext())
                {
                    playlistsController.DeletePlaylist(id);
                    dgvPlaylists.DataSource = playlistsNumOfSongsController.GetPlaylistsNumOfSongs(userId); 
                    dgvPlaylists.ClearSelection();
                }
            }
        }
        private void guna2Button1_Click(object sender, EventArgs e)
        {
            if (dgvPlaylists.SelectedRows.Count > 0)
            {
                using (PlaylistName playlistName = new PlaylistName())
                {
                    if (playlistName.ShowDialog() == DialogResult.OK)
                    {
                        var row = dgvPlaylists.SelectedRows[0];
                        int id = (int)row.Cells["Id"].Value;
                        playlistsController.ModifyPlaylist(userId, playlistName.playlistName);
                        dgvPlaylists.DataSource = playlistsNumOfSongsController.GetPlaylistsNumOfSongs(userId); 
                        dgvControlPanel.ClearSelection();
                    }
                }
            }
        }
        private void btAddToPlaylist_Click(object sender, EventArgs e)
        {
            playlistMusicController.AddPlaylistMusic(Convert.ToInt32(guna2ComboBox1.SelectedValue), Convert.ToInt32(dgvAllMusic.SelectedRows[0].Cells["Id"].Value));
        }
        private void btDeleteFromPlaylist_Click(object sender, EventArgs e)
        {
            if (dgvPlaylistsSongs.SelectedRows.Count > 0)
            {
                var row = dgvPlaylistsSongs.SelectedRows[0];
                int id = (int)row.Cells["Id"].Value;
                row = dgvPlaylists.SelectedRows[0];
                string denumirea = (string)row.Cells["Denumirea"].Value;
                playlistMusicController.DeletePlaylistMusic(id);
                dgvPlaylistsSongs.DataSource = musicInPlaylistsController.GetPlaylistSongs(denumirea, userId);
                dgvPlaylistsSongs.ClearSelection();
            }
        }
    }
}
