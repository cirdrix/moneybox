namespace MoneyBox.Web.Areas.Private.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using MoneyBox.Domain;
    using MoneyBox.Web.Areas.Private.Models;

    using Services;

    [Authorize]
    public class RegistrationController : Controller
    {
        private IRegistrationService registrationService;

        public RegistrationController(IRegistrationService registrationService)
        {
            this.registrationService = registrationService;
        }

        // GET: Private/Registration
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult LoadTable(JQueryDataTableParamModel param)
        {
            int pageRows = param.iDisplayLength;
            int pageNumber = (param.iDisplayStart / param.iDisplayLength);
            var searchTerm = param.SearchTerms;

            var sortorder = param.sSortDir_0;
            var sortname = param.FilteringColums[param.iSortCol_0];
            IEnumerable<BoxRegistration> rows;
            int tableRows;
            try
            {
                rows = registrationService.GetRegistrationsPaged(HttpContext.User.Identity.Name, searchTerm, null, pageNumber, pageRows, sortorder, sortname, out tableRows);
            }
            catch (Exception)
            {
                return Json(new { sEcho = param.sEcho, iTotalRecords = 0, iTotalDisplayRecords = 0 }, JsonRequestBehavior.AllowGet);
            }
            
            return Json(new
            {
                sEcho = param.sEcho,
                iTotalRecords = tableRows,
                iTotalDisplayRecords = tableRows,
                aaData = rows.Select(x => new string[]
                    {
                        x.Id.ToString(),
                        x.Description,
                        x.Box.Description,
                        x.Amount.ToString("C2")
                    }).ToList()

            },
            JsonRequestBehavior.AllowGet);
        }

        public ActionResult Import()
        {
            return this.View();
        }
    }
}