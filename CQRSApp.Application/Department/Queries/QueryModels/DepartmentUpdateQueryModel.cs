using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSApp.Application.Queries.QueryModels
{
    public class DepartmentUpdateQueryModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public DepartmentUpdateQueryModel(Guid id, string name)
        {
            Id = id;
            Name = name;
        }
        public DepartmentUpdateQueryModel()
        {

        }
    }
}
