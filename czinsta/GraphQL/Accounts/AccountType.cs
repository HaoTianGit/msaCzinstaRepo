using czinsta.Data;
using czinsta.GraphQL.Comments;
using czinsta.GraphQL.Posts;
using czinsta.GraphQL.Ratings;
using czinsta.Models;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Accounts
{
    public class AccountType : ObjectType<Account>
    {
        protected override void Configure(IObjectTypeDescriptor<Account> descriptor)
        {
            descriptor.Field(a => a.Id).Type<NonNullType<IdType>>();
            descriptor.Field(a => a.Name).Type<NonNullType<StringType>>();
            descriptor.Field(a => a.Bio).Type<NonNullType<StringType>>();
            descriptor.Field(a => a.Gender).Type<NonNullType<StringType>>();

            descriptor
                .Field(a => a.Posts)
                .ResolveWith<Resolvers>(r => r.GetPosts(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<PostType>>>>();

            descriptor
                 .Field(a => a.Comments)
                 .ResolveWith<Resolvers>(r => r.GetComments(default!, default!, default))
                 .UseDbContext<AppDbContext>()
                 .Type<NonNullType<ListType<NonNullType<CommentType>>>>();
            descriptor
                 .Field(a => a.Ratings)
                 .ResolveWith<Resolvers>(r => r.GetRatings(default!, default!, default))
                 .UseDbContext<AppDbContext>()
                 .Type<NonNullType<ListType<NonNullType<RatingType>>>>();

        }
        private class Resolvers
        {
            public async Task<IEnumerable<Post>> GetPosts(Account account, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Posts.Where(a => a.AccountId == account.Id).ToArrayAsync(cancellationToken);
            }

            public async Task<IEnumerable<Comment>> GetComments(Account account, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Comments.Where(a => a.AccountId == account.Id).ToArrayAsync(cancellationToken);
            }
            public async Task<IEnumerable<Rating>> GetRatings(Account account, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Ratings.Where(a => a.AccountId == account.Id).ToArrayAsync(cancellationToken);
            }
        }

    }

    
}


