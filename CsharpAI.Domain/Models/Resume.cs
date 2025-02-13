using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpAI.Domain.Models
{
    public class Resume
    {
        public string Name { get; set; }
        public List<string> Skills { get; set; } = new();
        public string Experience { get; set; }
    }
}
