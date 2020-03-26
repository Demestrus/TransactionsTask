using System;

namespace TransactionTask.WebApi.Models
{
    public class ExistingUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime CreateDate { get; set; }
    }
}