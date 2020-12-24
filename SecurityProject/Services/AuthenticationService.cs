using System.Linq;

using Microsoft.EntityFrameworkCore;

using SecurityProject.Models;

namespace SecurityProject.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IAESService aESService;
        private readonly DbContextOptions<SecurityProjectDbContext> dbContextOptions;

        public AuthenticationService(IAESService aESService, DbContextOptions<SecurityProjectDbContext> dbContextOptions)
        {
            this.aESService = aESService;
            this.dbContextOptions = dbContextOptions;
        }

        public User Login(string email, string password)
        {
            using (SecurityProjectDbContext context = new SecurityProjectDbContext(dbContextOptions))
            {
                var user = context.Users.SingleOrDefault(u => u.Email == email);
                if (user != null)
                {
                    var result = aESService.IsPasswordMatch(password, user.PasswordKey, user.Iv, user.Password);
                    if (result)
                        return user;
                    return null;
                }
                return null;
            }
        }

        public User Register(string name, string email, string password, string userType)
        {
            using (SecurityProjectDbContext context = new SecurityProjectDbContext(dbContextOptions))
            {
                var isNameOrEmailExists = context.Users.Any(u => u.Email == email || u.Name == name);
                if (isNameOrEmailExists)
                    return null;
                var encryptionResult = aESService.EncryptPassword(password);
                var user = new User
                {
                    Name = name,
                    Email = email,
                    Type = userType,
                    Iv = encryptionResult.IV,
                    Password = encryptionResult.Encryption,
                    PasswordKey = encryptionResult.Key
                };
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
        }
    }
}