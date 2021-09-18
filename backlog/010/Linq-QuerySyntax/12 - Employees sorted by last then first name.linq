<Query Kind="Expression">
  <Connection>
    <ID>9f795fec-6525-43c5-bbd0-2819df27768a</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Look up the Employees, sorted by last name then first name. Show the last/first name as distinct properties.
from person in Employees
orderby person.LastName, person.FirstName // We are sorting by First Name within Last Name
select new
{
    person.FirstName,
	person.LastName
}