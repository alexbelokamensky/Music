using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Model
{
    public class Playlist_Music
    {
        [Key]
        public int Id { get; set; }
        public int IdPlaylist { get; set; }
        public int IdMusic { get; set;}
    }
}
