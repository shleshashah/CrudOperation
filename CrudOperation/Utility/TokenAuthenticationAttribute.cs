using CrudOperation.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperation.Utility
{
    public class TokenAuthenticationAttribute : TypeFilterAttribute
    {
        public TokenAuthenticationAttribute() : base(typeof(TokenAuthenticationFilter))
        {
        }

        public class TokenAuthenticationFilter : IAuthorizationFilter
        {
            private readonly AppDbContext adbContext;

            public TokenAuthenticationFilter(AppDbContext applicationDbContext)
            {
                adbContext = applicationDbContext;
            }
            public void OnAuthorization(AuthorizationFilterContext context)
            {
                // Extract token from request headers
                string token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

                // Validate the token
                if (!ValidateToken(token))
                {
                    // If token is invalid, return unauthorized response
                    context.Result = new UnauthorizedResult();
                }
            }

            private bool ValidateToken(string token)
            {
                return adbContext.Customers.Where(w => w.Token == token).Any();
            }
        }
    }
}
