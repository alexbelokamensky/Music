using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Model
{
    public class PlaylistsNumOfSongs
    {
        [Key]
        public int Id { get; set; }
        public string Denumirea { get; set; }
        [Column("NumSongs")]
        public int NumSongs { get; set; }
    }
}
