
using BloggieWebProject.Models.Dominio;

namespace BloggieWebProject.Repositorio

{
    public interface ITagRepositorio
    {
      Task<IEnumerable<Tag>> GetAllAsync();
      
      Task<Tag> GetAsync(Guid id);
      Task<Tag>AddAsync(Tag tag);
      Task<Tag?> UpdateAsync(Tag tag);
      Task<Tag?> DeleteAsync(Guid id);
    }
}
