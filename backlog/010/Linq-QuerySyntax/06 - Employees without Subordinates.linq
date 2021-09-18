<Query Kind="Expression">
  <Connection>
    <ID>9f795fec-6525-43c5-bbd0-2819df27768a</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// List all the employees who do not manage anyone.
from person in Employees
//   thing      thing[] 
where person.ReportsToChildren.Count == 0 // .ReportsToChildren refers to the subordinates
//     thing    thing[]
select new
{
	Name = person.FirstName + " " + person.LastName,
	JobTitle = person.JobTitle,
	Picture = person.Photo.ToImage()
}
