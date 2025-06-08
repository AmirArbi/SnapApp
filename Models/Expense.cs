using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.Models
{
    public class Expense
    {
        public int id { get; set; }
        public float Amount { get; set; }
        public string Cause { get; set; }
        public string Username { get; set; }
        public string AdminName { get; set; }
        public DateTime DateTime { get; set; }
    }
}
