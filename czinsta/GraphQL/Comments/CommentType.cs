using czinsta.Data;
using czinsta.GraphQL.Accounts;
using czinsta.GraphQL.Posts;
using czinsta.Models;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Comments
{
    public class CommentType : ObjectType<Comment>
    {
        protected override void Configure(IObjectTypeDescriptor<Comment> descriptor)
        {
            descriptor.Field(s => s.Id).Type<NonNullType<IdType>>();
            descriptor.Field(s => s.Content).Type<NonNullType<StringType>>();
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

            descriptor.Field(p => p.Modified).Type<NonNullType<DateTimeType>>();
            descriptor.Field(p => p.Created).Type<NonNullType<DateTimeType>>();
        }

        private class Resolvers
        {
            public async Task<Post> GetPost(Comment comment, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Posts.FindAsync(new object[] { comment.PostId }, cancellationToken);
            }

            public async Task<Account> GetAccount(Comment comment, [ScopedService] AppDbContext context,
                CancellationToken cancellationToken)
            {
                return await context.Accounts.FindAsync(new object[] { comment.AccountId }, cancellationToken);
            }
        }


    }
}

