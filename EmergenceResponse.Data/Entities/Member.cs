using System;
using System.Collections.Generic;

namespace EmergenceResponse.Data;

public partial class Member
{
    public Guid Id { get; set; }

    public string Forename { get; set; }

    public string Surname { get; set; }

    public string Phone { get; set; }

    public string Email { get; set; }

    public DateTime CreationDate { get; set; }

    public virtual ICollection<Emergency> Emergencies { get; } = new List<Emergency>();

    public virtual ICollection<User> Users { get; } = new List<User>();
}
