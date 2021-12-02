﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AutoShop.Api.Config
{
	public static class JwtConfig
	{
		public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddSingleton<IApplicationServiceLogin, ApplicationServiceLogin>();

			var chave = Encoding.ASCII.GetBytes(configuration.GetSection("JWT:Secret").Value);

			services.AddAuthentication(p =>
			{
				p.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				p.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(p =>
			{
				p.RequireHttpsMetadata = false;
				p.SaveToken = true;
				p.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(chave),
					ValidateIssuer = true,
					ValidIssuer = configuration.GetSection("JWT:Issuer").Value,
					ValidateAudience = true,
					ValidAudience = configuration.GetSection("JWT:Audience").Value,
					ValidateLifetime = true
				};
			});
		}

		public static void UseJwtConfiguration(this IApplicationBuilder app)
		{
			app.UseAuthentication();
			app.UseAuthorization();
		}
	}
}
