using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Net;
using Toolbelt.Blazor;

namespace BlazorProducts.Client.HttpInterceptor
{
    public class HttpInterceptorService
	{
		private readonly HttpClientInterceptor _interceptor;
		private readonly NavigationManager _navManager;
		private readonly IToastService _toastService;

		public HttpInterceptorService(HttpClientInterceptor interceptor, 
			NavigationManager navManager, IToastService toastService)
		{
			_interceptor = interceptor;
			_navManager = navManager;
			_toastService = toastService;
		}

		public void RegisterEvent() => _interceptor.AfterSend += HandleResponse;
		public void DisposeEvent() => _interceptor.AfterSend -= HandleResponse;

		private void HandleResponse(object sender, HttpClientInterceptorEventArgs e)
		{
			if(e.Response is null)
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
					case HttpStatusCode.BadRequest:
						message = "Invalid request. Please try again.";
                        _toastService.ShowError(message);
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
