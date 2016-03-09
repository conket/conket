using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivs.Core.Data;

namespace Ivs.Core.Interface
{
    public interface IStockBl : IBl
    {
        StockResult InsertTransactionData(IDto insertDto);
        StockResult UpdateTransactionData(IDto insertDto);
    }
}
