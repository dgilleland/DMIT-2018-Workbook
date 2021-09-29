<Query Kind="Expression">
  <Connection>
    <ID>05a2444e-14ea-4451-ad3d-3398e9ff7898</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Supplier Product Counts
// Show me the company name of each supplier along with a count of how many products that supplier provides
// to us. Also do a break-down of the product counts by category for each supplier.
from company in Suppliers
select new
{
	Company = company.CompanyName,
	ProductCount = company.Products.Count(),
	ProductsByCategory = from item in company.Products
	                    //  \Product/
	                     group item by item.Category.CategoryName
						 into ProductByCategory // IGrouping<,> therefore, it's a collection
						 select new
						 {
						 	ProductByCategory.Key,
							Count = ProductByCategory.Count()
						 }
}