using Product__Application.Models;
using Product__Application.ViewModel;

namespace Product__Application.Repository.ProductRepository
{
    public interface IProductRepository
    {
        void AddProduct(AddProductViewModel product);
        void EditProduct(int id, EditProductViewModel product);
        void DeleteProduct(int id);
        string GetImageProductByID(int id);
        List<Category> GetAllCategories();
        List<Product> GetAll();
        List<Product> GetAllByCategory(int Categoryy);

        Product GetById(int id);

    }
}
