﻿using System;
using System.Collections.Generic;
using System.Text;
using CapstoneProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CapstoneProject.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {            
        }
        
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<StandardUser> StandardUsers { get; set; }
        public DbSet<Food> Food { get; set; }
    }
}
