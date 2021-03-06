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
        public DbSet<NonVeganFood> NonVeganFoods { get; set; }
        public DbSet<UserFood> UserFoods { get; set; }
        public DbSet<VeganFood> VeganFoods { get; set; }
        public DbSet<CapstoneProject.Models.VeganSubstitute> VeganSubstitutes { get; set; }
        public DbSet<CapstoneProject.Models.UserEatery> UserEateries { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        //public DbSet<CapstoneProject.Models.Recipe> Recipe { get; set; }
        //public DbSet<CapstoneProject.Models.City> City { get; set; }
        //public DbSet<CapstoneProject.Models.Restaurant2> Restaurant2 { get; set; }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    // get file with each of the foods as strings to do a for each loop to create all them
        //    // then out of the loop set the ones as questionable if needed or do a seperate file for those
        //}
    }
}
