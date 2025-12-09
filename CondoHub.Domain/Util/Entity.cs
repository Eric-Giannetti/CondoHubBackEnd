using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CondoHub.Domain.Util;

public class Entity
{
    [Key]
    public long Id { get; set; }
    [JsonIgnore]
    public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; }
    [JsonIgnore]
    public DateTime? DeletedAt { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
    [JsonIgnore]
    public long? CreatedBy { get; set; }
    [JsonIgnore]
    public string? UpdatedBy { get; set; }
    [JsonIgnore]
    public string? DeletedBy { get; set; }
}