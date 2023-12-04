using Microsoft.Playwright;

namespace PageObjects
{
    public class MyOtherPage : BasePage
    {
        public MyOtherPage(IPage page)
            : base(page)
        { }

        // returns all a elements found within articles in result page
        public ILocator Results => _page.Locator("[data-attrid=description]");

        public ILocator LnkPreviousPage => _page.Locator("a.prev");

        public ILocator LnkNextPage => _page.Locator("a.next");

        public ILocator LblCurrentPage => _page.Locator("span[aria-current=page]");
    }
}