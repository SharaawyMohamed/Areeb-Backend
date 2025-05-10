using Areeb.Application.Common.Helpers;
using FluentValidation.Results;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Areeb.Application.Common.Responses
{
	public class Response(HttpStatusCode statusCode = HttpStatusCode.OK)
	{
		public HttpStatusCode StatusCode { get; set; } = statusCode;
		public bool IsSuccess { get; set; }
		public string Message { get; set; } = "";
		public object? Data { get; set; }
		public List<string> Errors { get; set; }


		#region Success Methods
		public static Task<Response> SuccessAsync(object data, string message)
		{
			Response response = new Response()
			{
				IsSuccess = true,
				Message = message,
				Data = data
			};

			return Task.FromResult(response);
		}
		public static Task<Response> SuccessAsync()
		{
			Response response = new Response()
			{
				IsSuccess = true
			};

			return Task.FromResult(response);
		}
		public static Task<Response> SuccessAsync(object data)
		{
			Response response = new Response()
			{
				IsSuccess = true,
				Data = data
			};

			return Task.FromResult(response);
		}
		public static Task<Response> SuccessAsync(string message)
		{
			Response response = new Response()
			{
				IsSuccess = true,
				Message = message
			};

			return Task.FromResult(response);
		}
		#endregion

		#region Failure Methods
		public static Task<Response> FailureAsync(object data, string message)
		{
			Response response = new Response()
			{
				StatusCode = HttpStatusCode.BadRequest,
				IsSuccess = false,
				Message = message,
				Data = data
			};

			return Task.FromResult(response);
		}
		public static Task<Response> FailureAsync(string message)
		{
			Response response = new Response()
			{
				StatusCode = HttpStatusCode.BadRequest,
				IsSuccess = false,
				Message = message
			};

			return Task.FromResult(response);
		}
		public static Task<Response> FailureAsync(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
		{
			Response response = new Response()
			{
				IsSuccess = false,
				Message = message,
				StatusCode = statusCode
			};

			return Task.FromResult(response);
		}
		public static Task<Response> ValidationFailureAsync(List<ValidationFailure> validationFailures, HttpStatusCode statusCode)
		{
			List<string> errors = validationFailures.GetErrorsList();

			var response = new Response()
			{
				IsSuccess = false,
				StatusCode = statusCode,
				Errors = errors
			};

			return Task.FromResult(response);
		}

		public static Task<Response> ValidationFailureAsync(List<IdentityError> identityErrors, HttpStatusCode httpStatusCode)
		{
			List<string> errors = identityErrors.GetErrorsList();

			var response = new Response()
			{
				IsSuccess = false,
				StatusCode = httpStatusCode,
				Errors = errors
			};

			return Task.FromResult(response);
		}

		#endregion
	}
}
