<Query Kind="Expression">
  <Connection>
    <ID>05a2444e-14ea-4451-ad3d-3398e9ff7898</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// List all the customers grouped by country and region.
from row in Customers
// The following Key data type is an anonymous type with two properties: Country, Region
// group row by new { row.Address.Country, row.Address.Region } into CustomerGroups
// The following Key data type is an Entity/Object
group row by row.Address into CustomerGroups
//   data type: \Address/ object
select new
{
   Key = CustomerGroups.Key,
   Country = CustomerGroups.Key.Country,
   Region = CustomerGroups.Key.Region,
   Customers = from data in CustomerGroups
               select new
               {
                   Company = data.CompanyName,
                   City = data.Address.City
               }
}