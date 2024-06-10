using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Model
{
    public class DataBaseContext : DbContext
    {
        public DbSet<AllMusic> AllMusic { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;
        public DbSet<Playlists> Playlists { get; set; } = null!;
        public DbSet<PlaylistsNumOfSongs> NumOfSongs { get; set; } = null!;
        public DbSet<Playlist_Music> playlist_Music { get; set; } = null!;
        public DbSet<MusicInPlaylist> musicInPlaylists { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Music.db");
        }
    }
}
