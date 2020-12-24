using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SecurityProject.Constants
{
    public class PasswordEncryptionResult
    {
        public byte[] IV { get; set; }
        public byte[] Encryption { get; set; }
        public byte[] Key { get; set; }
    }
}
