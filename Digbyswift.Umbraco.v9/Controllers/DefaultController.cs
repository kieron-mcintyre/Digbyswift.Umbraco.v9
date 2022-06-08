using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Digbyswift.Umbraco.v9.Controllers
{
    public class DefaultController : BaseController
    {
        public DefaultController(ControllerDependencies dependencies) : base(dependencies) {}

        public override IActionResult Index()
        {
            if (CurrentPage.TemplateId > 0)
                return CurrentTemplate(CurrentPage);

            Logger.LogWarning($"Invalid page request: {Request.Path} #request");

            return NotFound();
        }
    }
}