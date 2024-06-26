using BloggieWebProject.Data;
using BloggieWebProject.Models.Dominio;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloggieWebProject.Repositorio
{
    public class TagRepositorio : ITagRepositorio
    {
        private readonly BlogDbContext blogDbContext;

        public TagRepositorio(BlogDbContext blogDbContext)
        {
            this.blogDbContext = blogDbContext;
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await blogDbContext.Tags.AddAsync(tag);
            await blogDbContext.SaveChangesAsync();
            return tag;
        }

        public async Task<Tag?> DeleteAsync(Guid id)
        {
            var tag = await blogDbContext.Tags.FindAsync(id);
            if (tag != null)
            {
                blogDbContext.Tags.Remove(tag);
                await blogDbContext.SaveChangesAsync();
                return tag;
            }
            return null;
        }

        public async Task<IEnumerable<Tag>> GetAllAsync()
        {
            return await blogDbContext.Tags.Include(t => t.BlogPosts).ToListAsync();
        }

        public async Task<Tag?> GetAsync(Guid id)
        {
            return await blogDbContext.Tags.Include(t => t.BlogPosts)
                                           .FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<Tag?> UpdateAsync(Tag tag)
        {
            var existingTag = await blogDbContext.Tags.FindAsync(tag.Id);
            if (existingTag != null)
            {
                existingTag.Nombre = tag.Nombre;
                existingTag.DisplayNombre = tag.DisplayNombre;

                await blogDbContext.SaveChangesAsync();
                return existingTag;
            }
            return null;
        }
    }
}
