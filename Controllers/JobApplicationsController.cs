using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence;
using DevJobs.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace DevJobs.API.Controllers
{
    [Route("api/job-vacancies/{id}/applications")]
    [ApiController]
    public class JobApplicationsController : ControllerBase
    {
        private readonly IJobVacancyRepository _repository;
        public JobApplicationsController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Cadastrar uma aplicação
        /// </summary>
        /// <remarks>
        /// {
        /// "title": "string",
        /// "description": "string"
        /// }
        /// </remarks>
        /// <param name="id">Id da vaga</param>
        /// <param name="model">Dados da aplicação</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Sucesso.</response>
        /// <response code="400">Dados Invalidos.</response>
        [HttpPost]
        public IActionResult Post(int id, AddJobApplicationInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            var application = new JobApplication(model.ApplicantName, model.ApplicantEmail, id);

            _repository.AddApplication(application);

            return NoContent();
        }



    }
}