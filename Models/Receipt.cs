using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace SDKTemplate.Models
{
    public class Receipt
    {
        public long id { get; set; }
        public string StudentName { get; set; }
        public string ProfName { get; set; }
        public string GroupName { get; set; }
        public string MonthName { get; set; }
        public string SchoolYear { get; set; }
        public float PaidMoney { get; set; }
        public string PaiementMethod { get; set; }
        public string Note { get; set; }
        public DateTime DateTime { get; set; }
        public Visibility Visibility { get; set; }
    }
}
