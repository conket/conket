using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivs.Core.Common;
using Ivs.Core.Data;

namespace Ivs.Core.Validation
{
    public class GridValidationResult : ValidationResult
    {
        private int _gridNumber = 1;
        public int GridNumber
        {
            get
            {
                return _gridNumber;
            }
        }

        public GridValidationResult(int rowIndex, string fieldName, CommonData.IsValid isValid, IvsMessage message)
            : base(rowIndex, fieldName, isValid, message)
        {
            this._gridNumber = 1;
        }

        public GridValidationResult(int gridNumber, int rowIndex, string fieldName, CommonData.IsValid isValid, IvsMessage message) 
            : base(rowIndex, fieldName, isValid, message)
        {
            this._gridNumber = gridNumber;
        }
        
    }
}
