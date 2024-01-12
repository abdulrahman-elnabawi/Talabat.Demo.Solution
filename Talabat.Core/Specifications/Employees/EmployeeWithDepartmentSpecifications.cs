using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications.Employees
{
    public class EmployeeWithDepartmentSpecifications:BaseSpecification<Employee>
    {
        // This Constructor is used for Get All Employees
        public EmployeeWithDepartmentSpecifications()
        {
            AddInclude(E => E.Department);
        }
        // This Constructor is used for Get a Specific Employee
        public EmployeeWithDepartmentSpecifications(int id):base(E => E.Id == id)
        {
            AddInclude(E => E.Department);
        }
    }
}
