using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PageObjects;

namespace PlaywrightTests;

[NonParallelizable]
[TestFixture]
public class Tests : PageTest
{
    private MyPage MyTestPage => new(Page);
    private MyOtherPage OtherTestPage => new(Page);

    private HerokuLogin LoginPage => new(Page);

    private HerokuLoggedLanding LoggedPage => new(Page);

    [Test]
    public async Task LandsOnPageAndPerformValidations_ValidationsPasses()
    {
        await Page.GotoAsync("https://www.google.com/");
        await Expect(Page).ToHaveTitleAsync("Google");

        //await this.MyTestPage.BtnSearch.ClickAsync();
        await this.MyTestPage.TxtSearch.TypeAsync("Outsource");
        await this.MyTestPage.TxtSearch.PressAsync("Enter");

        var searchString = "DescripciónLa subcontratación, ​ externalización​ o tercerización es un acuerdo por el que una empresa contrata a otra para que se encargue de una actividad prevista o existente que se realiza o podría realizarse internamente y, en ocasiones, implica la transferencia de empleados y activos de una empresa a otra. Wikipedia";
        var results = this.OtherTestPage.Results.InnerTextAsync().Result;

        await Expect(this.OtherTestPage.Results).ToContainTextAsync(searchString);
    }

    [Test]
    public async Task AttemptsToLoginToHerokuApp_UserIsLogged()
    {
        await Page.GotoAsync("https://the-internet.herokuapp.com/login");

        await this.LoginPage.TxtUsername.TypeAsync("tomsmith");
        await this.LoginPage.TxtPassword.TypeAsync("SuperSecretPassword!");
        await this.LoginPage.BtnLogin.ClickAsync();

        await Expect(this.LoggedPage.TxtSuccessfullyLogged).ToContainTextAsync("You logged into a secure area!");
    }

    [Test]
    public async Task CheckResultsThroughResultsList_FindsMatchingExpected()
    {
        await Page.GotoAsync("https://www.hexacta.com/");

        await this.MyTestPage.BtnSearch.ClickAsync();
        await this.MyTestPage.TxtSearch.TypeAsync("Outsource");
        await this.MyTestPage.TxtSearch.PressAsync("Enter");

        var searchString = "Developing an App with an Outsourcing Team: What you Need to Know";
        var results = this.OtherTestPage.Results.AllInnerTextsAsync().Result;

        var found = false;
        while (!found && this.OtherTestPage.LnkNextPage.IsVisibleAsync().Result)
        {
            found = results.Any(x => x.ToUpper() == searchString.ToUpper());
            if (found)
            {
                var xpath = $".//a[contains(text(),'{searchString}')]";
                await Expect(Page.Locator($"xpath={xpath}")).ToBeVisibleAsync();
            }
            await this.OtherTestPage.LnkNextPage.ClickAsync();
            results = this.OtherTestPage.Results.AllInnerTextsAsync().Result;
        }
    }
}