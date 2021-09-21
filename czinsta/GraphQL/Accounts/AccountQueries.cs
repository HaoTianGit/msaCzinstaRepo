
using czinsta.Data;
using czinsta.Extensions;
using czinsta.Models;
using HotChocolate;
using HotChocolate.Types;

using System.Linq;

namespace czinsta.GraphQL.Accounts
{
    [ExtendObjectType(name: "Query")]
    public class AccountQueries
    {
        [UseAppDbContext]
        [UsePaging]
        public IQueryable<Account> GetAccounts([ScopedService] AppDbContext context)
        {
            return context.Accounts;

        }

        [UseAppDbContext]
        public Account GetAccount(int id, [ScopedService] AppDbContext context)
        {
            return context.Accounts.Find(id);
        }
    }
}



