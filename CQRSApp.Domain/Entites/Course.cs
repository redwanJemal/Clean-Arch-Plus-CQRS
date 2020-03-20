using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSApp.Domain.Entites
{
    public class Course
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int CreditHour { get; set; }
        public bool HasPreRequest { get; set; }
        public Guid DepartmentId { get; set; }
        public Department Department { get; set; }
        public Course()
        {

        }
        public Course(Guid departmentId, string name, int creditHour, bool hasPreRequest)
        {
            Name = name;
            CreditHour = creditHour;
            HasPreRequest = hasPreRequest;
            DepartmentId = departmentId;
        }
    }
}
