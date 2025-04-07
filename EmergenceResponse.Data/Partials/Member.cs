using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmergenceResponse.Data
{
    public partial class Member
    {
        [NotMapped]
        public string Name => string.Join(" ", new string[] { Forename, Surname }.Where(c => !string.IsNullOrWhiteSpace(c)));
        //Types
    }
}
