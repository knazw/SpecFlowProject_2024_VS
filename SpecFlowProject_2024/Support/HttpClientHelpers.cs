using NUnit.Framework.Internal.Execution;
using SpecFlowProject_2024.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.Support
{
    internal class HttpClientHelpers
    {
        private static readonly string baseAddress = "http://localhost:3001";

        public static readonly HttpClient httpClient = new(new LoggingHandler(new HttpClientHandler()))
        {
            BaseAddress = new Uri(baseAddress)
        };

        public static HttpClient GetHttpClient()
        {
            return new(new LoggingHandler(new HttpClientHandler()))
            {
                BaseAddress = new Uri(baseAddress)
            };
        }

        public static HttpClient GetHttpClientNoCookies()
        {
            var handler = new HttpClientHandler
            {
                UseCookies = false
            };
            var httpClient = new HttpClient(new LoggingHandler(handler))
            {
                BaseAddress = new Uri(baseAddress) 
            };
            

            return httpClient;
        }

        public static HttpClient GetHttpClientWithCookiesCookies(string cookieValue)
        {
            var cookieContainer = new CookieContainer();
            cookieContainer.SetCookies(new Uri(baseAddress), cookieValue);            
            var handler = new HttpClientHandler
            {
                CookieContainer = cookieContainer
            };
            var httpClient = new HttpClient(new LoggingHandler(handler))
            {
                BaseAddress = new Uri(baseAddress) // Use your actual API base address here
            };


            return httpClient;
        }

    }
}
