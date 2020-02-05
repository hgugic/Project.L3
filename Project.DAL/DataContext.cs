using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project.DAL
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DataContext()
        {

        }

        public virtual DbSet<VehicleMake> Makers { get; set; }

        public virtual DbSet<VehicleModel> Models { get; set; }
    }
}
