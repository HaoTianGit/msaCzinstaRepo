using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Comments
{
    public record AddCommentInput(
        string Content,
        string PostId,
        string AccountId);
}


