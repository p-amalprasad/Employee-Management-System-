using Azure;
using LogGenerator.Models.Domain;
using LogGenerator.Models.ViewModels;
using LogGenerator.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace LogGenerator.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository empRepository)
        {
            this.employeeRepository = empRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var entry = new AddEmployee { };
            return View(entry);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployee addEmployeeRequest)
        {
            var newEmployee = new NewEmployee
            {
                EmpId = addEmployeeRequest.EmpId,
                Name = addEmployeeRequest.Name,
                MobileNo = addEmployeeRequest.MobileNo,
                About = addEmployeeRequest.About,
                JobDesc = addEmployeeRequest.JobDesc,
                Address = addEmployeeRequest.Address,
                Role = addEmployeeRequest.Role,
                DoB = addEmployeeRequest.DoB
            };

            await employeeRepository.AddAsync(newEmployee);

            return RedirectToAction("List");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            //Call the repository
            var newEmployee = await employeeRepository.GetAllASync();

            return View(newEmployee);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            //Retrieve the result from the repository
            var newEmployee = await employeeRepository.GetAsync(id);

            if (newEmployee != null)
            {
                //Map the domain model to the view model
                var model = new EditEmployeeRequest
                {
                    EmpId = newEmployee.EmpId,
                    Name = newEmployee.Name,
                    MobileNo = newEmployee.MobileNo,
                    About = newEmployee.About,
                    JobDesc = newEmployee.JobDesc,
                    Address = newEmployee.Address,
                    Role = newEmployee.Role,
                    DoB = newEmployee.DoB
                };
                return View(model);
            }

            //Pass data to view
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditEmployeeRequest editEmployeeRequest)
        {
            // map view model back to domain model
            var employeeDomainModel = new NewEmployee
            {
                EmpId = editEmployeeRequest.EmpId,
                Name = editEmployeeRequest.Name,
                MobileNo = editEmployeeRequest.MobileNo,
                About = editEmployeeRequest.About,
                JobDesc = editEmployeeRequest.JobDesc,
                Address = editEmployeeRequest.Address,
                Role = editEmployeeRequest.Role,
                DoB = editEmployeeRequest.DoB
            };

            var updatedEmployee = await employeeRepository.UpdateAsync(employeeDomainModel);

            if (updatedEmployee != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show error notification
            return RedirectToAction("Edit");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditEmployeeRequest editEmployeeRequest)
        {
            // Talk to repository to delete this blog post and tags
            var deletedEmployee = await employeeRepository.DeleteAsync(editEmployeeRequest.EmpId);

            if (deletedEmployee != null)
            {
                // Show success notification
                return RedirectToAction("List");
            }

            // Show error notification
            return RedirectToAction("Edit", new { id = editEmployeeRequest.EmpId });
        }
    }
}
