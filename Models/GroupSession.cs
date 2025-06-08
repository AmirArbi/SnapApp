using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SDKTemplate.Models
{
    public class GroupSession
    {
        public string GroupName { get; set; }
        public string ProfName { get; set; }
        public string StudentName { get; set; }
        public long session_id { get; set; }
        public DateTime Date { get; set; }
        public string MonthName { get; set; }
        public string SchoolYear { get; set; }
        public string Presence { get; set; }
        public string School { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public bool IsPresent { get; set; }
        public bool IsAbsent { get; set; }
        public bool IsCounting { get; set; }
        public bool IsDeleted { get; set; }
        public ImageSource Image { get; set; }
    }
}
