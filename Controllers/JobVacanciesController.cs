using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence;
using DevJobs.API.Persistence.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace DevJobs.API.Controllers
{
    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        // Readonly : So pode ser atribuido no construtor
        private readonly IJobVacancyRepository _repository;
        public JobVacanciesController(IJobVacancyRepository repository)
        {
            _repository = repository;
        }
        /// <summary>
        /// Obter todas as vagas.
        /// </summary>
        /// <returns>Todos os dados</returns>
        /// <response code="200">Sucesso.</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobvacancies = _repository.GetAll();
            return Ok(jobvacancies);
        }
        /// <summary>
        /// Obter vaga de emprego por id
        /// </summary>
        /// <param name="id">id da vaga</param>
        /// <returns>Vaga por id</returns>
        /// <response code="200">Sucesso.</response>
        /// <response code="404">Não encontrado.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }

        /// <summary>
        /// Cadastrar uma vaga de emprego.
        /// </summary>
        ///<remarks>
        ///{
        ///"title": ".NET SR",
        ///"description": "vaga para sustentação de .net core",
        ///"company": "SamuDev",
        ///"isRemote": true,
        ///"salaryRange": "9000 - 15000"
        ///}
        /// </remarks>
        /// <param name="model">Dados da vaga</param>
        /// <returns>Objeto recém-criado</returns>
        /// <response code="201">Sucesso.</response>
        /// <response code="400">Dados Invalidos.</response>
        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            Log.Information("POST JobVacancy chamado.");

            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );
            if (jobVacancy.Title.Length > 30)
                return BadRequest();

            _repository.Add(jobVacancy);

            return CreatedAtAction(nameof(GetById), new { id = jobVacancy.Id }, jobVacancy);
        }

        /// <summary>
        /// Alterar uma vaga de emprego
        /// </summary>
        /// <remarks>
        /// {
        /// "title": "string",
        /// "description": "string"
        /// }
        /// </remarks>
        /// <param name="id">Id Vaga</param>
        /// <param name="model">Dados da vaga</param>
        /// <returns>No content</returns>
        /// <response code="204">Sucesso.</response>
        /// <response code="404">Não encontado.</response>
        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var jobVacancy = _repository.GetById(id);

            if (jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);

            _repository.Update(jobVacancy);

            return NoContent();
        }


    }
}