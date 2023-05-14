using DemoProject.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace DemoProject
{
    public class UserDbContext : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder foModelBuilder)
        {
            base.OnModelCreating(foModelBuilder);
            foModelBuilder.Entity<UserModel>().ToTable("tblUsers");
        }
        public DbSet<UserModel> loUserModel { get; set; }
    }
}