using System;
using FinanceMap;
using TechTalk.SpecFlow;

namespace FinanceMapBehaviors.Features
{
    [Binding]
    public class StepDefinitions
    {
        private readonly ScenarioContext scenarioContext;

        public StepDefinitions(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

        [StepArgumentTransformation]
        public Account TransformAccount(double accountValue) => new() { Value = accountValue };

        [Given(@"an account (.*)")]
        public void GivenAnAccount(Account account)
        {
            this.scenarioContext.Pending();
        }
        
        [Given(@"a starting date (.*)")]
        public void GivenAStartingDate(DateTime startingDate)
        {
            this.scenarioContext.Pending();
        }
        
        [Given(@"a projection date (.*)")]
        public void GivenAProjectionDate(DateTime projectionDate)
        {
            this.scenarioContext.Pending();
        }
        
        [Given(@"an income (.*)")]
        public void GivenAnIncome(Income income)
        {
            this.scenarioContext.Pending();
        }
        
        [When(@"the user requests the account value")]
        public void WhenTheUserRequestsTheAccountValue()
        {
            this.scenarioContext.Pending();
        }
        
        [Then(@"the future account value is (.*)")]
        public void ThenTheFutureAccountValueIs(double futureValue)
        {
            this.scenarioContext.Pending();
        }
    }
}