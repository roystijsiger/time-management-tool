using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TimeManagementTool.Models;

namespace TimeManagementTool.Data
{
    class TimeManagementContext : DbContext
    {
        public TimeManagementContext() : base()
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ProcessCategory> Processes { get; set; }
    }
}
