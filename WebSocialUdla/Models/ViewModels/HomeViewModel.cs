using BloggieWebProject.Models.Dominio;

namespace BloggieWebProject.Models.ViewModels
{
	public class HomeViewModel
	{
        public IEnumerable <BlogPost> BlogPosts{ get; set; }
		public IEnumerable<Tag> Tags { get; set; }
	}
}
