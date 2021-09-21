using czinsta.Data;
using czinsta.GraphQL.Accounts;
using czinsta.GraphQL.Comments;
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

namespace czinsta.GraphQL.Posts
{
    public class PostType : ObjectType<Post>
    {
        protected override void Configure(IObjectTypeDescriptor<Post> descriptor)
        {
            descriptor.Field(p => p.Id).Type<NonNullType<IdType>>();
            descriptor.Field(P => P.PostImage).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.Caption).Type<NonNullType<StringType>>();
            descriptor.Field(p => p.DislikeNumber).Type<NonNullType<IntType>>();
            descriptor.Field(p => p.LikeNumber).Type<NonNullType<IntType>>();

            descriptor
                .Field(p => p.Account)
                .ResolveWith<Resolvers>(r => r.GetAccount(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<AccountType>>();

            descriptor
                .Field(p => p.Comments)
                .ResolveWith<Resolvers>(r => r.GetComments(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<CommentType>>>>();

            descriptor
                .Field(p => p.Ratings)
                .ResolveWith<Resolvers>(r => r.GetRatings(default!, default!, default))
                .UseDbContext<AppDbContext>()
                .Type<NonNullType<ListType<NonNullType<RatingType>>>>();


            descriptor.Field(p => p.Modified).Type<NonNullType<DateTimeType>>();
            descriptor.Field(p => p.Created).Type<NonNullType<DateTimeType>>();

        }

        private class Resolvers
        {
            public async Task<Account> GetAccount(Post post, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Accounts.FindAsync(new object[] { post.AccountId }, cancellationToken);
            }

            public async Task<IEnumerable<Comment>> GetComments(Post post, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Comments.Where(c => c.PostId == post.Id).ToArrayAsync(cancellationToken);
            }

            public async Task<IEnumerable<Rating>> GetRatings(Post post, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Ratings.Where(c => c.PostId == post.Id).ToArrayAsync(cancellationToken);
            }


        }

    }
}



