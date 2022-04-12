using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiSession1912.data
{
    [Table("Employees")]
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [ForeignKey("dept")]
        public int Department_Id { get; set; }

        public Department dept { get; set; }
    }
}
