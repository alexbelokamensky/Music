using Guna.UI2.WinForms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        bool SortAsc = true;
        //присоединение всх контроллеров
        AllMusicController allMusicController = new AllMusicController();
        UsersContoller usersContoller = new UsersContoller();
        MusicInPlaylistsController musicInPlaylistsController = new MusicInPlaylistsController();
        PlaylistsController playlistsController = new PlaylistsController();
        PlaylistsNumOfSongsController playlistsNumOfSongsController = new PlaylistsNumOfSongsController();
        PlaylistMusicController playlistMusicController = new PlaylistMusicController();
        public MainForm()
        {
            InitializeComponent();
            //вход пользователя
            using (Login login = new Login())
            {
                if (login.ShowDialog() == DialogResult.OK) { userName = login.UserName; }
                else Close();
            }
            btUser.Text = userName;
            //скрытие лишних панелей и проверка администратора
            pPlaylists.Hide();
            pMusic.Hide();
            pControlPanel.Hide();
            btControlPanel.Hide();
            pRaports.Hide();
            pRaports.Hide();
            Users user = usersContoller.GetUserByName(userName);
            userId = user.Id;
            if (user.Rol == "admin") btControlPanel.Visible = true;
            //заполнение таблиц и скрытие лишних столбцов
            dgvNewSongs.DataSource = allMusicController.GetRandomMusics();
            dgvHomeFavorites.DataSource = allMusicController.GetHomeFavorites(userName);
            dgvNewSongs.Columns[0].Visible = false;
            dgvNewSongs.Columns[6].Visible = false;
            dgvNewSongs.Columns[7].Visible = false;
            dgvHomeFavorites.Columns[0].Visible = false;
            dgvHomeFavorites.Columns[6].Visible = false;
            dgvHomeFavorites.Columns[7].Visible = false;
            //удаление выделения
            dgvAllMusic.ClearSelection();
            dgvNewSongs.ClearSelection();
            dgvHomeFavorites.ClearSelection();
        }
        //кнопки для управления состоянием окна
        private void btClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void btMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        //
        //переход по вкладкам
        //
        private void btFavorites_Click(object sender, EventArgs e)
        {
            //скрытие лишних панелей
            pMusic.Hide();
            pNewSongs.Hide();
            pHomeFavorites.Hide();
            pControlPanel.Hide();
            pRaports.Hide();
            pPlaylists.Show();
            //замена таблицы
            dgvPlaylists.Hide();
            dgvPlaylistsSongs.Show();
            //замена кнопок
            btAddPlaylist.Hide();
            btModifyPlaylist.Hide();
            btDeletePlaylist.Hide();
            btDeleteFromPlaylist.Show();

            label3.Text = "Favorites";
            guna2PictureBox3.Image = Image.FromFile("../../../images/favorites.png");
            //вывод музыки из плейлиста
            dgvPlaylistsSongs.DataSource = allMusicController.GetFavorites(userName);
            dgvPlaylistsSongs.Columns[0].Visible = false;
            dgvPlaylistsSongs.Columns[6].Visible = false;
            dgvPlaylistsSongs.Columns[7].Visible = false;
            dgvPlaylistsSongs.ClearSelection();
        }
        private void btPlaylists_Click(object sender, EventArgs e)
        {
            //скрытие лишних панелей
            pMusic.Hide();
            pNewSongs.Hide();
            pHomeFavorites.Hide();
            pControlPanel.Hide();
            pRaports.Hide();
            pPlaylists.Show();
            //замена таблицы
            dgvPlaylists.Show();
            dgvPlaylistsSongs.Hide();
            //замена кнопок
            btAddPlaylist.Show();
            btModifyPlaylist.Show();
            btDeletePlaylist.Show();
            btDeleteFromPlaylist.Hide();

            label3.Text = "Playlists";
            guna2PictureBox3.Image = Image.FromFile("../../../images/playlist.png");
            //вывод плейлистов пользователя
            dgvPlaylists.DataSource = playlistsNumOfSongsController.GetPlaylistsNumOfSongs(userId);
            dgvPlaylists.Columns[0].Visible = false;
            dgvPlaylists.ClearSelection();

        }
        private void btHome_Click(object sender, EventArgs e)
        {
            //скрытие лишних панелей
            pMusic.Hide();
            pNewSongs.Show();
            pHomeFavorites.Show();
            pControlPanel.Hide();
            pPlaylists.Hide();
            pRaports.Hide();
            //вывод музыки из плейлиста
            dgvHomeFavorites.DataSource = allMusicController.GetHomeFavorites(userName);
            dgvHomeFavorites.ClearSelection();
        }
        private void btMusic_Click(object sender, EventArgs e)
        {
            tbxFind.Text = "";
            //скрытие лишних панелей
            pNewSongs.Hide();
            pHomeFavorites.Hide();
            pPlaylists.Hide();
            pControlPanel.Hide();
            pMusic.Show();
            pRaports.Hide();
            //скрытие лишних таблиц и текстов
            dgvMusicAlbum.Hide();
            dgvMusicArtist.Hide();
            dgvMusicName.Hide();
            guna2HtmlLabel1.Hide();
            guna2HtmlLabel2.Hide();
            guna2HtmlLabel3.Hide();
            //вывод всей музыки и настройка таблицы
            dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("Denumirea", "ASC");
            dgvAllMusic.Columns[4].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAllMusic.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAllMusic.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvAllMusic.Columns[0].Visible = false;
            dgvAllMusic.Columns[6].Visible = false;
            //вывод всех плейлистов пользователя
            guna2ComboBox1.DataSource = playlistsController.GetAllPlaylisrs(userId);
            guna2ComboBox1.DisplayMember = "Denumirea";
            guna2ComboBox1.ValueMember = "Id";
            dgvAllMusic.ClearSelection();
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
                    pRaports.Hide();
                    dgvNewSongs.DataSource = allMusicController.GetRandomMusics();
                    dgvHomeFavorites.DataSource = allMusicController.GetHomeFavorites(userName);
                    Users user = usersContoller.GetUserByName(userName);
                    userId = user.Id;
                    if (user.Rol == "admin") btControlPanel.Visible = true;
                    else btControlPanel.Visible = false;
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
            pRaports.Hide();
            pControlPanel.Show();
            tbx4.Show();
            tbx5.Show();
            tbx6.Show();
            dgvControlPanel.DataSource = allMusicController.GetAllMusic();
            dgvControlPanel.ClearSelection();
            tbx1.Text = "";
            tbx2.Text = "";
            tbx3.Text = "";
            tbx4.Text = "";
            tbx5.Text = "";
            tbx6.Text = "";
            btControlsMusic.Enabled = false;
            btControlsUsers.Enabled = true;
        }
        private void btRaports_Click(object sender, EventArgs e)
        {
            pMusic.Hide();
            pNewSongs.Hide();
            pHomeFavorites.Hide();
            pControlPanel.Hide();
            pRaports.Show();
            pPlaylists.Hide();
            guna2ComboBox3.Hide();
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                dgvRaports.DataSource = dataBaseContext.AllMusic.FromSqlRaw($"SELECT * FROM AllMusic ORDER BY Gen DESC").ToList();
            }
            dgvRaports.Columns[0].Visible = false;
            dgvRaports.ClearSelection();
        }
        //
        //вкладка с музыкой
        //
            //поисковая система
        private void tbxFind_TextChanged(object sender, EventArgs e)
        {
            if (tbxFind.Text != "")
            {
                //вывод таблиц с данными и текста при начале поиска
                dgvAllMusic.Hide();
                dgvMusicAlbum.Show();
                dgvMusicArtist.Show();
                dgvMusicName.Show();
                guna2HtmlLabel1.Show();
                guna2HtmlLabel2.Show();
                guna2HtmlLabel3.Show();
                //скрытие сортировки
                guna2Button2.Hide();
                guna2Button3.Hide();
                guna2Button4.Hide();
                guna2Button5.Hide();
                guna2Button6.Hide();
                guna2Button7.Hide();
                //поиск
                string find = tbxFind.Text.ToString();
                using (var dataBaseContext = new DataBaseContext())
                {
                    dgvMusicName.DataSource = allMusicController.GetFindedMusic(find, "Denumirea");
                    dgvMusicArtist.DataSource = allMusicController.GetFindedMusic(find, "Grupa");
                    dgvMusicAlbum.DataSource = allMusicController.GetFindedMusic(find, "Album");
                }
                //настройка таблиц
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
                //скрытие лишних таблиц и текста
                dgvAllMusic.Show();
                dgvMusicAlbum.Hide();
                dgvMusicArtist.Hide();
                dgvMusicName.Hide();
                guna2HtmlLabel1.Hide();
                guna2HtmlLabel2.Hide();
                guna2HtmlLabel3.Hide();
                //показ сортировки
                guna2Button2.Show();
                guna2Button3.Show();
                guna2Button4.Show();
                guna2Button5.Show();
                guna2Button6.Show();
                guna2Button7.Show();
            }

        }
            //сортировка музыки
        private void btSortDenumirea_Click(object sender, EventArgs e)
        {
            if (SortAsc)
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("Denumirea", "ASC");
                SortAsc = false;
            }
            else
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("Denumirea", "DESC");
                SortAsc = true;
            }
            dgvAllMusic.ClearSelection();
        }
        private void btSortGrupa_Click(object sender, EventArgs e)
        {
            if (SortAsc)
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("Grupa", "ASC");
                SortAsc = false;
            }
            else
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("Grupa", "DESC");
                SortAsc = true;
            }
            dgvAllMusic.ClearSelection();
        }
        private void btSortAlbum_Click(object sender, EventArgs e)
        {
            if (SortAsc)
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("Album", "ASC");
                SortAsc = false;
            }
            else
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("Album", "DESC");
                SortAsc = true;
            }
            dgvAllMusic.ClearSelection();
        }
        private void btSortGen_Click(object sender, EventArgs e)
        {
            if (SortAsc)
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("Gen", "ASC");
                SortAsc = false;
            }
            else
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("Gen", "DESC");
                SortAsc = true;
            }
            dgvAllMusic.ClearSelection();
        }
        private void btSortAn_Click(object sender, EventArgs e)
        {
            if (SortAsc)
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("An", "ASC");
                SortAsc = false;
            }
            else
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("An", "DESC");
                SortAsc = true;
            }
            dgvAllMusic.ClearSelection();
        }
        private void btSortDataAdd_Click(object sender, EventArgs e)
        {
            if (SortAsc)
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("DataAdd", "ASC");
                SortAsc = false;
            }
            else
            {
                dgvAllMusic.DataSource = allMusicController.GetAllMusicsSorted("DataAdd", "DESC");
                SortAsc = true;
            }
            dgvAllMusic.ClearSelection();
        }
            //удаление лишних выделений
        private void dgvAllMusic_SelectionChanged(object sender, EventArgs e)
        {
            if (sender is DataGridView dataGridView)
            {
                if (dataGridView != dgvAllMusic) dgvAllMusic.ClearSelection();
                if (dataGridView != dgvMusicName) dgvMusicName.ClearSelection();
                if (dataGridView != dgvMusicArtist) dgvMusicArtist.ClearSelection();
                if (dataGridView != dgvMusicAlbum) dgvMusicAlbum.ClearSelection();
                if (dataGridView.SelectedRows.Count > 0)
                {
                    btAddToPlaylist.Enabled = true;
                }
                else
                {
                    btAddToPlaylist.Enabled = false;
                }
            }
        }
        //
        //работа с панелью управления
        //
        //переключение по вкладкам
        private void btControlsMusic_Click(object sender, EventArgs e)
        {
            //вывод дополнительных полей
            isMusic = true;
            tbx4.Show();
            tbx5.Show();
            tbx6.Show();
            tbx1.PlaceholderText = "Denumirea:";
            tbx2.PlaceholderText = "Grupa:";
            tbx3.PlaceholderText = "Album:";
            //вывод всей музыки
            dgvControlPanel.DataSource = allMusicController.GetAllMusic();
            dgvControlPanel.ClearSelection();
            //удаление лишнего текста
            tbx1.Text = "";
            tbx2.Text = "";
            tbx3.Text = "";
            tbx4.Text = "";
            tbx5.Text = "";
            tbx6.Text = "";
            btControlsMusic.Enabled = false;
            btControlsUsers.Enabled = true;
        }
        private void btControlsUsers_Click(object sender, EventArgs e)
        {
            //скрытие дополнительных полей
            isMusic = false;
            tbx4.Hide();
            tbx5.Hide();
            tbx6.Hide();
            tbx1.PlaceholderText = "Nume";
            tbx2.PlaceholderText = "Parola";
            tbx3.PlaceholderText = "Rol";
            //вывод всех пользователей
            dgvControlPanel.DataSource = usersContoller.GetAllUsers();
            dgvControlPanel.ClearSelection();
            //удаление лишнего текста
            tbx1.Text = "";
            tbx2.Text = "";
            tbx3.Text = "";
            btControlsMusic.Enabled = true;
            btControlsUsers.Enabled = false;
        }
            //управление данными
        private void btAdd_Click(object sender, EventArgs e)
        {
            //подтверждение действия
            var confirmation = MessageBox.Show("Are you sure to add this?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation == DialogResult.Yes)
            {
                //добавление объекта в базу
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
                        NumDeAscultari = Convert.ToInt32(tbx6.Text),
                        DataAdd = DateTime.Now.ToString("yyy/MM/dd", System.Globalization.CultureInfo.InvariantCulture)
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
        }
        private void btDelete_Click(object sender, EventArgs e)
        {
            //подтверждение действия
            var confirmation = MessageBox.Show("Are you sure to delete this?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation == DialogResult.Yes)
            {
                //удаление объекта
                if (dgvControlPanel.SelectedRows.Count > 0)
                {
                    var row = dgvControlPanel.SelectedRows[0];
                    int id = (int)row.Cells["Id"].Value;
                    using (var dataBaseContext = new DataBaseContext())
                    {
                        if (isMusic)
                        {
                            AllMusic music = allMusicController.GetMusicById(id);
                            if (music != null) allMusicController.DeleteMusic(music);
                            dgvControlPanel.DataSource = allMusicController.GetAllMusic();
                        }
                        else
                        {
                            Users users = usersContoller.GetUserById(id);
                            if (users != null) usersContoller.DeleteUser(users);
                            dgvControlPanel.DataSource = usersContoller.GetAllUsers();
                        }
                        dgvControlPanel.ClearSelection();
                    }
                }
            }
        }
        private void btModify_Click(object sender, EventArgs e)
        {
            //подтверждение действия
            var confirmation = MessageBox.Show("Are you sure to modify this?", "Confirm Modify", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmation == DialogResult.Yes)
            {
                //изменение объекта
                DateTime dateTime = DateTime.Today;
                Debug.Write(dateTime);
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
        }
            //выделение данных
        private void dgvControlPanel_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvControlPanel.SelectedRows.Count > 0)
            {
                //выведение выделенных данных
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
                btModify.Enabled = true;
                btDelete.Enabled = true;
            }
            else
            {
                //удаление лишних данных
                tbx1.Text = "";
                tbx2.Text = "";
                tbx3.Text = "";
                tbx4.Text = "";
                tbx5.Text = "";
                tbx6.Text = "";
                btModify.Enabled = false;
                btDelete.Enabled = false;
            }
        }
            //проверка данных
        private void tbxControls_TextChanged(object sender, EventArgs e)
        {
            if (isMusic)
            {
                bool isInts = false;
                //проверка что все данные заполнены и нужного формата
                try
                {
                    int num = Convert.ToInt32(tbx5.Text);
                    num = Convert.ToInt32(tbx6.Text);
                    isInts = true;
                }
                catch { isInts = false; }
                if (tbx1.Text != "" && tbx2.Text != "" && tbx3.Text != "" && tbx4.Text != "" && tbx5.Text != "" && tbx6.Text != "" && isInts && Convert.ToInt32(tbx5.Text) > 0 && Convert.ToInt32(tbx6.Text) >= 0)
                {
                    //разрешение/запрет доступа к функциям
                    btAdd.Enabled = true;
                    if (dgvControlPanel.SelectedRows.Count > 0)
                    {
                        btModify.Enabled = true;
                    }
                    else
                    {
                        btModify.Enabled = false;
                    }
                }
                else
                {
                    btAdd.Enabled = false;
                    btModify.Enabled = false;
                }
            }
            else
            {
                //проверка что все данные заполнены и нужного формата
                if (tbx1.Text != "" && tbx2.Text != "" && tbx3.Text != "")
                {
                    btAdd.Enabled = true;
                }
                else
                {
                    btAdd.Enabled = false;
                }
            }
        }
        //
        //управленеи плейлистами
        //
        private void dgvPlaylists_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvPlaylists.SelectedCells.Count > 0)
            {
                var row = dgvPlaylists.SelectedRows[0];
                Debug.WriteLine(row.Cells["Denumirea"].Value.ToString());
                //проверка названия плейлиста для удаления/модификации
                if (row.Cells["Denumirea"].Value.ToString() != "Favorites")
                {
                    btModifyPlaylist.Enabled = true;
                    btDeletePlaylist.Enabled = true;
                }
                else
                {
                    btModifyPlaylist.Enabled = false;
                    btDeletePlaylist.Enabled = false;
                }
            }
        }
        private void btAddPlaylist_Click(object sender, EventArgs e)
        {
            using (PlaylistName playlistName = new PlaylistName())
            {
                if (playlistName.ShowDialog() == DialogResult.OK)
                {
                    var confirmation = MessageBox.Show("Are you sure to add this playlist?", "Confirm Add", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (confirmation == DialogResult.Yes)
                    {
                        playlistsController.AddPlaylist(playlistName.playlistName, userId);
                        dgvPlaylists.DataSource = playlistsNumOfSongsController.GetPlaylistsNumOfSongs(userId);
                        dgvPlaylists.Columns[0].Visible = false;
                        dgvPlaylists.ClearSelection();
                    }
                }
            }
        }
        private void btDeletePlaylist_Click(object sender, EventArgs e)
        {
            if (dgvPlaylists.SelectedRows.Count > 0)
            {
                var confirmation = MessageBox.Show("Are you sure to delete this playlist?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
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
        }
        private void btModifyPlaylisr(object sender, EventArgs e)
        {
            if (dgvPlaylists.SelectedRows.Count > 0)
            {
                using (PlaylistName playlistName = new PlaylistName())
                {
                    var row = dgvPlaylists.SelectedRows[0];
                    if (playlistName.ShowDialog() == DialogResult.OK)
                    {
                        var confirmation = MessageBox.Show("Are you sure to modify this name of playlist?", "Confirm Modify", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (confirmation == DialogResult.Yes)
                        {
                            Debug.WriteLine(playlistName.playlistName);
                            playlistsController.ModifyPlaylist((int)row.Cells["Id"].Value, playlistName.playlistName);
                            dgvPlaylists.DataSource = playlistsNumOfSongsController.GetPlaylistsNumOfSongs(userId);
                            dgvControlPanel.ClearSelection();
                        }
                    }

                }
            }
        }
            //вход в плейлист
        private void dgvPlaylists_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //скрытие лишних кнопок
            btAddPlaylist.Hide();
            btModifyPlaylist.Hide();
            btDeletePlaylist.Hide();
            btDeleteFromPlaylist.Show();
            //получение назвваниея
            var row = dgvPlaylists.SelectedRows[0];
            string denumirea = (string)row.Cells["Denumirea"].Value;

            if (denumirea == "Favorites") guna2PictureBox3.Image = Image.FromFile("../../../images/favorites.png");
            else guna2PictureBox3.Image = Image.FromFile("../../../images/playlist.png");
            label3.Text = denumirea;
            //замена таблицы
            dgvPlaylists.Hide();
            dgvPlaylistsSongs.Show();
            //поиск и вывод песен
            dgvPlaylistsSongs.DataSource = musicInPlaylistsController.GetPlaylistSongs(denumirea, userId);
            dgvPlaylistsSongs.Columns[0].Visible = false;
            dgvPlaylistsSongs.ClearSelection();
        }
            //добавление и удаление песен в плейлисте
        private void btAddToPlaylist_Click(object sender, EventArgs e)
        {
            if (dgvAllMusic.SelectedRows.Count > 0)
            {
                playlistMusicController.AddPlaylistMusic(Convert.ToInt32(guna2ComboBox1.SelectedValue), Convert.ToInt32(dgvAllMusic.SelectedRows[0].Cells["Id"].Value));
            }
            else if (dgvMusicAlbum.SelectedRows.Count > 0)
            {
                playlistMusicController.AddPlaylistMusic(Convert.ToInt32(guna2ComboBox1.SelectedValue), Convert.ToInt32(dgvMusicAlbum.SelectedRows[0].Cells["Id"].Value));
            }
            else if (dgvMusicArtist.SelectedRows.Count > 0)
            {
                playlistMusicController.AddPlaylistMusic(Convert.ToInt32(guna2ComboBox1.SelectedValue), Convert.ToInt32(dgvMusicArtist.SelectedRows[0].Cells["Id"].Value));
            }
            else if (dgvMusicName.SelectedRows.Count > 0)
            {
                playlistMusicController.AddPlaylistMusic(Convert.ToInt32(guna2ComboBox1.SelectedValue), Convert.ToInt32(dgvMusicName.SelectedRows[0].Cells["Id"].Value));
            }
        }
        private void btDeleteFromPlaylist_Click(object sender, EventArgs e)
        {
            if (dgvPlaylistsSongs.SelectedRows.Count > 0)
            {
                var confirmation = MessageBox.Show("Are you sure to delete this song from the playlist?", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (confirmation == DialogResult.Yes)
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
        //
        //выбор отчета
        //
        private void cbRaportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //выбор данных отчета
            string column = "";
            if (guna2ComboBox2.SelectedIndex == 0)
            {
                column = "Gen";
                guna2ComboBox3.Show();
            }
            if (guna2ComboBox2.SelectedIndex == 1)
            {
                column = "An";
                guna2ComboBox3.Hide();
            }
            if (guna2ComboBox2.SelectedIndex == 2)
            {
                column = "NumDeAscultari";
                guna2ComboBox3.Hide();
            }
            if (guna2ComboBox2.SelectedIndex == 3)
            {
                column = "DataAdd";
                guna2ComboBox3.Hide();
            }
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                //сортировка по колоннам
                dgvRaports.DataSource = dataBaseContext.AllMusic
                                        .OrderByDescending(m => EF.Property<object>(m, column))
                                        .ToList();
                if (guna2ComboBox2.SelectedIndex == 0)
                {
                    column = "Gen";
                    guna2ComboBox3.Show();
                    //поиск всех жанров
                    var genres = dataBaseContext.AllMusic
                                    .GroupBy(m => m.Gen)
                                    .Select(g => g.Key)
                                    .OrderByDescending(g => g)
                                    .ToList();
                    guna2ComboBox3.DataSource = genres;
                    string selectedGenre = guna2ComboBox3.Items[0].ToString();
                    //поиск по первому жанру
                    var filteredSongs = dataBaseContext.AllMusic
                                    .Where(m => m.Gen == selectedGenre)
                                    .OrderByDescending(m => m.Gen)
                                    .ToList();
                    dgvRaports.DataSource = filteredSongs;
                }
            }
            dgvRaports.ClearSelection();
        }
        private void cbRaportGen_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedGenre = guna2ComboBox3.SelectedItem.ToString();
            //поиск по выбранным жанрам
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                var filteredSongs = dataBaseContext.AllMusic
                                    .Where(m => m.Gen == selectedGenre)
                                    .OrderByDescending(m => m.Gen)
                                    .ToList();

                dgvRaports.DataSource = filteredSongs;
            }
            dgvRaports.ClearSelection();
        }
    }
}
