using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Accounts
{
    public record AddAccountInput(
        string AccountId,
        string Name,
        string Bio,
        string Gender);

}

