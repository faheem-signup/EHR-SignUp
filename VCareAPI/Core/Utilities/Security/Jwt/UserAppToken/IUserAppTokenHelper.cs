using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Security.Jwt.UserAppToken
{
    public interface IUserAppTokenHelper
    {
        TAccessToken CreateUserAppToken<TAccessToken>(UserAppCore user)
        where TAccessToken : IAccessToken, new();
    }
}
