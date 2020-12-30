﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProductShop.Models
{
	public class Product
	{
		public int ProductID { get; set; }

		[Required(ErrorMessage = "Please enter product name")]
		public string Name { get; set; }

		[Required(ErrorMessage = "Please enter product price")]
		[Column(TypeName = "decimal(18,2)")]
		public decimal Price { get; set; }

		[Required(ErrorMessage = "Please select category")]
		public int CategoryId { get; set; } // foreign key

		public Category Category { get; set; }

		public decimal DiscountPercent => .20M;
		public decimal DiscountAmount => Price * DiscountPercent;
		public decimal DiscountPrice => Price - DiscountAmount;

		[Required(ErrorMessage = "Please enter product code")]
		public string Code { get; set; }

		public string Slug => Name == null ? "" : Name.Replace(' ', '-');

		/* This is the same as above ↑
		public string Slug
		{
			get
			{
				if (Name == null)
				{
					return "";
				}
				else
				{
					return Name.Replace(' ', '-');
				}
			}
		} */
	}
}
