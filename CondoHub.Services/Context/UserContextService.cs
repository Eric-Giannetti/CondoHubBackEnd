using CondoHub.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoHub.Services.Context;

public class UserContextService : IUserContextService
{
    public long UserId { get; set; }
    public bool IsAdmin { get; set; }
}
