using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CondoHub.Domain.Services;

public interface IUserContextService
{
    long UserId { get; set; }
    bool IsAdmin { get; set; }
}
