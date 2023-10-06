using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Product__Application.Constants;
using Product__Application.Context.DbContext;
using Product__Application.Models;
using Product__Application.Repository.ProductRepository;
using Product__Application.ViewModel;

namespace Product__Application.Controllers.Product
{
    public class ProductController : Controller
    {
        private readonly DataBaseContext _context;
        public readonly IProductRepository _productRepository;
        private readonly UserManager<UserApp> _userManager;

        public ProductController(DataBaseContext dataBaseContext, IProductRepository productRepository, UserManager<UserApp> userManager)
        {
            _context = dataBaseContext;
            _productRepository = productRepository;
            _userManager = userManager;
        }

        #region Get
        public IActionResult GetAllProduct(GetAllViewModel getViewModel)
        {
            getViewModel.Products = _productRepository.GetAll();
            getViewModel.Categories = _productRepository.GetAllCategories();
            return View(getViewModel);
        }
        [Authorize(Roles = UserRoles.User)]
        public IActionResult GetProductDetail(int productId)
        {
            var product = _productRepository.GetById(productId);
            return View(product);
        }

        public IActionResult GetProducByCategory(int categId)
        {
            var getViewModel = new GetAllViewModel();
            getViewModel.Products = _productRepository.GetAllByCategory(categId);
            getViewModel.Categories = _productRepository.GetAllCategories();

            return View(getViewModel);
        }
        #endregion

        #region Add
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult AddProduct()
        {
            var userId = _userManager.GetUserId(User);
            var viewmodel = new AddProductViewModel();
            viewmodel.UserID = userId;
            viewmodel.CreationDate = DateTime.Now;
            return View(viewmodel);
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult SaveItem(AddProductViewModel addProductViewModel)
        {
            if (ModelState.IsValid == true)
            {
                _productRepository.AddProduct(addProductViewModel);
                return RedirectToAction("GetAllProduct");

            }
            else
            {
                return View("AddProduct", addProductViewModel);

            }
        }
        #endregion

        #region Edit
        [Authorize]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult EditProduct(int productId)
        {
            var old = _productRepository.GetById(productId);
            EditProductViewModel model = new EditProductViewModel
            {
                Id = old.Id,
                Name = old.Name,
                Price = old.Price,
                StartDate = old.StartDate,
                CreationDate = old.CreationDate,
                Duration = old.Duration,
                CategoryId = old.CategoryId,
            };
            return View(model);
        }
        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult SaveEdit(int productId, EditProductViewModel product)
        {
            if (product.Name != null || product.Price > 0)
            {
                _productRepository.EditProduct(productId, product);
                return RedirectToAction("GetAllProduct");
            }
            else
            {
                return View("EditProduct", product);
            }

        }
        #endregion

        #region Delete
        [Authorize]
        [Authorize(Roles = UserRoles.Admin)]
        public IActionResult DeleteProduct(int productId)
        {
            _productRepository.DeleteProduct(productId);
            return RedirectToAction("GetAllProduct");
        }
        #endregion

    }
}
