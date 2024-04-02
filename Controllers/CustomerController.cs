using Azure;
using LogGenerator.Models.Domain;
using LogGenerator.Models.ViewModels;
using LogGenerator.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LogGenerator.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository customerRepository;

        public CustomerController(ICustomerRepository custRepository)
        {
            this.customerRepository = custRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var entry = new AddCustomer { };
            return View(entry);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddCustomer addCustomerRequest)
        {
            var newCustomer = new NewCustomer
            {
                CustId = addCustomerRequest.CustId,
                Company = addCustomerRequest.Company,
                MobileNo = addCustomerRequest.MobileNo,
                Email = addCustomerRequest.Email,
                CustType = addCustomerRequest.CustType,
                Address = addCustomerRequest.Address,
                CustSince = addCustomerRequest.CustSince
            };

            await customerRepository.AddAsync(newCustomer);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //Call the repository
            var newCustomer = await customerRepository.GetAllASync();

            return View(newCustomer);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //Retrieve the result from the repository
            var newCustomer = await customerRepository.GetAsync(id);

            if (newCustomer != null)
            {
                //Map the domain model to the view model
                var model = new EditCustomerRequest
                {
                    CustId = newCustomer.CustId,
                    Company = newCustomer.Company,
                    MobileNo = newCustomer.MobileNo,
                    Email = newCustomer.Email,
                    CustType = newCustomer.CustType,
                    Address = newCustomer.Address,
                    CustSince = newCustomer.CustSince
                };
                return View(model);
            }

            //Pass data to view
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCustomerRequest editCustomerRequest)
        {
            // map view model back to domain model
            var customerDomainModel = new NewCustomer
            {
                CustId = editCustomerRequest.CustId,
                Company = editCustomerRequest.Company,
                MobileNo = editCustomerRequest.MobileNo,
                Email = editCustomerRequest.Email,
                CustType = editCustomerRequest.CustType,
                Address = editCustomerRequest.Address,
                CustSince = editCustomerRequest.CustSince
            };

            var updatedCustomer = await customerRepository.UpdateAsync(customerDomainModel);

            if (updatedCustomer != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show error notification
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditCustomerRequest editCustomerRequest)
        {
            // Talk to repository to delete this blog post and tags
            var deletedCustomer = await customerRepository.DeleteAsync(editCustomerRequest.CustId);

            if (deletedCustomer != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show error notification
            return RedirectToAction("Edit", new { id = editCustomerRequest.CustId });
        }
    }
}
