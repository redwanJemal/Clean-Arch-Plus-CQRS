﻿using System;
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
        public Department Department { get; set; }
    }
}
