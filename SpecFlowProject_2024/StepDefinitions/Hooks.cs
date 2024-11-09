using TechTalk.SpecFlow;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]

namespace SpecFlowProject_2024.StepDefinitions
{
    [Binding]
    public sealed class Hooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        WebdriverReferenceHandler webdriverReferenceHandler;


        [BeforeScenario("@tag1")]
        public void BeforeScenarioWithTag()
        {
            // Example of filtering hooks using tags. (in this case, this 'before scenario' hook will execute if the feature/scenario contains the tag '@tag1')
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=hooks#tag-scoping

            //TODO: implement logic that has to run before executing each scenario

        }

        [BeforeScenario(Order = 1)]
        public void FirstBeforeScenario()
        {
            // Example of ordering the execution of hooks
            // See https://docs.specflow.org/projects/specflow/en/latest/Bindings/Hooks.html?highlight=order#hook-execution-order

            //TODO: implement logic that has to run before executing each scenario
            //webdriverReferenceHandler = new WebdriverReferenceHandler();
            //WebdriverReferenceHandler.GetBrowserInstance("chrome", false);
            //webdriverReferenceHandler.GetBrowserInstance("chrome", false);
            
        }

        [AfterScenario]
        public void AfterScenario()
        {
            //TODO: implement logic that has to run after executing each scenario
        }
    }
}