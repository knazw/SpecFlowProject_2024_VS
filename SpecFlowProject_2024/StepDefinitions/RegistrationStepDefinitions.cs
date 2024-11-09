using Newtonsoft.Json;
using NUnit.Framework;
using SpecFlowProject_2024.Model;
using SpecFlowProject_2024.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SpecFlowProject_2024.StepDefinitions
{
    [Binding]
    public  sealed class RegistrationStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;

        private readonly string _mainPage = "mainPage";
        private readonly string _registerPage = "registerPage";
        private readonly string _currentUser = "currentUser";
        private readonly string _registerResult = "registerResult";

        public RegistrationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given("following user (.*) from (.*) in (.*)")]
        public void FollowingUser(string username, string fileName, string path)
        {
            string json2Deserialize = ReadFile(path + fileName);

            Dictionary<string, User>? users = JsonConvert.DeserializeObject<Dictionary<string, User>>(json2Deserialize);


            User user = (User)users![username];
            user.Email = changeEmail(((User)users![username]!).Email);

            users[username] = user;
            _scenarioContext[_currentUser] = user;

            string jsonString = JsonSerializer.Serialize(users);

            WriteToFile(path + fileName, jsonString);
        }



        [Given("User (.*) is on register page")]
        public void UserIsOnRegisterPage(string username) 
        {

            WebdriverReferenceHandler.webDriver.Navigate().GoToUrl("https://demo.nopcommerce.com/");
            MainPage mainPage = new MainPage(WebdriverReferenceHandler.webDriver);

            _scenarioContext[_mainPage] = mainPage;

            RegisterPage registerPage = mainPage.clickRegister();
            _scenarioContext[_registerPage] = registerPage;
        }

        [Given(@"User fills all fields on registration page")]
        public void GivenUserFillsAllFieldsOnRegistrationPage()
        {
            var currentUser = (User) _scenarioContext[_currentUser];

            var registerPage = (RegisterPage)_scenarioContext[_registerPage];
            registerPage.FillFields(currentUser);
        }

        [When(@"User presses the register button")]
        public void WhenUserPressesTheRegisterButton()
        {
            var registerPage = (RegisterPage)_scenarioContext[_registerPage];
            _scenarioContext[_registerResult] = registerPage.ClickRegister();
        }

        [Then(@"Process ends with a sentence ""([^""]*)""")]
        public void ThenProcessEndsWithASentence(string sentence)
        {
            var registerResult = (RegisterResult)_scenarioContext[_registerResult];

            string registrationResult = registerResult.GetRegistrationResult();
            Assert.AreEqual(sentence, registrationResult);            
        }


        [Given("!the second number is (.*)")]
        public void GivenTheSecondNumberIs(int number)
        {
            throw new PendingStepException();
        }

        [When("!the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            throw new PendingStepException();
        }

        [Then("!the result should be (.*)")]
        public void ThenTheResultShouldBe(int result)
        {
            throw new PendingStepException();
        }

        private string changeEmail(string email)
        {

            string[] split = email.Split("@");
            return (Int32.Parse(split[0]) + 1) + "@" + split[1];
        }

        private static string ReadFile(string path)
        {
            string contents;
            using (var sr = new StreamReader(path))
            {
                contents = sr.ReadToEnd();
            }

            return contents;
        }

        private static void WriteToFile(string path, string jsonString)
        {
            using (FileStream fs = File.Create(path))
            {
                byte[] info = new UTF8Encoding(true).GetBytes(jsonString);
                fs.Write(info, 0, info.Length);
            }
        }
    }
}
