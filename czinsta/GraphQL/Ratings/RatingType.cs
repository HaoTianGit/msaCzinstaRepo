using czinsta.Data;
using czinsta.GraphQL.Accounts;
using czinsta.GraphQL.Comments;
using czinsta.GraphQL.Posts;
using czinsta.Models;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Ratings
{
    public class RatingType : ObjectType<Rating>
    {
        protected override void Configure(IObjectTypeDescriptor<Rating> descriptor)
        {
            descriptor.Field(s => s.ID).Type<NonNullType<IdType>>();
            descriptor.Field(s => s.Grading).Type<NonNullType<BooleanType>>();
            descriptor
                .Field(s => s.Post)
                .ResolveWith<Resolvers>(r => r.GetPost(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<PostType>>();

            descriptor
                .Field(s => s.Account)
                .ResolveWith<Resolvers>(r => r.GetAccount(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<AccountType>>();

        }
        private class Resolvers
        {
            public async Task<Post> GetPost(Rating rating, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Posts.FindAsync(new object[] { rating.PostId }, cancellationToken);
            }

            public async Task<Account> GetAccount(Rating rating, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Accounts.FindAsync(new object[] { rating.AccountId }, cancellationToken);
            }
        }
    }
}



