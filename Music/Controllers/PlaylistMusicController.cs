using Guna.UI2.WinForms;
using Music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Controllers
{
    internal class PlaylistMusicController
    {
        public Playlist_Music GetLastPlaylistMusic()
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.playlist_Music.OrderBy(p => p.Id).Last();
            }
        }
        public void AddPlaylistMusic(int playlistId, int musicId)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                Playlist_Music playlist_Music = new Playlist_Music()
                {
                    Id = GetLastPlaylistMusic().Id + 1,
                    IdPlaylist = playlistId,
                    IdMusic = musicId
                };
                dataBaseContext.playlist_Music.Add(playlist_Music);
                dataBaseContext.SaveChanges();
            }
        }
        public void DeletePlaylistMusic(int id)
        {
            using (var dataBaseContext = new DataBaseContext())
            {
                Playlist_Music music = dataBaseContext.playlist_Music.FirstOrDefault(p => p.Id == id);
                if (music != null)
                {
                    dataBaseContext.playlist_Music.Remove(music);
                    dataBaseContext.SaveChanges();
                }
            }
        }
    }
}
