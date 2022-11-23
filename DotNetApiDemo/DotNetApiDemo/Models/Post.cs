using System.ComponentModel.DataAnnotations;

namespace DotNetApiDemo.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Must enter your title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Must enter your description")]
        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
