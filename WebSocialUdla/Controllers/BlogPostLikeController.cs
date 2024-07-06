using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using WebSocialUdla.Models.Dominio;
using WebSocialUdla.Models.ViewModels;
using WebSocialUdla.Repositorio;

namespace WebSocialUdla.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class BlogPostLikeController : ControllerBase
	{
		private readonly IBlogPostLikeRepositorio blogPostLikeRepositorio;

		public BlogPostLikeController(IBlogPostLikeRepositorio blogPostLikeRepositorio)
        {
			this.blogPostLikeRepositorio = blogPostLikeRepositorio;
		}


        [HttpPost]
		[Route("Add")]
		public async Task<IActionResult> AddLike([FromBody] AddLikeRequest addLikeRequest) 
		{

			var model = new BlogPostLike
			{
				BlogPostId = addLikeRequest.BlogPostId,
				UserId = addLikeRequest.UserId,

			};

			await blogPostLikeRepositorio.AddLikeForBlog(model);

			return Ok();
		
		}



		[HttpGet]
		[Route("{blogPostId:Guid}/totalLikes")]
		public async Task<IActionResult> GetTotalLikesForBlog([FromRoute] Guid blogPostId) 
		{ 
			var totalLikes = await blogPostLikeRepositorio.GetTotalLikes(blogPostId);
			return Ok(totalLikes);
		
		}
	}
}
