using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSApp.Application.Queries.QueryModels
{
    public class DepartmentQueryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string SchoolName { get; set; }

        public DepartmentQueryModel(Guid id, string name, string schollName)
        {
            Id = id;
            Name = name;
            SchoolName = schollName;
        }
        public DepartmentQueryModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public DepartmentQueryModel()
        {

        }
    }
}
