using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Model
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        public string? Nume { get; set; }
        public string? Parola { get; set; }
        public string? Rol { get; set; }
    }
}
