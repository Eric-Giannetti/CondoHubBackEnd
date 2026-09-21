using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace CondoHub.Domain.Entity;

public class UserData : Util.Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Cpf { get; set; }
    public long UserId { get; set; }

    [ForeignKey("UserId")] [JsonIgnore] public User User { get; set; }
}