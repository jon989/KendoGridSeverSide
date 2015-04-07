using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace KndoGrid1.Helpers
{
    public class FilteringHelper
    {
        /* case 1
filter[filters][0][field]=carrierTrackingNumber
filter[filters][0][operator]=eq
filter[filters][0][value]=3
filter[filters][1][field]=orderQty
filter[filters][1][operator]=eq
filter[filters][1][value]=3
filter[filters][2][field]=productID
filter[filters][2][operator]=eq
filter[filters][2][value]=22 
filter[logic]=and
         */
        /* case 2
filter[filters][0][field]=carrierTrackingNumber
filter[filters][0][operator]=eq
filter[filters][0][value]=1
filter[logic]=and
        */

        /* case 3
filter[filters][0][field]=carrierTrackingNumber
filter[filters][0][operator]=eq
filter[filters][0][value]=1
filter[filters][1][field]=carrierTrackingNumber
filter[filters][1][operator]=eq
filter[filters][1][value]=2
filter[logic]=and
                */


        /* case 4
filter[filters][0][field]=carrierTrackingNumber
filter[filters][0][operator]=eq
filter[filters][0][value]=1
filter[filters][1][field]=carrierTrackingNumber
filter[filters][1][operator]=eq
filter[filters][1][value]=2
filter[filters][2][logic]=or
filter[filters][2][filters][0][field]=orderQty
filter[filters][2][filters][0][operator]=eq
filter[filters][2][filters][0][value]=3
filter[filters][2][filters][1][field]=orderQty
filter[filters][2][filters][1][operator]=eq
filter[filters][2][filters][1][value]=4
filter[logic]=and
         * 
         * 
         */
        /*
         * 
filter[filters][0][field]=carrierTrackingNumber
filter[filters][0][operator]=eq
filter[filters][0][value]=1
filter[filters][1][field]=carrierTrackingNumber
filter[filters][1][operator]=eq
filter[filters][1][value]=2
filter[logic]=or
         * 
         */


        private static readonly Regex GroupRegex = new Regex(@"^filter\[(\d*)\]\[(filters)\]$", RegexOptions.IgnoreCase);
       
    }
}