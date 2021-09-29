using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WestWind.BLL;
using WestWind.Entities;

namespace WebApp.Pages
{
    public class OrderDetailsEditorModel : PageModel
    {
        #region Constructor & DI
        private readonly TableMaintenanceServices _services;
        public OrderDetailsEditorModel(TableMaintenanceServices services)
        {
            _services = services ?? throw new ArgumentNullException();
        }
        #endregion

        #region Properties
        [BindProperty(SupportsGet = true)]
        public int? OrderDetailId { get; set; }
        [TempData]
        public string _SelectedCustomer { get; set; }
        [TempData]
        public string _PartialProductName { get; set; }
        [TempData]
        public string PostRedirectFeedback { get; set; }
        public string ErrorMessage { get; set; }


        [BindProperty]
        public OrderDetail OrderItem { get; set; }
        [BindProperty]
        public string SelectedCustomer { get; set; }
        [BindProperty]
        public string PartialProductName { get; set; }
        [BindProperty]
        public int SelectedOrder { get; set; }

        public List<SelectListItem> CustomerOrders { get; set; } = new();
        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> Products { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
        #endregion

        #region Request Handlers
        #region GET Request
        public void OnGet()
        {
            if (OrderDetailId.HasValue)
            {
                // An existing order detail item
                OrderItem = _services.GetOrderDetail(OrderDetailId.Value);
            }
            else
            {
                // Searching for existing or adding a new item
                SelectedCustomer = _SelectedCustomer;
                PartialProductName = _PartialProductName;
            }
            PopulateSelectionLists();
        }
        #endregion

        #region POST Requests
        public IActionResult OnPost()
        {
            return RedirectToPage(new { OrderDetailId = OrderDetailId });
        }

        public IActionResult OnPostNarrowResults()
        {
            _SelectedCustomer = SelectedCustomer;
            _PartialProductName = PartialProductName;
            return RedirectToPage();
        }

        public IActionResult OnPostAdd()
        {
            try
            {
                if (!OrderDetailId.HasValue)
                {
                    _services.AddOrderDetail(OrderItem);
                    PostRedirectFeedback = "Product order details have been updated.";
                    return RedirectToPage(new { OrderDetailId = OrderItem.OrderDetailId });
                }
                else
                {
                    ErrorMessage = "You should clear the form before trying to add a new order detail item.";
                    PopulateSelectionLists();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex);
                PopulateSelectionLists();
                return Page(); // Return the page as the POST result - This will preserve any user inputs
            }
        }

        public IActionResult OnPostUpdate()
        {
            try
            {
                if (OrderDetailId.HasValue)
                {
                    OrderItem.OrderDetailId = OrderDetailId.Value;
                    _services.UpdateOrderDetail(OrderItem);
                    PostRedirectFeedback = "Product order details have been updated.";
                    return RedirectToPage(new { OrderDetailId = OrderDetailId });
                }
                else
                {
                    ErrorMessage = "You can only update an existing item on the order.";
                    PopulateSelectionLists();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex);
                PopulateSelectionLists();
                return Page(); // Return the page as the POST result - This will preserve any user inputs
            }
        }

        public IActionResult OnPostDelete()
        {
            try
            {
                if (OrderDetailId.HasValue)
                {
                    OrderItem.OrderDetailId = OrderDetailId.Value;
                    _services.DeleteOrderDetail(OrderItem);
                    PostRedirectFeedback = "Product has been removed from the order.";
                    return RedirectToPage(new { OrderDetailId = (int?)null });
                }
                else
                {
                    ErrorMessage = "You can only delete an existing item on the order.";
                    PopulateSelectionLists();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                SetErrorMessage(ex);
                PopulateSelectionLists();
                return Page(); // Return the page as the POST result - This will preserve any user inputs
            }
        }

        public IActionResult OnPostClearForm()
        {
            return RedirectToPage( new { OrderDetailId = (int?)null });
        }
        #endregion
        #endregion

        #region Private helper methods
        private void SetErrorMessage(Exception ex)
        {
            // Start with the assumption that the given exception is the root of the problem
            Exception rootCause = ex;
            // Loop to "drill-down" to what the original cause of the problem is
            while (rootCause.InnerException != null)
                rootCause = rootCause.InnerException;

            ErrorMessage = rootCause.Message;
        }

        private void PopulateSelectionLists()
        {
            Customers = _services
                .ListAllCustomers()
                .Select(x => new SelectListItem($"{x.CompanyName}", x.CustomerId))
                .ToList();
            CustomerOrders = _services
                .ListOpenOrders(SelectedCustomer)
                .Select(x => new SelectListItem($"{x.OrderId} {(x.OrderDate.HasValue ? x.OrderDate.Value.ToShortDateString() : "-no date-")}", x.OrderId.ToString()))
                .ToList();
            Products = _services
                .ListMatchingProducts(PartialProductName)
                .Select(x => new SelectListItem(x.ProductName, x.ProductId.ToString()))
                .ToList();
            OrderDetails = _services
                .ListOrderDetails(SelectedCustomer, PartialProductName);
        }
        #endregion
    }
}
