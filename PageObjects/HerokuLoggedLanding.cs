using Microsoft.Playwright;

namespace PageObjects
{
    public class HerokuLoggedLanding : BasePage
    {
        public HerokuLoggedLanding(IPage page)
            : base(page)
        { }

        public ILocator TxtSuccessfullyLogged => _page.Locator("[id=flash]");
    }
}