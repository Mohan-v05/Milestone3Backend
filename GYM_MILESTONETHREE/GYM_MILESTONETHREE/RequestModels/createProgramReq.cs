﻿using GYM_MILESTONETHREE.Models;
using System.ComponentModel.DataAnnotations;

namespace GYM_MILESTONETHREE.RequestModels
{
    public class createProgramReq
    {


        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? Category { get; set; }

        [Required]
        public decimal Fees { get; set; }
         public IFormFile image { get; set; }

    }
}
