using Force.DeepCloner;
using log4net;
using SpecFlowProject_2024.DataAccess;
using SpecFlowProject_2024.Model;
using SpecFlowProject_2024.Support;
using System;
using System.Data.Common;
using System.Diagnostics;
using System.Transactions;
using TechTalk.SpecFlow;
using Xunit;

namespace SpecFlowProject_2024.StepDefinitions
{
    [Binding]
    public class TransactionScenariosStepDefinitions
    {
        private static ILog logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Stopwatch stopwatch;
        private readonly ScenarioContext _scenarioContext;

        public TransactionScenariosStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"""([^""]*)"" starts to login with credentials")]
        public void GivenStartsToLoginWithCredentials(string username)
        {
            stopwatch = Stopwatch.StartNew();
            _scenarioContext[ScenarioContextHelpers.response] = HttpClientHelpers.GetHttpClient().PostAsync(ApiEndpoints.pathLogin, TestHelpers.GetJsonStringContent((UserX)_scenarioContext[ScenarioContextHelpers.defaultUser])).GetAwaiter().GetResult();
        }

        [Given(@"Cookie can be obtained from response header")]
        public void GivenCookieCanBeObtainedFromResponseHeader()
        {
            var cookie = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Headers.GetValues("Set-Cookie");
            string[] cookieArray = cookie.FirstOrDefault()?.Split(";")!;

            _scenarioContext[ScenarioContextHelpers.cookieValue] = cookieArray[0];
        }

        [When(@"""([^""]*)"" creates a ""([^""]*)"" transaction from user ""([^""]*)"" to ""([^""]*)"" with (.*) and description ""([^""]*)""")]
        public void WhenCreatesATransactionFromUserToWithAndDescription(string usernameArg, string transactionType, string username, string username1, int amount, string description)
        {
            CreatesATransactionFromUserToWithAndDescription(usernameArg, transactionType, username, username1, amount, description);            
        }

        [Then(@"Transaction object is obtained from response")]
        public void ThenTransactionObjectIsObtainedFromResponse()
        {
            TransactionObjectIsObtainedFromResponse();
        }

        [Then(@"Correct transaction data are present in this object: ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", (.*), ""([^""]*)"", ""([^""]*)"" and ""([^""]*)""")]
        public void ThenCorrectTransactionDataArePresentInThisObjectAnd(string username, string username1, string transaction, int amount, string description, string status, string requestStatus)
        {
            CorrectTransactionDataArePresentInThisObjectAnd(username, username1, transaction, amount, description, status, requestStatus);
        }

        [Then(@"It is possible to obtain transactions list by get transaction request")]
        public void ThenItIsPossibleToObtainTransactionsListByGetTransactionRequest()
        {
            ItIsPossibleToObtainTransactionsListByGetTransactionRequest();
        }

        [Then(@"It is possible to obtain transaction from transactions list")]
        public void ThenItIsPossibleToObtainTransactionFromTransactionsList()
        {
            ItIsPossibleToObtainTransactionFromTransactionsList();
        }

        [Then(@"It is possible to compare obtained transaction with data: ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", (.*), ""([^""]*)"", ""([^""]*)"" and ""([^""]*)""")]
        public void ThenItIsPossibleToCompareObtainedTransactionWithDataAnd(string username, string username1, string transaction, int amount, string description, string status, string requestStatus)
        {
            ItIsPossibleToCompareObtainedTransactionWithDataAnd(username, username1, transaction, amount, description, status, requestStatus);
        }

        [Given(@"""([^""]*)"" creates a ""([^""]*)"" transaction from user ""([^""]*)"" to ""([^""]*)"" with (.*) and description ""([^""]*)""")]
        public void GivenCreatesATransactionFromUserToWithAndDescription(string usernameArg, string transactionType, string username, string username1, int amount, string description)
        {
            CreatesATransactionFromUserToWithAndDescription(usernameArg, transactionType, username, username1, amount, description);
        }

        [Given(@"Transaction object is obtained from response")]
        public void GivenTransactionObjectIsObtainedFromResponse()
        {
            TransactionObjectIsObtainedFromResponse();
        }

        [Given(@"Correct transaction data are present in this object: ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", (.*), ""([^""]*)"", ""([^""]*)"" and ""([^""]*)""")]
        public void GivenCorrectTransactionDataArePresentInThisObjectAnd(string username, string username1, string transaction, int amount, string description, string status, string requestStatus)
        {
            CorrectTransactionDataArePresentInThisObjectAnd(username, username1, transaction, amount, description, status, requestStatus);
        }

        public void CorrectTransactionDataArePresentInThisObjectAnd(string username, string username1, string transaction, int amount, string description, string status, string requestStatus)
        {
            string userId = ((Dictionary<string, UserCreated>)_scenarioContext[ScenarioContextHelpers.usersIdMap]).GetValueOrDefault(username)!.id;
            string userId1 = ((Dictionary<string, UserCreated>)_scenarioContext[ScenarioContextHelpers.usersIdMap]).GetValueOrDefault(username1)!.id;

            Assert.Equal(userId, ((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).senderId);
            //Assert.Equal(userId1, ((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).receiverId);
            Assert.Equal(status, ((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).status);  // complete / pending

            if (String.IsNullOrEmpty(requestStatus))
            {
                Assert.Null(((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).requestStatus);
            }
            else
            {
                Assert.Equal(requestStatus, ((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).requestStatus);
            }

            Assert.Equal(amount * 100, ((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).amount);
            Assert.Equal(description, ((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).description);

            Assert.True(!String.IsNullOrEmpty(((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).createdAt));
            Assert.True(!String.IsNullOrEmpty(((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).modifiedAt));
            Assert.Equal(((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).createdAt, ((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).modifiedAt);
        }

        [Given(@"It is possible to obtain transactions list by get transaction request")]
        public void GivenItIsPossibleToObtainTransactionsListByGetTransactionRequest()
        {
            ItIsPossibleToObtainTransactionsListByGetTransactionRequest();
        }

        [Given(@"It is possible to obtain transaction from transactions list")]
        public void GivenItIsPossibleToObtainTransactionFromTransactionsList()
        {
            ItIsPossibleToObtainTransactionFromTransactionsList();
        }

        public void ItIsPossibleToObtainTransactionFromTransactionsList()
        {
            string jsonData = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;
            string jsonTransactions = JsonHelpers.getJson(jsonData, "results");
            var transactionsList = TestHelpers.getDeserializedClass<IList<TransactionGet>>(jsonTransactions);
            var transaction = transactionsList?.Where(x => x.id.Equals(((TransactionCreated)_scenarioContext[ScenarioContextHelpers.transactionCreated]).id)).FirstOrDefault();

            _scenarioContext[ScenarioContextHelpers.transactionGet] = transaction;

            Assert.NotNull(transaction);
        }

        public void ItIsPossibleToCompareObtainedTransactionWithDataAnd(string username, string username1, string transaction, int amount, string description, string status, string requestStatus)
        {            
            var userCreated = ((Dictionary<string, UserCreated>)_scenarioContext[ScenarioContextHelpers.usersIdMap]).GetValueOrDefault(username);
            var userCreated1 = ((Dictionary<string, UserCreated>)_scenarioContext[ScenarioContextHelpers.usersIdMap]).GetValueOrDefault(username1);

            string user = userCreated!.firstName + " " + userCreated!.lastName;
            string user1 = userCreated1!.firstName + " " + userCreated1!.lastName;

            var transactionGet = (TransactionGet)_scenarioContext[ScenarioContextHelpers.transactionGet];

            Assert.Equal(userCreated.id, transactionGet.senderId);
            Assert.Equal(userCreated1.id, transactionGet.receiverId);

            Assert.Equal(amount * 100, transactionGet.amount);
            Assert.Equal(description, transactionGet.description);

            Assert.Equal(user, transactionGet.senderName);
            Assert.Equal(user1, transactionGet.receiverName);

            Assert.Equal(status, transactionGet.status);

            if (string.IsNullOrEmpty(requestStatus))
            {
                Assert.Null(transactionGet.requestStatus);
            }
            else
            {
                Assert.Equal(requestStatus, transactionGet.requestStatus);
            }
        }

        [Given(@"It is possible to compare obtained transaction with data: ""([^""]*)"", ""([^""]*)"", ""([^""]*)"", (.*), ""([^""]*)"", ""([^""]*)"" and ""([^""]*)""")]
        public void GivenItIsPossibleToCompareObtainedTransactionWithDataAnd(string username, string username1, string transaction, int amount, string description, string status, string requestStatus)
        {
            ItIsPossibleToCompareObtainedTransactionWithDataAnd(username, username1, transaction, amount, description, status, requestStatus);
        }

        [When(@"User ""([^""]*)"" accepts transaction")]
        public void WhenUserAcceptsTransaction(string p0)
        {
            TransactionGet transactionPatch = ((TransactionGet)_scenarioContext[ScenarioContextHelpers.transactionGet]).DeepClone();
            
            Assert.False(object.Equals(((TransactionGet)_scenarioContext[ScenarioContextHelpers.transactionGet]), transactionPatch));


            transactionPatch.status = "complete";
            transactionPatch.requestStatus = "accepted";

            string cookieValue = (string)_scenarioContext[ScenarioContextHelpers.cookieValue];
            _scenarioContext[ScenarioContextHelpers.response] = HttpClientHelpers.GetHttpClientWithCookiesCookies(cookieValue).PatchAsync(ApiEndpoints.pathTransactions + transactionPatch.id, TestHelpers.GetJsonStringContent(transactionPatch)).GetAwaiter().GetResult();

        }

        public void CreatesATransactionFromUserToWithAndDescription(string usernameArg, string transactionType, string username, string username1, int amount, string description)
        {
            CreateTransaction createTransaction = new CreateTransaction();
            createTransaction.amount = amount;
            createTransaction.description = description;
            createTransaction.transactionType = transactionType;
            createTransaction.senderId = ((UserCreated)((Dictionary<string, UserCreated>)_scenarioContext[ScenarioContextHelpers.usersIdMap]).GetValueOrDefault(username)!).id;
            createTransaction.receiverId = ((UserCreated)((Dictionary<string, UserCreated>)_scenarioContext[ScenarioContextHelpers.usersIdMap]).GetValueOrDefault(username1)!).id;

            string cookieValue = (string)_scenarioContext[ScenarioContextHelpers.cookieValue];
            _scenarioContext[ScenarioContextHelpers.response] = HttpClientHelpers.GetHttpClientWithCookiesCookies(cookieValue).PostAsync(ApiEndpoints.pathTransactions, TestHelpers.GetJsonStringContent(createTransaction)).GetAwaiter().GetResult();
        }

        public void ItIsPossibleToObtainTransactionsListByGetTransactionRequest()
        {

            stopwatch = Stopwatch.StartNew();
            string cookieValue = (string)_scenarioContext[ScenarioContextHelpers.cookieValue];
            _scenarioContext[ScenarioContextHelpers.response] = HttpClientHelpers.GetHttpClientWithCookiesCookies(cookieValue).GetAsync(ApiEndpoints.pathTransactions).GetAwaiter().GetResult();
        }

        public void TransactionObjectIsObtainedFromResponse()
        {
            var jsonData = ((HttpResponseMessage)_scenarioContext[ScenarioContextHelpers.response]).Content.ReadAsStringAsync().Result;

            logger.Info(String.Format("user created - response {0} at {1}", jsonData, DateTime.Now));

            jsonData = JsonHelpers.getJson(jsonData, "transaction");
            var value = TestHelpers.getDeserializedClass<TransactionCreated>(jsonData);
            _scenarioContext[ScenarioContextHelpers.transactionCreated] = value;
        }
    }
}
