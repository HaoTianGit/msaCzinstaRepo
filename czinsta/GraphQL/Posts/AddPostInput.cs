using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Posts
{
    public record AddPostInput(
        string Caption,
        string AccountId,
        string PostImage
        );

}

