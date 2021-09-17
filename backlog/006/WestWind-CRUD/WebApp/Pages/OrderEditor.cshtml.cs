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
    public class OrderEditorModel : PageModel
    {
        #region Constructor + DI Properties
        private readonly TableMaintenanceServices _services;
        public OrderEditorModel(TableMaintenanceServices services)
        {
            _services = services ?? throw new ArgumentNullException();
        }
        #endregion

        #region Model Properties
        [BindProperty]
        public string SelectedCustomer { get; set; }
        [BindProperty]
        public int SelectedOrder { get; set; }
        [BindProperty]
        public Order CurrentOrder { get; set; }
        public List<SelectListItem> OpenOrders { get; set; } = new();
        public List<SelectListItem> Employees { get; set; }
        public List<SelectListItem> Customers { get; set; }
        public List<SelectListItem> Addresses { get; set; } = new();
        public string ErrorMessage { get; set; }
        [TempData]
        public string Feedback { get; set; }
        [BindProperty(SupportsGet = true)]
        public int? OrderId { get; set; }
        [BindProperty]
        public string PartialAddress { get; set; }
        #endregion

        #region Request Handlers
        #region GET Request
        public void OnGet(int? id)
        {
            PopulateDropDowns();
            if (id.HasValue)
            {
                try
                {
                    CurrentOrder = _services.GetOrder(id.Value);
                    OrderId = CurrentOrder.OrderId;
                }
                catch (Exception ex)
                {
                    // Start with the assumption that the given exception is the root of the problem
                    Exception rootCause = ex;
                    // Loop to "drill-down" to what the original cause of the problem is
                    while (rootCause.InnerException != null)
                        rootCause = rootCause.InnerException;

                    ErrorMessage = rootCause.Message;
                }
            }
        }
        #endregion

        #region POST Requests
        public void OnPostSearchCustomerOrders()
        {
            PopulateDropDowns();
            OpenOrders = _services
                .ListOpenOrders(SelectedCustomer)
                .Select(x => new SelectListItem($"{x.OrderId} {(x.OrderDate.HasValue ? x.OrderDate.Value.ToShortDateString() : "-no date-")}", x.OrderId.ToString()))
                .ToList();
        }

        public IActionResult OnPostLookupOrder()
        {
            if (SelectedOrder > 0)
            {
                Feedback = "Order Found";
                return RedirectToPage(new { id = SelectedOrder });
            }
            else
            {
                Feedback = "Select an order before looking it up";
                return Page();
            }
        }

        public void OnPostSearchAddress()
        {
            PopulateDropDowns();
            Addresses = _services
                .ListAddresses(PartialAddress)
                .Select(x => new SelectListItem($"{x.Address1} {x.City}", x.AddressId.ToString()))
                .ToList();
        }

        public IActionResult OnPostClearForm()
        {
            return RedirectToPage(new { id = (int?)null });
        }

        public IActionResult OnPostAdd()
        {
            if (ModelState.IsValid) // recheck the validation based on the asp-validation-for
            {
                try
                {
                    _services.AddOrder(CurrentOrder); // Calling the ProductInventoryService.AddProduct method
                                                      // Use the POST-Redirect-GET pattern to prevent inadvertent resubmissions of POST requests
                    Feedback = "New customer order generated."; // Set the value of a [TempData] property for messages temporarily stored in cookies for redirect.
                    return RedirectToPage(new { id = CurrentOrder.OrderId });
                }
                catch (Exception ex)
                {
                    // Start with the assumption that the given exception is the root of the problem
                    Exception rootCause = ex;
                    // Loop to "drill-down" to what the original cause of the problem is
                    while (rootCause.InnerException != null)
                        rootCause = rootCause.InnerException;

                    ErrorMessage = rootCause.Message;
                    PopulateDropDowns();
                    return Page(); // Return the page as the POST result - This will preserve any user inputs
                }
            }
            else
            {
                ErrorMessage = $"There were {ModelState.ErrorCount} problems with your form submission.";
                foreach (var item in ModelState)
                {
                    foreach (var problem in item.Value.Errors)
                    {
                        ErrorMessage += $" Problem with {item.Key}: {problem.ErrorMessage}";
                    }
                }
                PopulateDropDowns();
                return Page();
            }
        }

        public IActionResult OnPostUpdate()
        {
            if (OrderId.HasValue && ModelState.IsValid)
            {
                try
                {
                    CurrentOrder.OrderId = OrderId.Value;
                    _services.UpdateOrder(CurrentOrder);
                    // Redirect to GET request since everything worked out OK
                    Feedback = "Order Updated.";
                    return RedirectToPage(new { id = CurrentOrder.OrderId });
                }
                catch (Exception ex)
                {
                    // Start with the assumption that the given exception is the root of the problem
                    Exception rootCause = ex;
                    // Loop to "drill-down" to what the original cause of the problem is
                    while (rootCause.InnerException != null)
                        rootCause = rootCause.InnerException;

                    ErrorMessage = rootCause.Message;
                    PopulateDropDowns();
                    return Page(); // Return the page as the POST result - This will preserve any user inputs
                }
            }
            else
            {
                ErrorMessage = "Invalid Order Information - Unable to update this order";
                PopulateDropDowns();
                return Page();
            }
        }

        public IActionResult OnPostDelete()
        {
            try
            {
                if (OrderId.HasValue)
                {
                    CurrentOrder.OrderId = OrderId.Value;
                    _services.DeleteOrder(CurrentOrder);
                    Feedback = "Customer Order has been removed from the database.";
                    return RedirectToPage(new { id = (int?)null }); // I need to remember to be explicit about having a "blank" product id
                }
                else
                {
                    ErrorMessage = "You can only delete an existing order.";
                    PopulateDropDowns();
                    return Page();
                }
            }
            catch (Exception ex)
            {
                // Start with the assumption that the given exception is the root of the problem
                Exception rootCause = ex;
                // Loop to "drill-down" to what the original cause of the problem is
                while (rootCause.InnerException != null)
                    rootCause = rootCause.InnerException;

                ErrorMessage = rootCause.Message;
                PopulateDropDowns();
                return Page(); // Return the page as the POST result - This will preserve any user inputs
            }
        }
        #endregion
        #endregion

        #region Private helper methods
        private void PopulateDropDowns()
        {
            Employees = _services
                .ListAllEmployees()
                .Select(x => new SelectListItem($"{x.FirstName} {x.LastName}", x.EmployeeId.ToString()))
                .ToList();

            Customers = _services
                .ListAllCustomers()
                .Select(x => new SelectListItem(x.CompanyName, x.CustomerId))
                .ToList();

        }

        #endregion
    }
}
