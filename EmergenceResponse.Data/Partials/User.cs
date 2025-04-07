using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using wCyber.Helpers.Identity.Auth;

namespace EmergenceResponse.Data
{
    partial class User : ISysUser
    {
        [NotMapped]
        public string PictureUrl { get => ""; set => throw new NotImplementedException(); }

        [NotMapped]
        public int RoleId { get => 0; set { } }

       
       
        
    }
}
