﻿using System.ComponentModel.DataAnnotations;

namespace University.Dto
{
    public class LessonForCreationDto
    {
        [Required]
        [MaxLength(14)]
        public string Name { get; set; }

    }
}
