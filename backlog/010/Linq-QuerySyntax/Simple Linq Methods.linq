<Query Kind="Expression">
  <Connection>
    <ID>05a2444e-14ea-4451-ad3d-3398e9ff7898</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

//Employees.First(person => person.TitleOfCourtesy == "Mr.") // .First() expects a minimum of 1
//Employees.Single(person => person.TitleOfCourtesy == "Dr.")// .Single() expects exactly 1
// Employees.FirstOrDefault(person => person.TitleOfCourtesy == "Jr.") // .FirstOrDefault() expect min. of 0
//Employees.SingleOrDefault(person => person.TitleOfCourtesy == "Jr.") // .SingleOrDefault() expects 0 or 1
//Addresses.OrderBy(place => place.City).Select(place => place.City).Distinct()
Products.Skip(3).Take(2)
