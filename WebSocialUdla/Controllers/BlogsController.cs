using BloggieWebProject.Repositorio;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebSocialUdla.Models.Dominio;
using WebSocialUdla.Models.ViewModels;
using WebSocialUdla.Repositorio;

namespace BloggieWebProject.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBlogPostRepositorio blogPostRepositorio;
		private readonly IBlogPostLikeRepositorio blogPostLikeRepositorio;
		private readonly SignInManager<IdentityUser> signInManager;
		private readonly UserManager<IdentityUser> userManager;
		private readonly IBlogPostCommentRepositorio blogPostCommentRepositorio;

		public BlogsController(IBlogPostRepositorio blogPostRepositorio,
            IBlogPostLikeRepositorio blogPostLikeRepositorio,
			SignInManager<IdentityUser> signInManager, 
			UserManager<IdentityUser> userManager,
			IBlogPostCommentRepositorio blogPostCommentRepositorio)
        {
            this.blogPostRepositorio = blogPostRepositorio;
			this.blogPostLikeRepositorio = blogPostLikeRepositorio;
			this.signInManager = signInManager;
			this.userManager = userManager;
			this.blogPostCommentRepositorio = blogPostCommentRepositorio;
		}

        [HttpGet]
        public async Task<IActionResult> Index(string manejadorUrl)
        {
			var liked = false;
            var blogPost = await blogPostRepositorio.GetByUrlHandleAsync(manejadorUrl);
			var blogDetallesViewModel = new BlogDetallesViewModel();



			if (blogPost != null) 
            {

                var totalLikes = await blogPostLikeRepositorio.GetTotalLikes(blogPost.Id);

				if (signInManager.IsSignedIn(User)) 
				{
					var likesForBlog = await blogPostLikeRepositorio.GetLikesForBlog(blogPost.Id);

					var userId = userManager.GetUserId(User);

					if(userId != null) 
					{ 
						var likeFromUser = likesForBlog.FirstOrDefault(x => x.UserId == Guid.Parse(userId));
						liked = likeFromUser != null;
					}

				}

				//Obtener los comentarios del Post
				var blogCommentsDomainModel = await blogPostCommentRepositorio.GetCommentsByBlogIdAsync(blogPost.Id);

				var blogCommentsForView = new List<BlogComment>();

				foreach (var blogComment in blogCommentsDomainModel) 
				{
					blogCommentsForView.Add(new BlogComment
					{
						Descripcion = blogComment.Descripcion,
						FechaAgregado = blogComment.FechaAgregado,
						Usuario = (await userManager.FindByIdAsync(blogComment.UserId.ToString())).UserName
					});
				}
				
				blogDetallesViewModel = new BlogDetallesViewModel
				{
					Id = blogPost.Id,
					Contenido = blogPost.Contenido,
					TituloPagina = blogPost.TituloPagina,
					Autor = blogPost.Autor,
					UrlImagenDestacada = blogPost.UrlImagenDestacada,
					Encabezado = blogPost.Encabezado,
					FechaPublicacion = blogPost.FechaPublicacion,
					DescripcionCorta = blogPost.DescripcionCorta,
					ManejadorUrl = blogPost.ManejadorUrl,
					Visible = blogPost.Visible,
					Tags = blogPost.Tags,
					TotalLikes = totalLikes,
					Liked = liked,
					Comentarios = blogCommentsForView
				};

			}


            return View(blogDetallesViewModel);
        }


		[HttpPost]
		public async Task<IActionResult> Index(BlogDetallesViewModel blogDetallesViewModel)
		{
			if (signInManager.IsSignedIn(User)) 
			{
				var domainModel = new BlogPostComment
				{
					BlogPostId = blogDetallesViewModel.Id,
					Descripcion = blogDetallesViewModel.DescripcionComentario,
					UserId = Guid.Parse(userManager.GetUserId(User)),
					FechaAgregado = DateTime.Now
				};
				await blogPostCommentRepositorio.AddAsync(domainModel);
				return RedirectToAction("Index", "Blogs", 
					new { manejadorUrl = blogDetallesViewModel.ManejadorUrl });
			}

			return View();
		}
    
	
	
	}
}
