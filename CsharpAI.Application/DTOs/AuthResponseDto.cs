﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsharpAI.Application.DTOs
{
    public class AuthResponseDto
    {
        public string? Token { get; set; }
        public string? Role { get; set; }
        public string? Message { get; set; }
    }
}
