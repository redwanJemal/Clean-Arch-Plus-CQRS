using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace CQRSApp.Domain.Entites
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SchoolName { get; set; }
        public ICollection<Course> Courses { get; set; }

        public Department()
        {
            Courses = new Collection<Course>();
        }
        public Department(string name, string schoolName)
        {
            Name = name;
            SchoolName = schoolName;
        }

    }
}
