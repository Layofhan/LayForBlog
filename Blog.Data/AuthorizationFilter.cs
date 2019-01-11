using Blog.Data.Interface;
using Blog.Data.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blog.Data
{
    public class AuthorizationFilter : ActionFilterAttribute, IAuthorizationFilter
    {
        public string ActionName { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string s = ActionName;
        }
    }
}
