namespace Desktop.Tests.Client
{
    using NUnit.Framework;

    using OpenQA.Selenium;

    using Should;

    public class TypescriptUnitTests : Test
    {
        [Test]
        public void Status()
        {
            var wrappedDriver = this.Driver.WrappedDriver;
            wrappedDriver.Navigate().GoToUrl(Test.TSUnitUrl);

            var resultDiv = wrappedDriver.FindElement(By.Id("result"));
            var bad = resultDiv.FindElement(By.ClassName("bad"));

            bad.Text.ShouldEqual("0");
        }
    }
}