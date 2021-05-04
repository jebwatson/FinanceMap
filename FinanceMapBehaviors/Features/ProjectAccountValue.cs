using System;
using FinanceMap;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace FinanceMapBehaviors.Features
{
    [Binding]
    public class StepDefinitions
    {
        private const double Delta = 0.001;
        private readonly AccountValueProjector sut = new();
        private Account account = new();
        private Account futureAccount = new();
        private DateTime futureDate;
        private Income income = new();
        private DateTime startingDate;

        public StepDefinitions(ScenarioContext scenarioContext)
        {
        }

        [Given(@"an account object")]
        public void GivenAnAccountObject()
        {
            Assert.NotNull(this.account);
        }

        [Given(@"(.*) dollars as the current value")]
        public void GivenDollarsAsTheCurrentValue(double currentValue)
        {
            this.account = this.account with { Value = currentValue };
            Assert.AreEqual(currentValue, this.account.Value, Delta);
        }

        [Given(@"(.*) weeks from today as the future date")]
        public void GivenWeeksFromTodayAsTheFutureDate(int weeksFromToday)
        {
            this.startingDate = DateTime.Today;
            this.futureDate = this.startingDate + TimeSpan.FromDays(weeksFromToday * 7);
        }

        [Given(@"an income object")]
        public void GivenAnIncomeObject()
        {
            Assert.NotNull(this.income);
        }

        [Given(@"(.*) weeks as the frequency")]
        public void GivenWeeksAsTheFrequency(int frequency)
        {
            this.income = this.income with { Frequency = TimeSpan.FromDays(frequency * 7) };
            Assert.True(TimeSpan.FromDays(frequency * 7).Equals(this.income.Frequency));
        }

        [Given(@"(.*) dollars as the value")]
        public void GivenDollarsAsTheValue(double value)
        {
            this.income = this.income with { Value = value };
            Assert.AreEqual(value, this.income.Value, Delta);
        }

        [When(@"the user requests the account value")]
        public void WhenTheUserRequestsTheAccountValue()
        {
            this.futureAccount = this.sut.ProjectValueAtDate(this.account, this.futureDate, this.income);
            Assert.NotNull(this.futureAccount);
        }

        [Then(@"(.*) dollars is the future account value")]
        public void ThenDollarsIsTheFutureAccountValue(double futureAccountValue)
        {
            Assert.AreEqual(futureAccountValue, this.futureAccount.Value, Delta);
        }
    }
}