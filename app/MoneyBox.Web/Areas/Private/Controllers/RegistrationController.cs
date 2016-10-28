namespace MoneyBox.Web.Areas.Private.Controllers
{
    using System.Web.Mvc;

    [Authorize]
    public class RegistrationController : Controller
    {
        // GET: Private/Registration
        public ActionResult Index()
        {
            return View();
        }
    }
}