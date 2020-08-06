using AvalieMe.API.Models;
using System.Linq;

namespace AvalieMe.API.Services
{
    public class Seguranca
    {
        public static bool Login(string login, string senha)
        {
            var senhaEncrypt = Util.EncryptString(senha);

            using (var contexto = new Contexto())
            {
                return contexto.usuario.Any(user => user.email.Equals(login.ToLower()) && user.senha == senhaEncrypt);
            }
        }
    }
}