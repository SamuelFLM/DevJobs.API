using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevJobs.API.Models
{
    /// <summary>
    /// record: Simplifica a criação de objeto. Class mais enxuta
    /// e facilita na parte de comparação.
    /// </summary>
    public record AddJobVacancyInputModel(
        string Title,
        string Description,
        string Company,
        bool IsRemote,
        string SalaryRange)
    {
        
    }
}