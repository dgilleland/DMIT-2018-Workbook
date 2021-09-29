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
    public class ProductEditorModel : PageModel
    {
        #region Constructor and Dependencies
        private readonly TableMaintenanceServices _service;
        public ProductEditorModel(TableMaintenanceServices service)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }
        #endregion

        #region Properties
        [BindProperty(SupportsGet = true)]
        public int? Id { get; set; }
        [TempData]
        public string FeedbackMessage { get; set; }
        public List<Category> Categories { get; set; }
        public List<SelectListItem> Suppliers { get; set; }
        public string ErrorMessage { get; set; }

        [BindProperty]
        public Product ProductItem { get; set; }
        #endregion

        #region OnGet
        public void OnGet()
        {
            if (Id.HasValue) // A nullable int will have a property called .HasValue
            {
                ProductItem = _service.GetProduct(Id.Value); // The .Value property of the nullable int is an actual int
                Id = ProductItem?.ProductId; // to "reset" the id to null if the product was not found
            }
            PopulateDropDown();
        }
        #endregion

        #region OnPost
        public IActionResult OnPostCancel()
        {
            return RedirectToPage(new { Id = Id });
        }

        // An IActionResult allows me more control in communicating the results of this request to the web browser
        public IActionResult OnPostAdd()
        {
            if (Id.HasValue)
            {
                ErrorMessage = "This is an existing product, and cannot be re-added.";
                PopulateDropDown();
                return Page(); // Return the page as the POST result - This will preserve any user inputs
            }
            else if (ModelState.IsValid) // recheck the validation based on the asp-validation-for
            {
                try
                {
                    _service.AddProduct(ProductItem); // Calling the ProductInventoryService.AddProduct method
                                                      // Use the POST-Redirect-GET pattern to prevent inadvertent resubmissions of POST requests
                    FeedbackMessage = "New Product Added.";
                    return RedirectToPage(new { id = ProductItem.ProductId });
                }
                catch (Exception ex)
                {
                    // Start with the assumption that the given exception is the root of the problem
                    Exception rootCause = ex;
                    // Loop to "drill-down" to what the original cause of the problem is
                    while (rootCause.InnerException != null)
                        rootCause = rootCause.InnerException;

                    ErrorMessage = rootCause.Message;
                    PopulateDropDown();
                    return Page(); // Return the page as the POST result - This will preserve any user inputs
                }
            }
            PopulateDropDown();
            return Page();
        }

        public IActionResult OnPostUpdate()
        {
            if (Id.HasValue && ModelState.IsValid)
            {
                try
                {
                    ProductItem.ProductId = Id.Value;
                    _service.UpdateProduct(ProductItem);
                    // Redirect to GET request since everything worked out OK
                    FeedbackMessage = "Product Updated.";
                    return RedirectToPage(new { id = ProductItem.ProductId });
                }
                catch (Exception ex)
                {
                    // Start with the assumption that the given exception is the root of the problem
                    Exception rootCause = ex;
                    // Loop to "drill-down" to what the original cause of the problem is
                    while (rootCause.InnerException != null)
                        rootCause = rootCause.InnerException;

                    ErrorMessage = rootCause.Message;
                    PopulateDropDown();
                    return Page(); // Return the page as the POST result - This will preserve any user inputs
                }
            }
            else
            {
                ErrorMessage = "Invalid Product Details - Unable to Update Product";
                PopulateDropDown();
                return Page();
            }
        }

        public IActionResult OnPostDelete()
        {
            try
            {
                if (Id.HasValue)
                {
                    ProductItem.ProductId = Id.Value;
                    _service.DeleteProduct(ProductItem);
                    FeedbackMessage = "Product has been removed from the database.";
                    return RedirectToPage(new { id = (int?)null }); // I need to remember to be explicit about having a "blank" product id
                }
                else
                {
                    ErrorMessage = "You can only delete an existing product.";
                    PopulateDropDown();
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
                PopulateDropDown();
                return Page(); // Return the page as the POST result - This will preserve any user inputs
            }
        }
        #endregion

        #region Private Methods
        private void PopulateDropDown()
        {
            Categories = _service.ListCategories();
            Suppliers = _service.ListSuppliers()
                                .Select(x => new SelectListItem(x.CompanyName, x.SupplierId.ToString()))
                                .ToList();
        }
        #endregion
    }
}
