<Query Kind="Expression">
  <Connection>
    <ID>9f795fec-6525-43c5-bbd0-2819df27768a</ID>
    <Server>.</Server>
    <Database>WestWind</Database>
  </Connection>
</Query>

// Lookup the category names, in alphabetical order
from row in Categories
orderby row.CategoryName // descending // uncomment for reverse alphabetical order
select row.CategoryName