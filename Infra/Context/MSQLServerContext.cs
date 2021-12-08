using Microsoft.EntityFrameworkCore;
using Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infra.Context
{
    public class MSQLServerContext : DbContext
    {
        public MSQLServerContext(DbContextOptions<MSQLServerContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }
    
    }
}
