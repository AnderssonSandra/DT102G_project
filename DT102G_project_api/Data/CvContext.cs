using DT102G_project_API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DT102G_project_api.Data
{
    public class CvContext : DbContext
    {
        //constructor
        public CvContext(DbContextOptions<CvContext> options) : base(options)
        {

        }

        //props
        public DbSet<Work> Works { get; set; }

        public DbSet<Education> Education { get; set; }

        public DbSet<PersonalInformation> PersonalInformation { get; set; }

        public DbSet<Project> Project { get; set; }

    }
}
