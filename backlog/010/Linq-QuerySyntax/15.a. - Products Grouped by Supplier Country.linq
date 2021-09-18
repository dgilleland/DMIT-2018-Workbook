<Query Kind="Expression">
  <Connection>
    <ID>05a2444e-14ea-4451-ad3d-3398e9ff7898</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Show all the products grouped by the supplier's country. I want to see the name of the product, the quantity per unit and unit price, and the company name of the supplier. (Hint: Start your query from the **Products** table)
from item in Products
//  Product
group item by item.Supplier.Address.Country into ProductBySupplierAddressCountry
select new // come up with a ViewModel class name later
{
	Country = ProductBySupplierAddressCountry.Key,
	ItemWeSell = from thing in ProductBySupplierAddressCountry
	             //  Product
	             select new // come up with a ViewModel class name later
				 {//PropetyName = ValueFromThing
				    ProductName = thing.ProductName,
					QtyPerUnit = thing.QuantityPerUnit,
					Price = thing.UnitPrice,
					Supplier = thing.Supplier.CompanyName
				 }
}