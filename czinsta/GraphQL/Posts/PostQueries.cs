using czinsta.Data;
using czinsta.Extensions;
using czinsta.Models;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Posts
{
    [ExtendObjectType(name: "Query")]
    public class PostQueries
    {
        [UseAppDbContext]
        [UsePaging]
        public IQueryable<Post> GetPosts([ScopedService] AppDbContext context)
        {
            return context.Posts;
        }

        [UseAppDbContext]
        public Post GetPost(int id, [ScopedService] AppDbContext context)
        {
            return context.Posts.Find(id);
        }
    }
}
