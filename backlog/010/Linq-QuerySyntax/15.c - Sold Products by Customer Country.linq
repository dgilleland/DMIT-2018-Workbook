<Query Kind="Expression">
  <Connection>
    <ID>05a2444e-14ea-4451-ad3d-3398e9ff7898</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Show all the sold products grouped by the customer's country. Show the product name and the customer's company name. (Hint: Start your query from the **OrderDetails** table)
from item in OrderDetails
// OrderDetail
group item by item.Order.Customer.Address.Country
	into SoldItemByCountry
select new
{
	// Some good property name for the key, and the value of the key
	ShippedToCountry = SoldItemByCountry.Key,
	Products = from soldItem in SoldItemByCountry
	           //  OrderDetail
	           select new
			   {
			   	   Product = soldItem.Product.ProductName,
				   //                .Product <- Navigation Property in the OrderDetail class
				   //   data type    \Product/
				   //                        .ProductName <- Property in the Product class
				   Customer = soldItem.Order.Customer.CompanyName
				   //                 .Order  <- Navigation Property in the OrderDetail class
				   //   data type    \ Order /
				   //                       .Customer <- Navigation Property in Order class
				   //   data type           \Customer/
				   //                                .CompanyName <- Property in the Customer class
			   }
}