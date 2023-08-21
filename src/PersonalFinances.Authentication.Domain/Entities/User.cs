namespace PersonalFinances.Authentication.Domain.Entities
{
    public class User : BaseEntity
    {
        public User(string firstName, string lastName, string email, string password)
        {             
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            Active = true;
        }

        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string? Role { get; private set; }
        public bool Active { get; private set; }

        public void Update(string firstName, string lastName, string email, string role)
        {            
            FirstName = firstName;
            LastName = lastName;
            Email = email;            
            Role = role;
        }

        public void ChangePassword(string password)
        {
            Password = password;
        }
    }
}
