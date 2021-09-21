using czinsta.Data;
using czinsta.Extensions;
using czinsta.Models;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Accounts
{
    [ExtendObjectType(name: "Mutation")]
    public class AccountMutations
    {
        [UseAppDbContext]
        public async Task<Account> AddAccountAsync(AddAccountInput input,
        [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var account = new Account
            {
                Name = input.Name,
                Bio = input.Bio,
                Gender = input.Gender,

            };

            context.Accounts.Add(account);
            await context.SaveChangesAsync(cancellationToken);

            return account;

        }

        [UseAppDbContext]
        public async Task<Account> EditAccountAsync(EditAccountInput input,
        [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var account = await context.Accounts.FindAsync(int.Parse(input.AccountId));

            account.Name = input.Name ?? account.Name;
            account.Gender = input.Gender ?? account.Gender;
            account.Bio = input.Bio ?? account.Bio;

            await context.SaveChangesAsync(cancellationToken);

            return account;
        }
    }
}
