<Query Kind="Expression">
  <Connection>
    <ID>9f795fec-6525-43c5-bbd0-2819df27768a</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// List all the employees who do not have a manager
// (i.e.: they do not report to anyone).
from person in Employees
//   thing      thing[] 
where person.ReportsToEmployee == null // .ReportsToEmployee is the manager
//   thing     thing 
select new
{
  Name = person.FirstName + " " + person.LastName,
  JobTitle = person.JobTitle,
  Age = (DateTime.Now - person.BirthDate).Days / 365,
  Picture = person.Photo.ToImage(),
  Address = person.Address
}