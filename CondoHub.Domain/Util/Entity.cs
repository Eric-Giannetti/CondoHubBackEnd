using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CondoHub.Domain.Services;

namespace CondoHub.Domain.Util;

public class Entity
{
    private readonly IUserContextService _userContextService;
    
    
    [Key]
    public long Id { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    [JsonIgnore]
    public DateTime? DeletedAt { get; set; }
    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;
    [JsonIgnore]
    public long? CreatedBy { get; set; }
    [JsonIgnore]
    public long? UpdatedBy { get; set; }
    [JsonIgnore]
    public long? DeletedBy { get; set; }
}