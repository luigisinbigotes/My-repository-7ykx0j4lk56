using Microsoft.Playwright;

namespace PageObjects
{
    public class BasePage
    {
        protected IPage _page;

        public BasePage(IPage page) => _page = page;
    }
}