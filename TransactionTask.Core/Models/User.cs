using System;
using System.ComponentModel.DataAnnotations;

namespace TransactionTask.Core.Models
{
    public class User
    {
        public User()
        {
            
        }
        
        public User(string name, string surname)
        {
            Name = name;
            Surname = surname;
        }
        
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Surname { get; set; }
        public long CreateDate { get; set; }
    }
}