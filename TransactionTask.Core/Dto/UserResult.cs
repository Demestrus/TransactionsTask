using TransactionTask.Core.Models;

namespace TransactionTask.Core.Dto
{
    public class UserResult
    {
        public UserResult(User user = null) : this(true, null, user)
        {
            
        }
        
        public UserResult(bool success, string errorMsg = null, User user = null)
        {
            Success = success;
            ErrorMsg = errorMsg;
            User = user;
        }
        
        public bool Success { get; }
        public string ErrorMsg { get; }
        public User User { get; }
    }
}