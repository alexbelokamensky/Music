using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Music.Controllers
{
    public class AllMusicController
    {
        public List<AllMusic> GetAllMusicsSorted()
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                List<AllMusic> allMusic = dataBaseContext.AllMusic.FromSql($"SELECT * FROM AllMusic ORDER BY Denumirea ASC").ToList();
                return allMusic;
            }
        }
        public List<AllMusic> GetRandomMusics()
        {
            Random random = new Random();
            using (var dataBaseContext = new DataBaseContext())
            {
                List<AllMusic> randomMusic = new List<AllMusic>();
                for (int i = 0; i < 6; i++) randomMusic.Add(GetAllMusicsSorted()[random.Next(1, GetAllMusicsSorted().Count)]);
                return randomMusic;
            }
        }
        public List<AllMusic> GetHomeFavorites(string username)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.AllMusic.FromSql($"SELECT AllMusic.* FROM AllMusic JOIN Playlist_Music ON AllMusic.id = Playlist_Music.IdMusic JOIN Playlists ON Playlist_Music.IdPlaylist = Playlists.Id JOIN Users ON Playlists.IdUser = Users.Id WHERE Playlists.Denumirea = 'Favorites' AND Users.Nume = {username} LIMIT 5").ToList();
            }
        }
        public List<AllMusic> GetFavorites(string username)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.AllMusic.FromSql($"SELECT AllMusic.* FROM AllMusic JOIN Playlist_Music ON AllMusic.id = Playlist_Music.IdMusic JOIN Playlists ON Playlist_Music.IdPlaylist = Playlists.Id JOIN Users ON Playlists.IdUser = Users.Id WHERE Playlists.Denumirea = 'Favorites' AND Users.Nume = {username}").ToList();
            }
        }
        public List<AllMusic> GetAllMusic()
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.AllMusic.FromSql($"SELECT * FROM AllMusic").ToList();
            }
        }

        public List<AllMusic> GetFindedMusic(string find, string column)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.AllMusic.FromSqlRaw($"SELECT * FROM AllMusic WHERE {column} LIKE '%{find}%'").ToList();
            }
        }

        public AllMusic GetLastMusic()
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.AllMusic.OrderBy(m => m.Id).Last();
            }
        }
        public AllMusic GetMusicById(int id)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.AllMusic.FirstOrDefault(m => m.Id == id);
            }
        }
        public void AddMusic(AllMusic music)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                dataBaseContext.AllMusic.Add(music);
                dataBaseContext.SaveChanges();
            }
        }
        public void DeleteMusic(AllMusic music)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                dataBaseContext.AllMusic.Remove(music);
                dataBaseContext.SaveChanges();
            }
        }
        public void ModifyMusic(int id, string denumirea, string grupa, string album, string gen, int an, int numDeAscultari)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                AllMusic music = GetMusicById(id);
                if (music != null)
                {
                    music.Denumirea = denumirea;
                    music.Grupa = grupa;
                    music.Album = album;
                    music.Gen = gen;
                    music.An = an;
                    music.NumDeAscultari = numDeAscultari;
                    dataBaseContext.SaveChanges();
                }
            }
        }
    }
}
