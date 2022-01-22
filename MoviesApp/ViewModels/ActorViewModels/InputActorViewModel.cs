using System;
using System.ComponentModel.DataAnnotations;
using MoviesApp.Filters;

namespace MoviesApp.ViewModels.ActorViewModels
{
    public class InputActorViewModel
    {
        [Required]
        [ShortName(4)]
        public string FirstName { get; set; }
        
        [Required]
        [ShortName(4)]
        public string LastName { get; set; }
        
        [Required]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
    }
}