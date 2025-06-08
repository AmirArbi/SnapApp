using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDKTemplate.Models
{
    class ChangeStudent
    {
        public long id { get; set; }
        public string StudentName { get; set; }
        public string ProfName { get; set; }
        public string SchoolYear { get; set; }
        public int CurrentSession { get; set; }
        public string OldGroup { get; set; }
        public string MonthName { get; set; }
    }
}
