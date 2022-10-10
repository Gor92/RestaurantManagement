﻿using System;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace RestaurantManagement.Core
{
    public enum SortDirection
    {
        [Description("Descending")]
        Desc = 0,

        [Description("Ascending")]
        Asc = 1,
    }

    [Flags]
    public enum AccessLevel
    {
        Add = 1,
        Update = 2,
        Read = 3,
        Delete = 4
    }

    public enum ResourceType
    {
        Undefined,
        Order
    }
}
