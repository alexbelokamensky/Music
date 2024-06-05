using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Model
{
    public class AllMusic
    {
        [Key]
        public int Id { get; set; }
        public string? Denumirea { get; set; }
        public string? Grupa { get; set; }
        public string? Album { get; set; }
        public string? Gen {  get; set; }
        public int An {  get; set; }
        public int NumDeAscultari { get; set; }
    }
}
