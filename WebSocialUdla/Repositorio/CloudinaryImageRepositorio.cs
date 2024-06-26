﻿
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace BloggieWebProject.Repositorio
{
    public class CloudinaryImageRepositorio : IImageRepositorio
    {
        private readonly IConfiguration configuration;
        private readonly Account account;
        public CloudinaryImageRepositorio(IConfiguration configuration)
        {
            this.configuration = configuration;
            account = new Account(
                configuration.GetSection("Cloudinary")["CloudName"],
                configuration.GetSection("Cloudinary")["Apikey"],
                configuration.GetSection("Cloudinary")["ApiSecret"]);

        }

        public IConfiguration Configuration { get; }

        public async Task<string> UploadAsync(IFormFile file)
        {
            var client = new Cloudinary(account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(file.FileName, file.OpenReadStream()),
                DisplayName= file.FileName
            };
            var uploadResult = await client.UploadAsync(uploadParams);
            if(uploadResult != null && uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return uploadResult.SecureUri.ToString();
            }
            return null;
        }
    }
}
