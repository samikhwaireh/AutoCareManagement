
using System.Text.Json.Serialization;

namespace AutoCare.Core.Models.Entity;

public abstract class BaseEntity<TId>
{
    public TId Id { get; set; }
    [JsonIgnore]
    public DateTime AddedTime { get; set; } = DateTime.UtcNow;
    [JsonIgnore]

    public string AddedBy { get; set; } = "Global";
}

