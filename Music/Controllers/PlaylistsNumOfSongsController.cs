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
    public class PlaylistsNumOfSongsController
    {
        public List<PlaylistsNumOfSongs> GetPlaylistsNumOfSongs(int userId)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.NumOfSongs.FromSql($"SELECT Playlists.Id, Playlists.Denumirea, COALESCE(COUNT(Playlist_Music.IdMusic), 0) AS NumSongs FROM Playlists JOIN Users ON Playlists.IdUser = Users.Id LEFT JOIN Playlist_Music ON Playlists.Id = Playlist_Music.IdPlaylist WHERE Users.Id = {userId} GROUP BY Playlists.Id, Playlists.Denumirea;").ToList();
            }
        }
    }
}
