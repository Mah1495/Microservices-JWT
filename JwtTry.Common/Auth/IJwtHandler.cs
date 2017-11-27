using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JwtTry.Common
{
    public interface IJwtHandler
    {
        JsonWebToken Create(Guid userId);
    }
}
