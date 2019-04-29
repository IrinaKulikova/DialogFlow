using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDialog.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace WebDialog.Services
{
    public static class AntiforgeryCookieService
    {
        public static IServiceCollection AddAntiforgeryConfig(this IServiceCollection services){
            
             services.AddAntiforgery(options => 
                {
                    // Set Cookie properties using CookieBuilder properties†.
                    options.FormFieldName = "AntiforgeryCookie";
                    options.HeaderName = "AntiforgeryCookie";
                    options.SuppressXFrameOptionsHeader = false;
                });
            return services;
        }
    }
}