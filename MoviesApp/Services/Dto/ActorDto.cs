using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.Services.Dto
{
    public class ActorDto
    {
        //когда только создаем объект, то id = null
        public int? Id { get; set; }
        
        [Required]
        [StringLength(32,ErrorMessage = "Title length can't be more than 32.")]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(32,ErrorMessage = "Title length can't be more than 32.")]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}