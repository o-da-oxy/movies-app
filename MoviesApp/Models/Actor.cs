using System;
using System.ComponentModel.DataAnnotations;

namespace MoviesApp.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [DataType(DataType.Time)]
        public DateTime BirthDate { get; set; }
        
    }
}