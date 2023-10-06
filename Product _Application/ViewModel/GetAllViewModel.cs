using Product__Application.Models;

namespace Product__Application.ViewModel
{
    public class GetAllViewModel
    {
        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }
    }
}
