using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.Models
{
    class Presence
    {
        public string StudentName { get; set; }
        public string ProfName { get; set; }
        public string GroupName { get; set; }
        public string SchoolYear { get; set; }
        public string MonthName { get; set; }
        public long session_id { get; set; }
        public bool IsPresent { get; set; }
    }
}
