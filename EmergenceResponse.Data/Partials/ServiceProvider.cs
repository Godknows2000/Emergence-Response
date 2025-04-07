using EmergenceResponse.Lib;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergenceResponse.Data
{
    public partial class ServiceProvider
    {
        [NotMapped]
        public ServiceProviderStatus Status
        {
            get => (ServiceProviderStatus)StatusId;
            set => StatusId = (int)value;
        }
        [NotMapped]
        public EmergencyType Type
        {
            get => (EmergencyType)TypeId;
            set => TypeId = (int)value;
        }
    }
}
