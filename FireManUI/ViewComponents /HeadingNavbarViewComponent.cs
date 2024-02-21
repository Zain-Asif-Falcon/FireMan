using Application.Common.Utilities;
using Domain.Contracts.V1;
using FireManUI.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace FireManUI.ViewComponents
{
    public class HeadingNavbarViewComponent : ViewComponent
    {
        private readonly ILogger<HeadingNavbarViewComponent> _logger;
        private readonly IConfiguration _config;

        public HeadingNavbarViewComponent(ILogger<HeadingNavbarViewComponent> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
        }
 
    }
}
