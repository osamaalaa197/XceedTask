using Grpc.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.VisualBasic;
using Product__Application.Constants;
using Product__Application.Context.DbContext;
using Product__Application.Models;
using Product__Application.ViewModel;

namespace Product__Application.Repository.ProductRepository
{
    public class ProductRepository : IProductRepository
    {
        private readonly DataBaseContext _context;

        public ProductRepository(DataBaseContext dataBaseContext) {
            _context= dataBaseContext;

        }
        #region Add
        public  void AddProduct(AddProductViewModel product)
        {
            try
            {
                var newProduct = new Product
                {
                    Name = product.Name,
                    Price = product.Price,
                    Duration = product.Duration,
                    CategoryId = product.CategoryId,
                    CreationDate = product.CreationDate,
                    StartDate = product.StartDate,
                    Image = UploadPhoto(product.Image, FolderPathConstant.ProductImage, Guid.NewGuid(), string.Empty),
                    UserAppId = product.UserID
                };
                _context.products.Add(newProduct);
                _context.SaveChanges();
            }
            catch (Exception ex) {
                throw new RpcException(new Status(StatusCode.Internal, "An error occurred while adding the product."), ex.Message);

            }

        }
        #endregion

        #region Delete
        public void DeleteProduct(int id)
        {
            var res = GetById(id);
            _context.products.Remove(res);
            _context.SaveChanges();
        }
        #endregion

        #region Get
        public List<Product> GetAll()
        {
            var currentDate = DateTime.Now;
            return _context.products.Include(e => e.Category).Where(e => e.StartDate <= currentDate && e.StartDate.AddMinutes(e.Duration) >= currentDate).ToList();
        }

        public List<Category> GetAllCategories()
        {
            return _context.categories.ToList();
        }

        public List<Product> GetAllByCategory(int Category)
        {
            var currentDate = DateTime.Now;
            var res = _context.products.Include(e => e.Category).
                Where(e => e.CategoryId == Category && e.StartDate <= currentDate && e.StartDate.AddMinutes(e.Duration) >= currentDate).ToList();
            return res;
        }

        public Product GetById(int id)
        {
            var res = _context.products.Include(e => e.Category).FirstOrDefault(e => e.Id == id);
            return res;
        }
        public string GetImageProductByID(int id)
        {
            return _context.products.FirstOrDefault(e => e.Id == id).Image.ToString();
        }

        #endregion

        #region Edit
        public void EditProduct(int id, EditProductViewModel Newproduct)
        {
            var oldProduct = GetById(id);
            if (Newproduct.Image == null)
            {
                var img = GetImageProductByID(id);
                oldProduct.Image = img;


            }
            else
            {
                var img = UploadPhoto(Newproduct.Image, FolderPathConstant.ProductImage, Guid.NewGuid(), string.Empty);
                oldProduct.Image = img;
            }
            oldProduct.Name = Newproduct.Name;
            oldProduct.Duration = Newproduct.Duration;
            oldProduct.Price = Newproduct.Price;
            oldProduct.CreationDate = Newproduct.CreationDate;
            oldProduct.StartDate = Newproduct.CreationDate;
            oldProduct.CategoryId = Newproduct.CategoryId;
            _context.products.Update(oldProduct);
            _context.SaveChanges();

        }

        #endregion
        #region Helper
        public string UploadPhoto(IFormFile file, string folder, Guid id, string name)
        {
            string dpPath = "";
            try
            {
                var ext = Path.GetExtension(file.FileName);
                bool res = IsValidImageExtension(ext);

                if (file != null && res)
                {
                    if (file.Length > 0)
                    {
                        string folderName = Path.Combine("wwwroot", folder);
                        string pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                        if (!Directory.Exists(pathToSave))
                            Directory.CreateDirectory(pathToSave);
                        string fileName = name.Trim() + id + ".jpg";
                        string fullPath = Path.Combine(pathToSave, fileName);
                        dpPath = Path.Combine(folderName, fileName); //you can add this path to a list and then return all dbPaths to the client if require
                        using (var stream = new FileStream(fullPath, FileMode.Create))
                        {
                            file.CopyTo(stream);
                        }
                    }
                    else
                    {
                        return "Invalid";
                    }
                }
            }

            catch (Exception ex)
            {
                return ex.Message.ToString();
            }
            return dpPath.Replace($"wwwroot\\{folder}\\", "");
        }
        private bool IsValidImageExtension(string ext)
        {
            ext = ext.ToLower();
            return ext == ".jpg" || ext == ".png" || ext == ".jpeg";
        }
        #endregion


    }
}
