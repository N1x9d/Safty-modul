using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM
{
    public class TimeWork
    {

        public static long getTimeInSecods(TimeSpan dt)
        {
            long ress = dt.Days;
            ress *= 86400;
            ress += 3600 * dt.Hours + 60 * dt.Minutes + dt.Seconds;
            return ress;
        }
    }
}
