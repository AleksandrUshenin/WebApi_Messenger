using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UserManagerServer.Controllers;

namespace TestProjects
{
    [TestFixture]
    public class RSAToolsTests
    {
        [Test]
        public void TestDecryptMethod()
        {
            string path = "D:\\workspace\\gb.ru.messenger.applications\\UserService\\rsa\\private_key.pem";
            RSA plaintext = RSATools.GetPrivateKey(path);

            Assert.NotNull(plaintext);
        }

    }
}
