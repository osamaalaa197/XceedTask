using Product__Application.Models;
using Product__Application.Validation;
using System.ComponentModel.DataAnnotations;

namespace Product__Application.ViewModel
{
    public class AddProductViewModel
    {
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }

        [FileSize(1048576, ErrorMessage = "The image file must be no larger than 1MB.")]
        public IFormFile Image { get; set; }
        public string? ImageUrl { get; set; }


        public int CategoryId { get; set; }
        public string UserID { get; set; }
    }
}
