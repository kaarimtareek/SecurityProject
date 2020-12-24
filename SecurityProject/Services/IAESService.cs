using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using SecurityProject.Constants;

namespace SecurityProject.Services
{
    public interface IAESService
    {
        PasswordEncryptionResult EncryptPassword(string password);
        string DecryptPassword(byte[] password, byte[] key, byte[] IV);
        bool IsPasswordMatch(string password, byte[] key,byte[] IV, byte[] encryption);
    }
}
