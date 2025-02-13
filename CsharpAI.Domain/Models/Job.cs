using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsharpAI.Domain.Enums;

namespace CsharpAI.Domain.Models
{
    public class Job
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public List<string> RequiredSkills { get; set; } = new();
        public JobType Type { get; set; }
    }
}
