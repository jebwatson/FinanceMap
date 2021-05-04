using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FinanceMapBehaviors.Features
{
    [Binding]
    public class StepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;

        public StepDefinitions(ScenarioContext scenarioContext)
        {
            this._scenarioContext = scenarioContext;
        }
        [Given(@"the starting account value")]
        public void GivenTheStartingAccountValue()
        {
            Assert.False(true);
            this._scenarioContext.Pending();
        }
        
        [Given(@"the future date")]
        public void GivenTheFutureDate()
        {
            this._scenarioContext.Pending();
        }
        
        [When(@"the user requests the account value")]
        public void WhenTheUserRequestsTheAccountValue()
        {
            this._scenarioContext.Pending();
        }
        
        [Then(@"the account value is returned")]
        public void ThenTheAccountValueIsReturned()
        {
            this._scenarioContext.Pending();
        }
    }
}