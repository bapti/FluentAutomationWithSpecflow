using BoDi;
using FluentAutomation.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace FluentAutomationTest
{
    [Binding]
    public class WebScenario : FluentAutomation.FluentTest
    {
        private readonly IObjectContainer objectContainer;

        public WebScenario(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;

            FluentAutomation.Settings.ScreenshotPath = @"C:\Work\Temp";
            FluentAutomation.Settings.ScreenshotOnFailedExpect = false;
            FluentAutomation.Settings.ScreenshotOnFailedAction = false;
            FluentAutomation.Settings.DefaultWaitTimeout = TimeSpan.FromSeconds(1);
            FluentAutomation.Settings.DefaultWaitUntilTimeout = TimeSpan.FromSeconds(30);
            FluentAutomation.Settings.MinimizeAllWindowsOnTestStart = true;
        }

        [BeforeScenario("Web")]
        public void BeforeScenario()
        {
            FluentAutomation.SeleniumWebDriver.Bootstrap(FluentAutomation.SeleniumWebDriver.Browser.Firefox);

            objectContainer.RegisterInstanceAs<INativeActionSyntaxProvider>(I);
        }

        [AfterScenario("Web")]
        public void AfterScenario()
        {

        }
    }

    [Binding]
    public class GoogleSteps
    {
        public INativeActionSyntaxProvider I { get; set; }
        
        public GoogleSteps(INativeActionSyntaxProvider I)
        {
            this.I = I;
        }

        [Given(@"I go to google")]
        public void GivenIGoToGoogle()
        {
            I.Open("http://google.co.uk");
        }

        [Then(@"I should be at google")]
        public void ThenIShouldBeAtGoogle()
        {
            I.Expect.Url(x => x.AbsoluteUri.Contains("google"));
        }

    }
}
