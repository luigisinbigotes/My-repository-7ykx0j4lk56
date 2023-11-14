using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using PageObjects;

namespace PlaywrightTests;

[NonParallelizable]
[TestFixture]
public class HxTests : PageTest
{
    private MyPage MyTestPage => new(Page);
    private MyOtherPage OtherTestPage => new(Page);

    [Test]
    public async Task LandsOnPageAndPerformValidations_ValidationsPasses()
    {
        await Page.GotoAsync("https://www.hexacta.com/");
        await Expect(Page).ToHaveTitleAsync("Nearshore Software Development Company | Hexacta");

        await this.MyTestPage.BtnSearch.ClickAsync();
        await this.MyTestPage.TxtSearch.TypeAsync("Outsource");
        await this.MyTestPage.TxtSearch.PressAsync("Enter");

        var xpath = ".//a[contains(text(),'What not to do when working with an outsourced software team')]";
        await Expect(Page.Locator($"xpath={xpath}")).ToBeVisibleAsync();
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