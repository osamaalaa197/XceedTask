using Product__Application.Validation;

namespace Product__Application.ViewModel
{
    public class EditProductViewModel
    {
        public int Id { get; set; }     
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }

        [FileSize(1048576, ErrorMessage = "The image file must be no larger than 1MB.")]
        public IFormFile? Image { get; set; }
        public string ImageUrl { get; set; }


        public int CategoryId { get; set; }
    }
}
