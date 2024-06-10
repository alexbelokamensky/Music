using Guna.UI2.WinForms;
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
    public class PlaylistsController
    {
        public Playlists GetLastPlaylist()
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.Playlists.OrderBy(p => p.Id).Last();
            }
        }
        public List<Playlists> GetAllPlaylisrs(int userId)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.Playlists.FromSql($"Select * FROM Playlists where Iduser = {userId}").ToList();
            }
        }
        public Playlists GetPlaylistById(int id)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                return dataBaseContext.Playlists.FirstOrDefault(p => p.Id == id);
            }
        }
        public void AddPlaylist(string playlistName, int userId)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                Playlists playlist = new Playlists()
                {
                    Id = GetLastPlaylist().Id + 1,
                    Denumirea = playlistName,
                    IdUser = userId
                };
                dataBaseContext.Playlists.Add(playlist);
                dataBaseContext.SaveChanges();
            }
        }
        public void DeletePlaylist(int id)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                Playlists playlists = GetPlaylistById(id);
                if (playlists != null)
                {
                    dataBaseContext.Playlists.Remove(playlists);
                    dataBaseContext.SaveChanges();
                }
            }
        }
        public void ModifyPlaylist(int id, string playlistName)
        {
            using (DataBaseContext dataBaseContext = new DataBaseContext())
            {
                Playlists playlists = GetPlaylistById(id);
                if (playlists != null)
                {
                    playlists.Denumirea = playlistName;
                    dataBaseContext.SaveChanges();
                }
            }
        }
    }
}
