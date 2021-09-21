using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Posts
{
    public record EditPostInput(
        string PostId,
        string PostImage,
        string Caption
    );

}


