using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrinterManuf_interfaces
{
    internal class EpsonPrinter:IPrinter2User
    {
        string printer_model;
        string printer_colour_level_cyan;
        string printer_colour_level_yellow;
        string printer_colour_level_magenta;
        string printer_black_level;
        string printer_connection;
        string printer_name;

        public void Print()
        {
            Console.WriteLine("Epson Printer Working Sucessfully!");

        }
    }
}
