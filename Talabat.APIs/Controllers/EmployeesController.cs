using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications.Employees;

namespace Talabat.APIs.Controllers
{
    public class EmployeesController : BaseApiController
    {
        private readonly IGenericRepository<Employee> _employeesRepo;

        public EmployeesController(IGenericRepository<Employee> employeesRepo)
        {
            _employeesRepo = employeesRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<Employee>>> GetEmployees()
        {
            var spec = new EmployeeWithDepartmentSpecifications();

            var employees = await _employeesRepo.GetAllWithSpecAsync(spec);

            return Ok(employees);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<IReadOnlyList<Employee>>> GetEmployee(int id)
        {
            var spec = new EmployeeWithDepartmentSpecifications(id);

            var employee = await _employeesRepo.GetAllWithSpecAsync(spec);

            return Ok(employee);
        }
    }
}
