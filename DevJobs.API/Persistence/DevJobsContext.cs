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
        public DevJobsContext(DbContextOptions<DevJobsContext> options) : base(options)
        {

        }
        public DbSet<JobVacancy> JobVacancies { get; set; }
        public DbSet<JobApplication> JobApplications { get; set; }

        /// <summary>
        /// Se chama fluent api, recomendado para configurar minhas tabelas para o banco de dados.
        /// </summary>
        /// <param name="builder"></param>
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<JobVacancy>(e =>
            {
                e.HasKey(jv => jv.Id); // configura chave primaria
                //e.ToTable("tb_JobVacancies"); configura o nome da tabela

                //Configurando o relacionamento da tabela
                e.HasMany(jv => jv.Applications) // um jobvacancies tem muitos application
                    .WithOne() // tem uma aplicação tem apenas uma vaga
                    .HasForeignKey(ja => ja.IdJobVacancy) // qual a foreign key ? 
                    .OnDelete(DeleteBehavior.Restrict);  // qual vai ser o comportamento no delete. vou restringir e não deixa deletar o registro.
            });
            //Relacionando job application -> job Vacancies
            builder.Entity<JobApplication>(e =>
            {
                e.HasKey(ja => ja.Id);
            });
        }
    }
}