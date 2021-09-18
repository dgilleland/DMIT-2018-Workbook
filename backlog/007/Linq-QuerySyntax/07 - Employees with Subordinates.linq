<Query Kind="Expression">
  <Connection>
    <ID>9f795fec-6525-43c5-bbd0-2819df27768a</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// List all the employees who are managers.
from person in Employees
//   thing      thing[] 
where person.ReportsToChildren.Count > 0
//     thing    thing[]
select new // Manager
{
	Name = person.FirstName + " " + person.LastName,
	JobTitle = person.JobTitle,
	Picture = person.Photo.ToImage(),
	Subordinates = from emp in person.ReportsToChildren
	//                Employee        \  DbSet<Employee>
	               select new // Person
				   {
				   		GivenName = emp.FirstName,
						Surname = emp.LastName
				   }
}
