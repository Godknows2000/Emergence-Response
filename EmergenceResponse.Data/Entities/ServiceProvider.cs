using System;
using System.Collections.Generic;

namespace EmergenceResponse.Data;

public partial class ServiceProvider
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public Guid LocationId { get; set; }

    public DateTime CreationDate { get; set; }

    public int StatusId { get; set; }

    public int TypeId { get; set; }

    public string Email { get; set; }

    public virtual ICollection<Emergency> Emergencies { get; } = new List<Emergency>();

    public virtual Location Location { get; set; }

    public virtual ICollection<User> Users { get; } = new List<User>();
}
