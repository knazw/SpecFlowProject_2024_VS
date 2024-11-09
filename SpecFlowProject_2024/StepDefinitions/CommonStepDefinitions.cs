using SpecFlowProject_2024.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpecFlowProject_2024.StepDefinitions
{
    internal class CommonStepDefinitions
    {
        public static void ObtainCookieFromHeaders(ScenarioContext scenarioContext)
        {
            var cookie = ((HttpResponseMessage)scenarioContext[ScenarioContextHelpers.response]).Headers.GetValues("Set-Cookie");
            string[] cookieArray = cookie.FirstOrDefault()?.Split(";")!;

            scenarioContext[ScenarioContextHelpers.cookieValue] = cookieArray[0];
        }
    }
}
