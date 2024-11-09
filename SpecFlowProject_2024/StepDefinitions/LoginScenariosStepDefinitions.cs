using log4net;
using SpecFlowProject_2024.DataAccess;
using SpecFlowProject_2024.Model;
using SpecFlowProject_2024.Support;
using System;
using System.Diagnostics;
using System.Net.Http;
using TechTalk.SpecFlow;
using Xunit;

namespace SpecFlowProject_2024.StepDefinitions
{
    [Binding]
    public class LoginScenariosStepDefinitions
    {
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Stopwatch stopwatch;
        private readonly ScenarioContext _scenarioContext;

        public LoginScenariosStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Then(@"Response message contains Unathorized")]
        public void ThenResponseMessageContainsUnathorized()
        {
            string response = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;

            Assert.Equal("Unauthorized", response);
            
        }

        [When(@"""([^""]*)"" starts to login with credentials from file ""([^""]*)""")]
        public void WhenStartsToLoginWithCredentialsFromFile(string username, string path)
        {
            DataAccess.JsonReader jsonReader = new DataAccess.JsonReader();
            var userX = jsonReader.readFileGeneric<UserX>(path);

            stopwatch = Stopwatch.StartNew();
            _scenarioContext[ScenarioContextHelpers.response] = HttpClientHelpers.GetHttpClient().PostAsync(ApiEndpoints.pathLogin, TestHelpers.GetJsonStringContent(userX)).GetAwaiter().GetResult();
        }

        [Then(@"Response message ""([^""]*)""")]
        public void ThenResponseMessage(string responseMessage)
        {            
            string response = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;

            Assert.Equal(responseMessage, response);
        }

        [When(@"""([^""]*)"" starts to login with no credentials")]
        public void WhenStartsToLoginWithNoCredentials(string username)
        {
            // todo
            _scenarioContext[ScenarioContextHelpers.response] = HttpClientHelpers.GetHttpClient().PostAsync(ApiEndpoints.pathLogin, null).GetAwaiter().GetResult();
        }
    }
}
