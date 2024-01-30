using System.Security.Cryptography;

namespace UserManagerServer.Controllers
{
    public static class RSATools
    {
        public static RSA GetPrivateKey(string path = "rsa/private_key.pem")
        {
            var f = File.ReadAllText(path);
            var rsa = RSA.Create();
            rsa.ImportFromPem(f);
            return rsa;
        }
    }
}
