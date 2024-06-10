using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using Music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Controllers
{
    public class MusicInPlaylistsController
    {
        public List<MusicInPlaylist> GetPlaylistSongs(string denumirea, int userId)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.musicInPlaylists.FromSql($"SELECT Playlist_Music.Id, AllMusic.Denumirea, AllMusic.Grupa, AllMusic.Album, AllMusic.Gen, AllMusic.An FROM Playlist_Music JOIN Playlists ON Playlist_Music.IdPlaylist = Playlists.Id JOIN AllMusic ON Playlist_Music.IdMusic = AllMusic.ID JOIN Users ON Playlists.IdUser = Users.Id WHERE Playlists.Denumirea = {denumirea} AND Users.Id = {userId};").ToList();
            }
        }
    }
}
