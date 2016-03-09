using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ivs.Core.Interface;
using DevExpress.XtraPrinting;
using DevExpress.XtraReports.UI;

namespace Ivs.Core.Common
{
    public class PrintCommandHandler : ICommandHandler, IPrint
    {
        public XtraReport Report { get; set; }
        public bool HasPrinted { get; protected set; }
        public int ReturnCode { get; protected set; }
        public IDto Dto { get; set; }

        public virtual void HandleCommand(PrintingSystemCommand command, object[] args, IPrintControl control, ref bool handled)
        {
            if (!CanHandleCommand(command, control))
            {
                return;
            }

            try
            {
                if (Report != null)
                {
                    Report.Print();
                }
                
            }
            catch (Exception)
            {

            }

            handled = true;
            ////To do something
            //if (!this.BeforePrint())
            //{
            //    handled = true;
            //    //return;
            //}

            ////Report is printed
            //this.HasPrinted = true;
        }

        public virtual bool CanHandleCommand(PrintingSystemCommand command, IPrintControl control)
        {
            // This handler is used for the ExportGraphic command.
            return (command == PrintingSystemCommand.Print || command == PrintingSystemCommand.PrintDirect);
        }

        public virtual bool BeforePrint()
        {
            return true;
        }
    }
}
