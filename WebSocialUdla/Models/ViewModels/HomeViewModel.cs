using System.Collections.Generic;
using WebSocialUdla.Dominio.Models; 

namespace BloggieWebProject.Models.ViewModels
{
	public class HomeViewModel
	{
		public IEnumerable<BlogFica> BlogPostsFica { get; set; }
		public IEnumerable<BlogNodo> BlogPostsNodo { get; set; }
		public IEnumerable<Tag> Tags { get; set; } 
	}
}
