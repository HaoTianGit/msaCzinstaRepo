using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Ratings
{
    public record AddRatingInput(
        bool Grading,
        string PostId,
        string AccountId

        );

}
