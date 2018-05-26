using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XCars.ViewModels
{
    public class OrderVM
    {
        public int LMI_PREREQUEST { get; set; }

        public int LMI_MERCHANT_ID { get; set; }

        public double LMI_PAYMENT_AMOUNT { get; set; }

        public string LMI_PAYMENT_NO { get; set; }

        public int LMI_MODE { get; set; }

        public string LMI_PAYMENT_SYSTEM { get; set; }

        public string LMI_PAYMENT_DESC { get; set; }

        //********************************************
        public int LMI_SYS_PAYMENT_ID { get; set; }

        public DateTime? LMI_SYS_PAYMENT_DATE { get; set; }

        public string LMI_PAYER_IDENTIFIER { get; set; }

        public string LMI_HASH { get; set; }
    }
}