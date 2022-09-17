using AutoMapper;
using EmployeeManagement.Business;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Controllers
{
    [Route("api/demointernalemployees")]
    public class DemoInternalEmployeesController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IMapper _mapper;

        public DemoInternalEmployeesController(IEmployeeService employeeService,
            IMapper mapper)
        {
            _employeeService = employeeService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<InternalEmployeeDto>> CreateInternalEmployee(
            InternalEmployeeForCreationDto internalEmployeeForCreation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var internalEmployee =
                    await _employeeService.CreateInternalEmployeeAsync(
                        internalEmployeeForCreation.FirstName, internalEmployeeForCreation.LastName);

            await _employeeService.AddInternalEmployeeAsync(internalEmployee);

            return CreatedAtAction("GetInternalEmployee",
                _mapper.Map<InternalEmployeeDto>(internalEmployee),
                new { employeeId = internalEmployee.Id });
        }


        [HttpGet(), Authorize()]
        public IActionResult GetProtectedInternalEmployees()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction(
                    "GetInternalEmployees", "ProtectedInternalEmployees");
            }

            return RedirectToAction("GetInternalEmployees", "InternalEmployees");
        }

    }
}
