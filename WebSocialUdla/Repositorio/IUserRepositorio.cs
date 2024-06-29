using Microsoft.AspNetCore.Identity;

namespace WebSocialUdla.Repositorio
{
    public interface IUserRepositorio
    {
        Task<IEnumerable<IdentityUser>>GetAll();
    }
}
