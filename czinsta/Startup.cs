using czinsta.Data;
using czinsta.GraphQL.Accounts;
using czinsta.GraphQL.Comments;
using czinsta.GraphQL.Posts;
using czinsta.GraphQL.Ratings;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace czinsta
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddPooledDbContextFactory<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services
                .AddGraphQLServer()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<PostQueries>()
                    .AddTypeExtension<AccountQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<AccountMutations>()
                    .AddTypeExtension<PostMutations>()
                    .AddTypeExtension<CommentMutations>()
                    .AddTypeExtension<RatingMutations>()
                .AddType<AccountType>()
                .AddType<PostType>()
                .AddType<CommentType>()
                .AddType<RatingType>();

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
        
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }

    }
}
