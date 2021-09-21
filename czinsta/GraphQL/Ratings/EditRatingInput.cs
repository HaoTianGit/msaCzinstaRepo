using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace czinsta.GraphQL.Ratings
{
    public record EditRatingInput(
        string RatingId,
        bool Grading
    );

}
