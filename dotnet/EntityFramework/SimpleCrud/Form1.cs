using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleCrud
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void ListPeople_Click(object sender, EventArgs e)
		{
			//ApplyChangesToMultiple();
			RunNormalQuery();
			//RevertChangesFromMultiple();
		}

		private void ApplyChangesToMultiple()
		{
			using (var context = new AdventureWorks2012Entities())
			{
				var people = from p in context.People
							 where p.LastName == "King"
							 orderby p.LastName, p.FirstName descending
							 select p;
				foreach (var person in people)
				{
					person.FirstName = person.FirstName + " - 2";
				}

				context.SaveChanges();
			}
		}

		private void RevertChangesFromMultiple()
		{
			using (var context = new AdventureWorks2012Entities())
			{
				var people = from p in context.People
							 where p.LastName == "King"
							 orderby p.LastName, p.FirstName descending
							 select p;
				foreach (var person in people)
				{
					var firstName = person.FirstName;
					person.FirstName = firstName.Substring(0, firstName.Length - 4);
				}

				context.SaveChanges();
			}
		}

		private void RunNormalQuery()
		{
			using (var context = new AdventureWorks2012Entities())
			{
				var people = from p in context.People
							 where p.LastName == "King" || p.LastName == "Jones"
							 orderby p.LastName, p.FirstName descending
							 select new { p.FirstName, p.LastName };
				/*var people =
					context.People.
						Where(p => p.LastName == "King").
						OrderByDescending(p => p.FirstName).
						Select(p => new {p.FirstName, p.LastName});*/
				foreach (var person in people)
				{
					listBox1.Items.Add(string.Format("{0} {1}", person.FirstName, person.LastName));
				}
			}
		}

		private void RunGroupQuery()
		{
			using (var context = new AdventureWorks2012Entities())
			{
				var people = from p in context.People
							 orderby p.FirstName
							 where p.LastName == "King" || p.LastName == "Jones"
							 group new { p.FirstName, p.LastName } by p.LastName;
				foreach (var group in people)
				{
					foreach (var person in group)
					{
						listBox1.Items.Add(string.Format("{0} {1}", person.FirstName, person.LastName));
					}
				}
			}
		}

		private void AddProductModel_Click(object sender, EventArgs e)
		{
			//AddProductModelMethodTwo();
		}

		private void AddProductModelMethodOne()
		{
			try
			{
				using (var context = new AdventureWorks2012Entities1())
				{
					var productModel = new ProductModel();
					productModel.Name = "Front Forks";
					productModel.rowguid = Guid.NewGuid();
					productModel.ModifiedDate = DateTime.Now;
					context.ProductModels.AddObject(productModel);
					context.SaveChanges();
					label1.Text = "Save successful";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				throw;
			}
		}

		private void AddProductModelMethodTwo()
		{
			try
			{
				using (var context = new AdventureWorks2012Entities1())
				{
					var productModel = ProductModel.CreateProductModel(0, "Rear Shock", Guid.NewGuid(), DateTime.Now);
					context.AddToProductModels(productModel);
					context.SaveChanges();
					label1.Text = "Save successful";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
				throw;
			}
		}

		private void AddChildProduct_Click(object sender, EventArgs e)
		{
			AddChildProductExample();
		}

		private void AddChildProductExample()
		{
			try
			{
				using (var context = new AdventureWorks2012Entities1())
				{
					var prodMod = context.ProductModels.Where(pm => pm.ProductModelID == 129).First();
					var prod = new Product();
					prod.Name = "Inverted Kayaba";
					prod.ProductNumber = "IKAYA-R209";
					prod.MakeFlag = true;
					prod.FinishedGoodsFlag = true;
					prod.Color = "Red";
					prod.SafetyStockLevel = 250;
					prod.ReorderPoint = 250;
					prod.StandardCost = 2500;
					prod.ListPrice = 3900;
					prod.Size = "40M";
					prod.SizeUnitMeasureCode = "CM";
					prod.WeightUnitMeasureCode = "LB";
					prod.Weight = (decimal)45.2;
					prod.DaysToManufacture = 5;
					prod.ProductLine = "S";
					prod.Class = "M";
					prod.Style = "M";
					prod.ProductSubcategoryID = 1;
					prod.SellStartDate = DateTime.Now;
					prod.ModifiedDate = DateTime.Now;
					prod.rowguid = Guid.NewGuid();
					prod.ProductModel = prodMod;
					context.SaveChanges();
					label1.Text = "Save successful";
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.InnerException.Message);
			}
		}

		private void DeleteChildProduct_Click(object sender, EventArgs e)
		{
			DeleteChildProductExample();
		}

		private void DeleteChildProductExample()
		{
			try
			{
				using (var context = new AdventureWorks2012Entities1())
				{
					var prod = context.Products.Where(p => p.ProductNumber == "IKAYA-R209").First();
					context.DeleteObject(prod);
					context.SaveChanges();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.InnerException.Message);
			}
		}

		private void Insert_Click(object sender, EventArgs e)
		{
			try
			{
				using (var context = new AdventureWorks2012Entities())
				{
					var busent = context.BusinessEntities.Where(p => p.BusinessEntityID == 292).First();
					var per = new Person();
					per.PersonType = "SC";
					per.NameStyle = true;
					per.Title = "Geek";
					per.FirstName = "Scott";
					per.MiddleName = "L";
					per.LastName = "Klein";
					per.Suffix = "Mr";
					per.EmailPromotion = 1;
					per.rowguid = Guid.NewGuid();
					per.ModifiedDate = DateTime.Now;
					busent.Person = per;
					context.SaveChanges();
					MessageBox.Show("record inserted");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.InnerException.Message);
			}
		}

		private void Update_Click(object sender, EventArgs e)
		{
			try
			{
				using (var context = new AdventureWorks2012Entities())
				{
					var per = context.People.Where(p => p.BusinessEntityID == 292).First();
					per.Title = "Head Geek";
					per.ModifiedDate = DateTime.Now;
					per.PersonType = "EM";
					context.SaveChanges();
					MessageBox.Show("record updated");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.InnerException.Message);
			}
		}

		private void Delete_Click(object sender, EventArgs e)
		{
			try
			{
				using (var context = new AdventureWorks2012Entities())
				{
					var per = context.People.Where(p => p.BusinessEntityID == 292).First();
					context.DeleteObject(per);
					context.SaveChanges();
					MessageBox.Show("record deleted");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void Select_Click(object sender, EventArgs e)
		{
			using (var context = new AdventureWorks2012Entities())
			{
				var query = from p in context.SelectPeople()
								where p.LastName.StartsWith("Kl")
								select p;
				foreach (var per in query)
				{
					listBox1.Items.Add(string.Format("{0} {1}", per.FirstName, per.LastName));
				}
			}
		}
	}
}
