using System;
using System.Collections.Generic;

namespace EmergenceResponse.Data;

public partial class Emergency
{
    public Guid Id { get; set; }

    public int TypeId { get; set; }

    public string Description { get; set; }

    public DateTime CreationDate { get; set; }

    public Guid LocationId { get; set; }

    public Guid CreatorId { get; set; }

    public int StatusId { get; set; }

    public Guid? ServiceProviderId { get; set; }

    public DateTime? ApprovalDate { get; set; }

    public byte[] AudioData { get; set; }

    public string AudioMimeType { get; set; }

    public virtual Member Creator { get; set; }

    public virtual Location Location { get; set; }

    public virtual ServiceProvider ServiceProvider { get; set; }
}
