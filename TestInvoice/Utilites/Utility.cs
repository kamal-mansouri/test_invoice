using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;

namespace TestInvoice
{
    public static class Utility
    {
        

        public static string ToShamsi(this DateTime dt)
        {
            if (dt == null)
                return "";

            PersianCalendar pdate = new PersianCalendar();

            return (pdate.GetYear(dt).ToString() + "/" + pdate.GetMonth(dt).ToString() + "/" + pdate.GetDayOfMonth(dt).ToString());
        }
    }
}