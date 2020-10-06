using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using IdentityServer4.Models;
using IdentityServer4.Services;

namespace GameRecordKeeper.Pages.Home
{
    public class ErrorModel : PageModel
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IHostingEnvironment _environment;

        public ErrorModel(IIdentityServerInteractionService interaction, IHostingEnvironment environment)
        {
            _interaction = interaction;
            _environment = environment;
        }

        public ErrorMessage Error { get; set; }

        public async void OnGet(string errorId)
        {
            var message = await _interaction.GetErrorContextAsync(errorId);
            if (message != null)
            {
                Error = message;

                if (!_environment.IsDevelopment())
                {
                    // only show in development
                    message.ErrorDescription = null;
                }
            }
        }
    }
}
