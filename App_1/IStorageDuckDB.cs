using DuckDB.NET.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App_1
{
    internal interface IStorageDuckDB
    {
        DuckDBConnection Connection { get; }
    }
}
