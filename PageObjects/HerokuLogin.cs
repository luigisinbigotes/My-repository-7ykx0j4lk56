using Microsoft.Playwright;

namespace PageObjects
{
    public class HerokuLogin : BasePage
    {
        public HerokuLogin(IPage page)
            : base(page)
        { }

        public ILocator TxtUsername => _page.Locator("[id=username]");

        public ILocator TxtPassword => _page.Locator("[id=password]");

        public ILocator BtnLogin => _page.Locator("[type=submit]");
    }
}