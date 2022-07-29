using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.ViewModels.Test
{
    public class InformationTestFormModel
    {
        public int Id { get; init; }

        public string Type { get; set; }

        public string Link { get; set; }

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public TimeSpan Continuity { get; set; }
    }
}
