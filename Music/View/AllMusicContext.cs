﻿using Microsoft.EntityFrameworkCore;
using Music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.View
{
    public class AllMusicContext : DbContext
    {
        public DbSet<AllMusic> AllMusic { get; set; } = null!;
        public DbSet<Users> Users { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=Music.db");
        }
    }
}
