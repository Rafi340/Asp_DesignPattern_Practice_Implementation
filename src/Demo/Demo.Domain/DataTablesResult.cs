﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Domain
{
    public class DataTablesResult<T>
    {
        public IList<T> Data { get; set; }
        public int Total {  get; set; }
        public int TotalDisplay { get; set; }
    }
}
