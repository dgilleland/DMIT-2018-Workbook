<Query Kind="Expression">
  <Connection>
    <ID>05a2444e-14ea-4451-ad3d-3398e9ff7898</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Show all the products grouped by the supplier's company name and country.
// I want to see the name of the product, the quantity per unit and unit price.
// (Hint: Start your query from the **Products** table)
// Extra: Sort my results by the supplier name.
from item in Products
group item by new { item.Supplier.CompanyName, item.Supplier.Address.Country }
	into ProductGroup
orderby ProductGroup.Key.CompanyName
select new // ViewModel name
{
	//Grouping = ProductGroup.Key,
	Suppliers = ProductGroup.Key.CompanyName + " (" + ProductGroup.Key.Country + ")",
	Products = from thing in ProductGroup
	           select new
			   {
			   		ProductName = thing.ProductName,
					QtyPerUnit = thing.QuantityPerUnit,
					Price = thing.UnitPrice
			   }
}