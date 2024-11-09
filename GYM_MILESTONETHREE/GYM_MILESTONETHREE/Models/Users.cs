﻿using GYM_MILESTONETHREE.Enums;
using System.ComponentModel.DataAnnotations;

namespace GYM_MILESTONETHREE.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Role Role { get; set; }

        public string Nicnumber {  get; set; }

        public Address? Address { get; set; }

        public string Password { get; set; }

        public Enrollments? Enrollment { get; set; }

        public Guid? EnrollmentId {  get; set; }
    }
}
