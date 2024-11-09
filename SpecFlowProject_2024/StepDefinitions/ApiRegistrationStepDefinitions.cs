using AngleSharp.Common;
using Gherkin;
using Gherkin.CucumberMessages.Types;
using LivingDoc.Dtos;
using log4net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using SpecFlowProject_2024.DataAccess;
using SpecFlowProject_2024.Model;
using SpecFlowProject_2024.Support;
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Mime;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.CommonModels;
using Xunit;

namespace SpecFlowProject_2024.StepDefinitions
{

    [Binding]
    public class ApiRegistrationStepDefinitions : IDisposable
    {
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private readonly ScenarioContext _scenarioContext;
        private Stopwatch stopwatch;



        public ApiRegistrationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [BeforeScenario]
        public void SetupTestUsers()
        {
            logger.Info(String.Format("ApiRegistrationStepDefinitions SetupTestUsers BeforeScenario {0} at {1}", "", DateTime.Now));
            HttpClientHelpers.GetHttpClient().PostAsync(ApiEndpoints.pathDataSeed, null).GetAwaiter().GetResult();
            logger.Info(String.Format("end ApiRegistrationStepDefinitions SetupTestUsers BeforeScenario {0} at {1}", "", DateTime.Now));
        }

        [AfterScenario]
        public void CleanupTest()
        {
            logger.Info(String.Format("ApiRegistrationStepDefinitions CleanupTest start after scenario {0} at {1}", "", DateTime.Now));
            HttpClientHelpers.GetHttpClient().PostAsync(ApiEndpoints.pathDataSeed, null);
            logger.Info(String.Format("ApiRegistrationStepDefinitions CleanupTest end after scenario {0} at {1}", "", DateTime.Now));
        }

        void IDisposable.Dispose()
        {           
            logger.Info(String.Format("empty dispose api registration {0} at {1}", "", DateTime.Now));
        }


        [Given(@"Following user ""([^""]*)""")]
        public void GivenFollowingUser(string userForChecks)
        {
            DataAccess.JsonReader jsonReader = new DataAccess.JsonReader();
            jsonReader.readFile("usersAll.json");
            var userXItems = jsonReader.UserXList.Where(x => x.username.Equals(userForChecks));            
            _scenarioContext[ScenarioContextHelpers.defaultUser] = userXItems.FirstOrDefault();

        }

        [Given(@"""([^""]*)"" is created")]
        public void GivenIsCreated(string userForChecks)
        {
            stopwatch = Stopwatch.StartNew();
            _scenarioContext[ScenarioContextHelpers.response] = HttpClientHelpers.GetHttpClientNoCookies().PostAsync(ApiEndpoints.pathUsers, TestHelpers.GetJsonStringContent(_scenarioContext[ScenarioContextHelpers.defaultUser])).GetAwaiter().GetResult();
        }

        [Given(@"(.*) response code is received")]
        public void GivenResponseCodeIsReceived(int expectedStatusCode)
        {            
            checkResponseCode(expectedStatusCode);
        }

        [Given(@"Json in response body matches createdUser\.json")]
        public void GivenJsonInResponseBodyMatchesCreatedUser_Json()
        {
            string jsonData = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "jsons\\createdUser.json");
            
            checkJsonSchema(jsonData, path);
        }


        [Given(@"Response object is properly validated as an user object of an user ""([^""]*)""")]
        public async Task GivenResponseObjectIsProperlyValidatedAsAnUserObjectOfAnUserAsync(string username)
        {
            var response = (HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response];
            var expectedContent = _scenarioContext[ScenarioContextHelpers.defaultUser];

            var jsonData = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;

            string jsonUserCreated = JsonHelpers.getJson(jsonData, "user");

            logger.Info(String.Format("user created - response {0} at {1}", response, DateTime.Now));
            logger.Info(String.Format("user created - dictionary {0} at {1}", response.Content.ToDictionary().ToString(), DateTime.Now));

            var value = TestHelpers.getDeserializedClass<UserCreated>(jsonUserCreated);
  

            Assert.Equal(((UserX)_scenarioContext[ScenarioContextHelpers.defaultUser]).firstName, value?.firstName);
            Assert.Equal(((UserX)_scenarioContext[ScenarioContextHelpers.defaultUser]).lastName, value?.lastName);
            Assert.Equal(((UserX)_scenarioContext[ScenarioContextHelpers.defaultUser]).username, value?.username);

            Assert.True(value?.password != null);
            Assert.NotNull(value?.uuid);

            if (!_scenarioContext.ContainsKey(ScenarioContextHelpers.usersIdMap))
            {
                _scenarioContext[ScenarioContextHelpers.usersIdMap] = new Dictionary<string, UserCreated>();
            }
            ((Dictionary<string, UserCreated>)_scenarioContext[ScenarioContextHelpers.usersIdMap]).Add(value?.username!, value!);
        }

        [When(@"""([^""]*)"" starts to login with credentials")]
        public void WhenStartsToLoginWithCredentials(string username)
        {
            stopwatch = Stopwatch.StartNew();
            _scenarioContext[ScenarioContextHelpers.response] = HttpClientHelpers.GetHttpClient().PostAsync(ApiEndpoints.pathLogin, TestHelpers.GetJsonStringContent((UserX)_scenarioContext[ScenarioContextHelpers.defaultUser])).GetAwaiter().GetResult();

        }

        [Then(@"(.*) response code is received")]
        public void ThenResponseCodeIsReceived(int responseCode)
        {
            checkResponseCode(responseCode);
        }

        [Then(@"Correct user object is received")]
        public void ThenCorrectUserObjectIsReceived()
        {
            string jsonData = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;
            var path = Path.Combine(Directory.GetCurrentDirectory(), "jsons\\loggedUser.json");

            checkJsonSchema(jsonData, path);

            string jsonUserLogged = JsonHelpers.getJson(jsonData, "user");
            var userLogged = TestHelpers.getDeserializedClass<UserCreated>(jsonUserLogged);

            Assert.Equal(((UserX)_scenarioContext[ScenarioContextHelpers.defaultUser]).firstName, userLogged?.firstName);
            Assert.Equal(((UserX)_scenarioContext[ScenarioContextHelpers.defaultUser]).lastName, userLogged?.lastName);
            Assert.Equal(((UserX)_scenarioContext[ScenarioContextHelpers.defaultUser]).username, userLogged?.username);

            Assert.True(userLogged?.password != null);
            Assert.NotNull(userLogged?.uuid);

        }

        [Then(@"Cookie can be obtained from response header")]
        public void ThenCookieCanBeObtainedFromResponseHeader()
        {
            CommonStepDefinitions.ObtainCookieFromHeaders(_scenarioContext);
        }


        [When(@"""([^""]*)"" is created with not all data provided in request based on ""([^""]*)""")]
        public void WhenIsCreatedWithNotAllDataProvidedInRequestBasedOn(string username, string path)
        {
            DataAccess.JsonReader jsonReader = new DataAccess.JsonReader();
            var userX = jsonReader.readFileGeneric<UserX>(path);

            _scenarioContext[ScenarioContextHelpers.response] = HttpClientHelpers.GetHttpClientNoCookies().PostAsync(ApiEndpoints.pathUsers, TestHelpers.GetJsonStringContent(userX)).GetAwaiter().GetResult();

        }

        [Then(@"Response object is validated with a file ""([^""]*)""")]
        public void ThenResponseObjectIsValidatedWithAFile(string p0)
        {            
            string jsonData = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;         
            var path = Path.Combine(Directory.GetCurrentDirectory(), p0);

            checkJsonSchema(jsonData, path);
        }

        [Then(@"Get request for users list is sent")]
        public void ThenGetRequestForUsersListIsSent()
        {

            stopwatch = Stopwatch.StartNew();
            string cookieValue =  (string)_scenarioContext[ScenarioContextHelpers.cookieValue];
            _scenarioContext[ScenarioContextHelpers.response] =  HttpClientHelpers.GetHttpClientWithCookiesCookies(cookieValue).GetAsync(ApiEndpoints.pathUsers).GetAwaiter().GetResult();
        }

        [Then(@"Response contains ""([^""]*)""")]
        public void ThenResponseContains(string username)
        {
            string jsonData = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;
            string jsonUsers = JsonHelpers.getJson(jsonData, "results");
            var usersList = TestHelpers.getDeserializedClass<IList<UserCreated>>(jsonUsers);
            var user = usersList?.Where(x => x.username.Equals(username)).FirstOrDefault();

            Assert.NotNull(user);
        }

        [Then(@"Response contains no username ""([^""]*)"" for data from ""([^""]*)""")]
        public void ThenResponseContainsNoUsernameForDataFrom(string username, string path)
        {
            DataAccess.JsonReader jsonReader = new DataAccess.JsonReader();
            var userX = jsonReader.readFileGeneric<UserX>(path);

            Assert.NotNull(userX);
            Assert.NotNull(userX.lastName); 
            Assert.NotNull(userX.firstName);


            string jsonData = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;
            string jsonUsers = JsonHelpers.getJson(jsonData, "results");
            var usersList = TestHelpers.getDeserializedClass<IList<UserCreated>>(jsonUsers);
            var user = usersList?.Where(x => x.firstName.Equals(userX.firstName) && x.lastName.Equals(userX.lastName)).FirstOrDefault();

            Assert.NotNull(user);
            Assert.Null(user.username);
        }

        [Then(@"Response does not contain object for ""([^""]*)""")]
        public void ThenResponseDoesNotContainObjectFor(string username)
        {
            string jsonData = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;

            string jsonUsers = JsonHelpers.getJson(jsonData, "results");
            logger.Info(String.Format("users GET - all {0} at {1}", jsonData, DateTime.Now));
            logger.Info(String.Format("users GET - {0} at {1}", jsonUsers, DateTime.Now));
            var usersList = TestHelpers.getDeserializedClass<IList<UserCreated>>(jsonUsers);

            var user = usersList?.Where(x => x.username.Equals(username)).FirstOrDefault();

            Assert.Null(user);
        }

        private void checkResponseCode(int expectedStatusCode)
        {
            TestHelpers.AssertCommonResponseParts(stopwatch, ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]), expectedStatusCode);
        }


        private void checkJsonSchema(string jsonData, string path)
        {
            string schemaData = File.ReadAllText(path);

            JSchema schema = JSchema.Parse(schemaData);
            JObject json = JObject.Parse(jsonData);

            Assert.True(json.IsValid(schema, out IList<string> errors));
        }
    }
}
