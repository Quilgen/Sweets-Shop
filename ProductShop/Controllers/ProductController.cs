using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductShop.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductShop.Controllers
{
	public class ProductController : Controller
	{
		private ShopContext context;

		public ProductController(ShopContext ctx)
		{
			context = ctx;
		}

		public IActionResult Index()
		{
			return RedirectToAction("List", "Products");
		}

		public IActionResult Detail(int id)
		{
			var categories = context.Categories.OrderBy(c => c.CategoryId).ToList();
			Product product = context.Products.Find(id);
			var categoryName = "";

			foreach (var category in categories)
			{
				if (category.CategoryId == product.CategoryId)
				{
					categoryName = category.Name;
				}
			}

			string imageFileName = product.Code + "-m.jpg";
			ViewBag.CategoryName = categoryName;
			ViewBag.ImageFileName = imageFileName;

			return View(product);
		}

		[Route("[controller]s/{id?}")]
		public IActionResult List(string id = "All")
		{
			var categories = context.Categories.OrderBy(c => c.CategoryId).ToList();

			List<Product> products;
			if (id == "All")
			{
				products = context.Products.OrderBy(p => p.ProductID).ToList();
			}
			else if (id == "Specials")
			{
				products = context.Products.Where(p => p.Price < 5.0M).OrderBy(p => p.ProductID).ToList();
			}
			else
			{
				products = context.Products.Where(p => p.Category.Name == id).OrderBy(p => p.ProductID).ToList();
			}

			var model = new ProductListViewModel
			{
				Categories = categories,
				Products = products,
				SelectedCategory = id
			};

			//ViewBag.SelectedCategoryName = id;
			//ViewBag.AllCategories = categories;

			return View(model);
		}
	}
}
