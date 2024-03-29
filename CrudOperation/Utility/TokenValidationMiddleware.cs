using CrudOperation.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudOperation.Utility
{
    public class TokenValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppDbContext adbContext;

        public TokenValidationMiddleware(RequestDelegate next, AppDbContext applicationDbContext)
        {
            _next = next;
            adbContext = applicationDbContext;
        }

        public async Task Invoke(HttpContext context)
        {
            // Extract token from request headers
            string token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            // Validate the token
            if (ValidateToken(token))
            {
                // If token is valid, proceed with the request
                await _next(context);
            }
            else
            {
                // If token is invalid, return unauthorized response
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Unauthorized");
            }
        }

        private bool ValidateToken(string token)
        {
            return adbContext.Customers.Where(w => w.Token == token).Any();
        }
    }
}
