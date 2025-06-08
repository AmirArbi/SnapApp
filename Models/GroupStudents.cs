using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace SDKTemplate.Models
{
    public class GroupStudents
    {
        public string GroupName { get; set; }
        public string StudentName { get; set; }
        public string ProfName { get; set; }
        public string Note { get; set; }
        public string School { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Absence { get; set; }
        public string Payment { get; set; }
        public string Gender { get; set; }
        public ImageSource Image { get; set; }
        public string SchoolYear { get; set; }
        public float Reduction { get; set; }
        public long student_id { get; set; }
        public bool IsStudying { get; set; }
    }
}
