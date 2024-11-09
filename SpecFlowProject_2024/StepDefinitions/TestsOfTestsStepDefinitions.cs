using AngleSharp.Common;
using log4net;
using SpecFlowProject_2024.DataAccess;
using SpecFlowProject_2024.Support;
using System;
using System.Diagnostics;
using TechTalk.SpecFlow;

namespace SpecFlowProject_2024.StepDefinitions
{
    [Binding]
    public class TestsOfTestsStepDefinitions : IDisposable
    {
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ScenarioContext _scenarioContext;
        private Stopwatch stopwatch;



        public TestsOfTestsStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void SetupTestUsers()
        {
        }

        [AfterScenario]
        public void CleanupTest()
        {
        }

        void IDisposable.Dispose()
        {
        }

        [Then(@"Seed of the database is performed")]
        public void ThenSeedOfTheDatabaseIsPerformedAsync()
        {
            var requestMessage = HttpClientHelpers.GetHttpClient().PostAsync(ApiEndpoints.pathDataSeed, null).GetAwaiter().GetResult().RequestMessage;
        }

        [Given(@"Seed of the database is performed")]
        public void GivenSeedOfTheDatabaseIsPerformedAsync()
        {
            var requestMessage = HttpClientHelpers.GetHttpClient().PostAsync(ApiEndpoints.pathDataSeed, null).GetAwaiter().GetResult().RequestMessage;
        }
    }
}
