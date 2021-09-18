<Query Kind="Expression">
  <Connection>
    <ID>05a2444e-14ea-4451-ad3d-3398e9ff7898</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// List all the orders showing the order ID, Company Name, Freight Charge,
// and Subtotal (no discounts) as well as the Subtotal of the discount.
from sale in Orders
select new
{
    OrderId = sale.OrderID,
    Company = sale.Customer.CompanyName,
    FreightCharge = sale.Freight,
	LineItemCount = sale.OrderDetails.Count(), // Add in another line to calculate the LineItemCount
    Subtotal = sale.OrderDetails.Sum(lineItem => lineItem.Quantity * lineItem.UnitPrice),
    DiscountSubtotal = 
        sale.OrderDetails.Sum(lineItem =>
                              lineItem.Quantity * lineItem.UnitPrice * (decimal)lineItem.Discount),
	LineItems = from item in sale.OrderDetails
	            select new
				{
					Name = item.Product.ProductName,
					Qty = item.Quantity,
					Price = item.UnitPrice,
					Discount = item.Discount
				}
}