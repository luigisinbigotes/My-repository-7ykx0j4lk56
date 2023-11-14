using Microsoft.Playwright;

namespace PageObjects
{
    public class MyPage : BasePage
    {
        public MyPage(IPage page)
            : base(page)
        {
            _page = page;
        }

        public ILocator BtnSearch => _page.Locator("css=a.searchbox");

        public ILocator TxtSearch => _page.Locator("css=[type=search]");
    }
}