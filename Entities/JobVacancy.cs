using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevJobs.API.Entities
{
    public class JobVacancy
    {
        public JobVacancy(string title, string description, string company, bool isRemote, string salaryRange)
        {
            Title = title;
            Description = description;
            Company = company;
            IsRemote = isRemote;
            SalaryRange = salaryRange;
            //Inicializando lista e pegando dt atual
            CreateAt = DateTime.Now;
            Applications = new List<JobApplication>();
        }
        /// <summary>
        ///  Encapsulamento no private set.. para que ninguem fique setando novos valores.
        /// </summary>
        public int Id { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Company { get; private set; }
        public bool IsRemote { get; private set; }
        public string SalaryRange { get; private set; }
        public DateTime CreateAt { get; private set; }
        public List<JobApplication> Applications { get; private set; }

        // Para as propriedades que deseja alterar - Criar um método. *Encapsulamento*
        public void Update(string title, string description)
        {
            Title = title;
            Description = description;
        }

    }
}