<Query Kind="Expression">
  <Connection>
    <ID>05a2444e-14ea-4451-ad3d-3398e9ff7898</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Display all the company names and contact names for our customers, grouped by country
from row in Customers
//Customer \Customer[]/
group  row   by   row.Address.Country into CustomersByCountry
//    \what/      \       how       /      \ new item ref   /
//                                        \Group<KeyType, Customer>
//                              similar to \List<Customer>/, but with a data type for the key.
// Think of lines 2-4 as saying "from CustomersByCountry"
select new
{
   Country = CustomersByCountry.Key, // the key is "how" we have sorted the data
   Customers = from data in CustomersByCountry
               //          \IGrouping<string,Customers>/
               select new
               {
			       Company = data.CompanyName,
				   Contact = data.ContactName
               }
}