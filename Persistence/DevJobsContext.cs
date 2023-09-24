using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevJobs.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Persistence
{
    /// <summary>
    /// Context vem do Contexto de Dados.
    /// </summary>
    public class DevJobsContext : DbContext 
    {   
        public DevJobsContext(DbContextOptions<DevJobsContext> options) : base(options){

        }
        public DbSet<JobVacancy> JobVacancies { get; set; }
    }
}