using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevJobs.API.Entities;
using DevJobs.API.Models;
using DevJobs.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DevJobs.API.Controllers
{
    [Route("api/job-vacancies")]
    [ApiController]
    public class JobVacanciesController : ControllerBase
    {
        // Readonly : So pode ser atribuido no construtor
        private readonly DevJobsContext _context;
        public JobVacanciesController(DevJobsContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var jobvacancies = _context.JobVacancies;
            return Ok(jobvacancies);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var jobVacancy = _context.JobVacancies
                .Include(jv => jv.Applications)
                .SingleOrDefault(jv => jv.Id == id);

            if (jobVacancy == null)
                return NotFound();

            return Ok(jobVacancy);
        }

        [HttpPost]
        public IActionResult Post(AddJobVacancyInputModel model)
        {
            var jobVacancy = new JobVacancy(
                model.Title,
                model.Description,
                model.Company,
                model.IsRemote,
                model.SalaryRange
            );

            _context.JobVacancies.Add(jobVacancy);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = jobVacancy.Id }, jobVacancy);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, UpdateJobVacancyInputModel model)
        {
            var jobVacancy = _context.JobVacancies
                .SingleOrDefault(jv => jv.Id == id);

            if (jobVacancy == null)
                return NotFound();

            jobVacancy.Update(model.Title, model.Description);
            _context.SaveChanges();

            return NoContent();
        }


    }
}