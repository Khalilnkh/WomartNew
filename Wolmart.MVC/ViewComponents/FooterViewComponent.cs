using Microsoft.AspNetCore.Mvc;
using Wolmart.MVC.ViewModels.Footer;

namespace Wolmart.MVC.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(FooterVM footerVM)
        {
            return View(await Task.FromResult(footerVM));
        }
    }
}
