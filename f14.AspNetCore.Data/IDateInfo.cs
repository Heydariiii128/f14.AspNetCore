using System;
using System.Collections.Generic;
using System.Text;

namespace f14.AspNetCore.Data
{
    public interface IDateInfo
    {
        DateTime Created { get; set; }
        DateTime Modified { get; set; }
    }
}
