using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSession1912.data
{
    [Table("Departments")]
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Employee> liEmp { get; set; }
    }
}
