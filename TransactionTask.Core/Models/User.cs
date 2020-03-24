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
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}