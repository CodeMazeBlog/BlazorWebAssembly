﻿using Blazored.LocalStorage;
using BlazorProducts.Client.Features;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BlazorProducts.Client.AuthProviders
{
	public class AuthStateProvider : AuthenticationStateProvider
	{
		private readonly HttpClient _httpClient;
		private readonly ILocalStorageService _localStorage;
		private readonly AuthenticationState _anonymous;

		public AuthStateProvider(HttpClient httpClient, ILocalStorageService localStorage)
		{
			_httpClient = httpClient;
			_localStorage = localStorage;
			_anonymous = new AuthenticationState(
				new ClaimsPrincipal(new ClaimsIdentity()));
		}

		public override async Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var token = await _localStorage.GetItemAsync<string>("authToken");
			if (string.IsNullOrWhiteSpace(token))
				return _anonymous;

			_httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("bearer", token);

			return new AuthenticationState(new ClaimsPrincipal(
				new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
		}

		public void NotifyUserAuthentication(string token)
		{
			var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(
				JwtParser.ParseClaimsFromJwt(token), "jwtAuthType"));

			var authState = Task.FromResult(new AuthenticationState(authenticatedUser));

			NotifyAuthenticationStateChanged(authState);
		}

		public void NotifyUserLogout()
		{
			var authState = Task.FromResult(_anonymous);

			NotifyAuthenticationStateChanged(authState);
		}
	}
}
