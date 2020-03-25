using System;

namespace TransactionTask.WebApi.Models
{
    public class CreatedUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreateDate { get; set; }
    }
}