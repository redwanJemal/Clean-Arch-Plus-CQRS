using AutoMapper;
using CQRSApp.Application.Queries.QueryModels;
using CQRSApp.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace CQRSApp.Application.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Department, DepartmentQueryModel>().ReverseMap();
            CreateMap<Department, DepartmentUpdateQueryModel>().ReverseMap();
        }
    }
}
