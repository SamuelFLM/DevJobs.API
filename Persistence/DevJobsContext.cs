using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevJobs.API.Entities;

namespace DevJobs.API.Persistence
{
    /// <summary>
    /// Context vem do Contexto de Dados.
    /// </summary>
    public class DevJobsContext 
    {   
        public DevJobsContext()
        {
            JobVacancies = new List<JobVacancy>();
        }
        public List<JobVacancy> JobVacancies { get; set; }
    }
}