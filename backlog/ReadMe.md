# Backlog

The folders here represent distinct demos and practice materials as referenced in class. Your instructor will give you directions on moving these folders to the `src/` folder as needed.

**Do not move these backlog items until you are told to do so** by your instructor; this will help you to avoid confusion about what is being done in each class. Your instructor may augment these backlog items with additional folders throughout the course by giving you *pull requests* via GitHub.

## Lessons

> ***Note:** A number of lessons are numbered in increments of 10 - this is because they are bulkier and will need to be broken down into smaller lessons. (Think the good old Basic, with line numbers).*

- [001](./001-StartHere/ReadMe.md) - **Start Here**
- [002](./002/ReadMe.md) - **More Markdown**
- [003](./003/ReadMe.md) - **Know Your Language**
- [004](./004/ReadMe.md) - **Know Your Tools**
- [005](./005/ReadMe.md) - **English/Klingon Dictionary**
- [006](./006/ReadMe.md) - **Awww CRUD**
- [010](./010/ReadMe.md) - **LINQ** *(multi-class lesson)*
- [020](./020/ReadMe.md) - TBA - Planning and Documentation ***How-To*** - [**Design Plan**](https://dmit-2018.github.io/demos/Northwind/CustomerOrders/Design.html#selecting-a-customer) sample
- [030](./030/ReadMe.md) - TBA - Implementation - Boilerplate Setup
  - *Combine with the next one on styling?*
- [040](./040/ReadMe.md) - TBA - Razor Pages Setup and Styling
- [050](./050/ReadMe.md) - TBA - Reverse Engineering Databases - Internal Classes - **`public interface IWestWindRepository`**
- [060](./060/ReadMe.md) - TBA - Configuring Services & Dependency Injection - About Page w. Database Version - ***Practice:** Contact Page with staff information* - ***Advanced Practice:** Make an Org Chart based on `ReportsTo` information*
- [070](./070/ReadMe.md) - TBA - Product Catalog - Implementation
- [080](./080/ReadMe.md) - TBA - Customer Orders - Implementation
- [090](./090/ReadMe.md) - TBA - Suppliers & Order Fulfillment - Implementation (*practice*) - (see Sep 2020 term)
- [100](./100/ReadMe.md) - TBA - **Security**
- [110](./110/ReadMe.md) - TBA - Resolving Merge Conflicts
- [120](./120/ReadMe.md) - TBA - [Publishing](#publishing)
- [130](./130/ReadMe.md) - TBA
- [140](./140/ReadMe.md) - TBA

## Future Demos

- Eye-Max
- Capstone-Teams
- Hire-Learning
- State-Registration

----

## Publishing

> 1.  **Web Deploy** - If you have IIS on server machine running and configured to receive Web Deploy requests, it will send all requested by IIS files. IIS will immediately start running new web page.
> 2.  **Web Deploy Package** - If you have IIS on server machine running and configured to receive a Web Deploy Package it will pack your whole web page, and will upload them to server machine. IIS will immediately start running new web page.
> 3.  **FTP** will upload files to FTP server (can be any OS that handles FTP), but be aware that this is **NOT** safe and whole upload process can be captured and compromised.
> 4.  **File system** will deploy all items required for launching a web service, with any method supported by current project configuration, into provided directory on your machine.
> 
> *Source: https://stackoverflow.com/a/42660168/191885
