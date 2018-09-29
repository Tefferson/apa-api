using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace crosscutting.identity.models
{
    public class ApplicationUser : IIdentity
    {
        public ApplicationUser()
        { }

        public ApplicationUser(string name, string email)
        {
            Name = name;
            Email = email;
        }

        [Key]
        public virtual string Id { get; set; } = Guid.NewGuid().ToString().Replace("-", "");
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string NormalizedUserName { get; set; }
        public string Email { get; set; }
        public string NormalizedEmail { get; set; }
        public virtual string PasswordHash { get; set; }
    }
}
