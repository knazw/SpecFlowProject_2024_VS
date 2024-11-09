//using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace SpecFlowProject_2024.Support
{
    internal class TestHelpers
    {
        public const string _jsonMediaType = "application/json";
        public const int _expectedMaxElapsedMilliseconds = 1000;
        public static readonly JsonSerializerOptions _jsonSerializerOptions = new() { PropertyNameCaseInsensitive = true };

        public static StringContent GetJsonStringContent<T>(T model)
    => new(JsonSerializer.Serialize(model), Encoding.UTF8, _jsonMediaType);

        public static void AssertCommonResponseParts(Stopwatch stopwatch,
    HttpResponseMessage response, System.Net.HttpStatusCode expectedStatusCode)
        {
            
            Assert.Equal(expectedStatusCode, response.StatusCode);
            Assert.True(stopwatch.ElapsedMilliseconds < _expectedMaxElapsedMilliseconds);
        }

        public static void AssertCommonResponseParts(Stopwatch stopwatch,
HttpResponseMessage response, int expectedStatusCode)
        {

            Assert.Equal(expectedStatusCode, (int)response.StatusCode);
            Assert.True(stopwatch.ElapsedMilliseconds < _expectedMaxElapsedMilliseconds);
        }

        public static async Task AssertResponseWithContentAsync<T>(HttpResponseMessage response,  T expectedContent)
        {
            Assert.Equal(_jsonMediaType, response.Content.Headers.ContentType?.MediaType);
            Assert.Equal(expectedContent, await JsonSerializer.DeserializeAsync<T?>(
                await response.Content.ReadAsStreamAsync(), _jsonSerializerOptions));
        }

        public static T? getDeserializedClass<T>(HttpResponseMessage response)
        {
            T? tClass = JsonSerializer.Deserialize<T>(response.Content.ReadAsStream());

            return tClass;
        }

        public static T? getDeserializedClass<T>(string jsonData)
        {
            T? tClass = JsonSerializer.Deserialize<T>(jsonData);

            return tClass;
        }
    }
}
