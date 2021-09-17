using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WestWind.Collections;
using WestWind.DAL;
using WestWind.Entities;

namespace WestWind.BLL
{
    public class TableMaintenanceServices
    {
        #region Constructor & DI
        private readonly WestwindContext _context;
        public TableMaintenanceServices(WestwindContext context)
        {
            _context = context ?? throw new ArgumentNullException();
        }
        #endregion

        #region Assorted Search Methods
        public List<Category> ListCategories()
        {
            return _context.Categories.ToList(); // Gets all the categories in the database
        }

        public List<Supplier> ListSuppliers()
        {
            return _context.Suppliers.ToList(); // Gets all the suppliers in the database
        }

        public PartialList<Product> GetProducts(string partialProductName, int skip, int take)
        {
            var items = _context.Products
                                // The .Where() extension method allows me to filter the Products results
                                // This method uses a Lambda expression to do the filter check
                                .Where(item => item.ProductName.Contains(partialProductName))
                                // The .Skip() extension method says to "pass over" a certain number of rows
                                .Skip(skip)
                                // The .Take() extension method says to "retrieve" a certain number of rows
                                .Take(take)
                                // We can explicitly ask for the related data to be
                                // retreived as well as the Product information.
                                .Include(x => x.Supplier)
                                .Include(x => x.Category);
            var itemCount = _context.Products.Where(item => item.ProductName.Contains(partialProductName)).Count();
            return new PartialList<Product>(itemCount, items.ToList());
        }

        #region Related Table Data for Orders
        public List<Customer> ListAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public List<Employee> ListAllEmployees()
        {
            return _context.Employees.ToList();
        }

        public List<Address> ListAllAddresses()
        {
            return _context.Addresses.ToList();
        }

        public IEnumerable<Order> ListOpenOrders(string selectedOrder)
        {
            var orders = _context.Orders.Where(x => x.CustomerId == selectedOrder && !x.Shipped);
            return orders.ToList();
        }

        public List<Product> ListMatchingProducts(string partialProductName)
        {
            IEnumerable<Product> results;
            if (string.IsNullOrWhiteSpace(partialProductName))
                results = _context.Products;
            else
                results = _context.Products.Where(x => x.ProductName.Contains(partialProductName));
            return results.ToList();
        }

        public List<OrderDetail> ListOrderDetails(string selectedCustomer, string partialProductName)
        {
            var results = _context.OrderDetails.Include(x => x.Product).Include(x => x.Order);
            return results.Where(x => x.Order.CustomerId == selectedCustomer && x.Product.ProductName.Contains(partialProductName)).ToList();
        }

        public List<Address> ListAddresses(string partialAddress)
        {
            var results = _context.Addresses.Where(x => x.Address1.Contains(partialAddress));
            return results.ToList();
        }
        #endregion
        #endregion

        #region Products CRUD
        public List<Product> GetProducts()
        {
            var result = _context.Products;
            return result.ToList();
        }

        public Product GetProduct(int productId)
        {
            return _context.Products.Find(productId); // Find a single product with the PK of productID
        }

        public int AddProduct(Product item)
        {
            _context.Products.Add(item); // Put the new product item in the DbSet<Product>
            _context.SaveChanges(); // Examine the database context to see if there are any changes in the DbSet items
                                    // and then will save those changes to the database
                                    // Because the ProductID on the database table is an IDENTITY column, the database will generate the
                                    // value for this newly added item
                                    // I will see that new value reflected in my Product item object.
            return item.ProductId; // Send back the database-generated PK value for this new row of data.
        }

        public void UpdateProduct(Product item)
        {
            // The .Entry() method will fetch the most recent information on the Product object as it exists in the database.
            // It can do that because the Product item we are supplying has the ProductID
            EntityEntry<Product> existing = _context.Entry(item);
            // Next, I tell EF that I do indeed want to modify whatever is in the database with
            // what item information I have been given through my parameter variable.
            existing.State = EntityState.Modified;
            _context.SaveChanges(); // Perform an update on the database with the changes supplied
        }

        public void DeleteProduct(Product item)
        {
            EntityEntry<Product> existing = _context.Entry(item);
            existing.State = EntityState.Deleted; // I want to delete this from the databae
            _context.SaveChanges();
        }
        #endregion

        #region Order CRUD
        public Order GetOrder(int orderId)
        {
            return _context.Orders.Find(orderId);
        }

        public void AddOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
        }

        public void UpdateOrder(Order item)
        {
            EntityEntry<Order> existing = _context.Entry(item);
            existing.State = EntityState.Modified; // I want to delete this from the databae
            _context.SaveChanges();
        }

        public void DeleteOrder(Order item)
        {
            EntityEntry<Order> existing = _context.Entry(item);
            existing.State = EntityState.Deleted; // I want to delete this from the databae
            _context.SaveChanges();
        }
        #endregion

        #region OrderDetail CRUD
        public OrderDetail GetOrderDetail(int orderDetailId)
        {
            return _context.OrderDetails.Find(orderDetailId);
        }

        public void AddOrderDetail(OrderDetail detail)
        {
            _context.OrderDetails.Add(detail);
            _context.SaveChanges();
        }

        public void UpdateOrderDetail(OrderDetail item)
        {
            EntityEntry<OrderDetail> existing = _context.Entry(item);
            existing.State = EntityState.Modified; // I want to delete this from the databae
            _context.SaveChanges();
        }

        public void DeleteOrderDetail(OrderDetail item)
        {
            EntityEntry<OrderDetail> existing = _context.Entry(item);
            existing.State = EntityState.Deleted; // I want to delete this from the databae
            _context.SaveChanges();
        }
        #endregion
    }
}
