using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.DataFramework
{
    interface ILocation : IComponent
    {
        float X { get; set; }
        float Y { get; set; }
    }
}
