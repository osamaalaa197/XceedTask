using Product__Application.Validation;
using System.ComponentModel.DataAnnotations;

namespace Product__Application.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime StartDate { get; set; }
        public int Duration { get; set; }
        public double Price { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string UserAppId { get; set; }

        public UserApp UserApp { get; set; }


    }
}
