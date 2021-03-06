﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductShop.Models;

namespace ProductShop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		private ShopContext context;

		public CategoryController(ShopContext ctx)
		{
			context = ctx;
		}
		public IActionResult Index()
		{
			return RedirectToAction("List", "Category");
		}

		[Route("[area]/Categories/{id?}")]
		public IActionResult List()
		{
			var categories = context.Categories.OrderBy(c => c.CategoryId).ToList();
			return View(categories);
		}

		[HttpGet]
		public IActionResult Add()
		{
			ViewBag.Action = "Add";
			return View("AddUpdate", new Category());
		}

		[HttpGet]
		public IActionResult Update(int id)
		{
			var category = context.Categories.Find(id);
			ViewBag.Action = "Update";
			return View("AddUpdate", category);
		}

		[HttpPost]
		public IActionResult Update(Category category)
		{
			if (ModelState.IsValid)
			{
				if (category.CategoryId == 0)
				{
					context.Categories.Add(category);
				}
				else
				{
					context.Categories.Update(category);
				}
				context.SaveChanges();
				return RedirectToAction("List", "Category");
			}
			else
			{
				ViewBag.Action = "Save";
				return View("AddUpdate");
			}
		}

		[HttpGet]
		public IActionResult Delete(int id)
		{
			var category = context.Categories.Find(id);
			return View(category);
		}

		[HttpPost]
		public IActionResult Delete(Category category)
		{
			context.Remove(category);
			context.SaveChanges();
			return RedirectToAction("List", "Category");
		}
	}
}