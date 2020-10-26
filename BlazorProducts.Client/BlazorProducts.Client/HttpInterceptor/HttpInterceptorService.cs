using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Toolbelt.Blazor;

namespace BlazorProducts.Client.HttpInterceptor
{
	public class HttpInterceptorService
	{
		private readonly HttpClientInterceptor _interceptor;
		private readonly NavigationManager _navManager;

		public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navManager)
		{
			_interceptor = interceptor;
			_navManager = navManager;
		}

		public void RegisterEvent() => _interceptor.AfterSend += HandleResponse;
		public void DisposeEvent() => _interceptor.AfterSend -= HandleResponse;

		private void HandleResponse(object sender, HttpClientInterceptorEventArgs e)
		{
			if(e.Response == null)
			{
				_navManager.NavigateTo("/error");
				throw new HttpResponseException("Server not available.");
			}

			var message = "";

			if(!e.Response.IsSuccessStatusCode)
			{
				switch (e.Response.StatusCode)
				{
					case HttpStatusCode.NotFound:
						_navManager.NavigateTo("/404");
						message = "Resource not found.";
						break;
					case HttpStatusCode.Unauthorized:
						_navManager.NavigateTo("/unauthorized");
						message = "Unauthorized access";
						break;
					default:
						_navManager.NavigateTo("/error");
						message = "Something went wrong. Please contact the administrator.";
						break;
				}

				throw new HttpResponseException(message);
			}
		}
	}
}
