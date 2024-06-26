namespace BloggieWebProject.Repositorio
{
    public interface IImageRepositorio
    {
        Task<string> UploadAsync(IFormFile file);
    }
}
