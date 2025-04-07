using EmergenceResponse.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergenceResponse.Data
{
    public partial class Emergency
    {
        [NotMapped]
        public EmergencyStatus Status
        {
            get => (EmergencyStatus)StatusId;
            set => StatusId = (int)value;
        }
        [NotMapped]
        public EmergencyType Type
        {
            get => (EmergencyType)TypeId;
            set => TypeId = (int)value;
        }
        public string ResponceTime {
            get
            {
                if (ApprovalDate == null)
                    return "";
                var timedif = ApprovalDate - CreationDate;

                var timestring = "";
                timestring += timedif.Value.Days == 0 ? "" : timedif.Value.Days.ToString() + "days ";
                timestring += timedif.Value.Hours == 0 ? "" : timedif.Value.Hours.ToString() + "hrs ";
                timestring += timedif.Value.Minutes.ToString() + "min";

                return timestring;
            }
        }

    }
}
