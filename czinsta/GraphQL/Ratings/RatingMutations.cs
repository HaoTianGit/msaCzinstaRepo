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

namespace czinsta.GraphQL.Ratings
{
    [ExtendObjectType(name: "Mutation")]
    public class RatingMutations
    {
        [UseAppDbContext]
        public async Task<Rating> AddRatingAsync(AddRatingInput input,
            [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var rating = new Rating
            {
                Grading = input.Grading,
                PostId = int.Parse(input.PostId),
                AccountId = int.Parse(input.AccountId),
            };
            context.Ratings.Add(rating);

            await context.SaveChangesAsync(cancellationToken);

            return rating;

        }
        [UseAppDbContext]
        public async Task<Rating> EditRatingAsync(EditRatingInput input,
            [ScopedService] AppDbContext context, CancellationToken cancellationToken)
        {
            var rating = await context.Ratings.FindAsync(int.Parse(input.RatingId));
            if(input.Grading == rating.Grading)
            {
                rating.Grading = rating.Grading;
            }
            else
            {
                rating.Grading = input.Grading;
            }

            await context.SaveChangesAsync(cancellationToken);

            return rating;
        }
    }
}






