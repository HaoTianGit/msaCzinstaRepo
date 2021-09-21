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

namespace czinsta.GraphQL.Posts
{
    [ExtendObjectType(name: "Mutation")]
    public class PostMutations
    {
        [UseAppDbContext]
        public async Task<Post> AddPostAsync(AddPostInput input,
            [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var post = new Post
            {
                Caption = input.Caption,
                PostImage = input.PostImage,
                AccountId = int.Parse(input.AccountId),
                Modified = DateTime.Now,
                Created = DateTime.Now,
            };
            context.Posts.Add(post);

            await context.SaveChangesAsync(cancellationToken);

            return post;
        }

        [UseAppDbContext]
        public async Task<Post> EditPostAsync(EditPostInput input,
            [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var post = await context.Posts.FindAsync(int.Parse(input.PostId));

            post.PostImage = input.PostImage ?? post.PostImage;
            post.Caption = input.Caption ?? post.Caption;
            post.Modified = DateTime.Now;

            await context.SaveChangesAsync(cancellationToken);

            return post;
        }
    }


}
