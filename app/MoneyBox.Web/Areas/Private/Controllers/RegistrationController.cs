namespace MoneyBox.Web.Areas.Private.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using MoneyBox.Domain;

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
    }

    /// <summary>
    /// Class that encapsulates most common parameters sent by DataTables plugin
    /// </summary>
    public class JQueryDataTableParamModel
    {
        /// <summary>
        /// Request sequence number sent by DataTable,
        /// same value must be returned in response
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Text used for filtering
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Number of records that should be shown in table
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// First record that should be shown(used for paging)
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Number of columns in table
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Number of columns that are used in sorting
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Comma separated list of column names
        /// </summary>
        public string sColumns { get; set; }

        /// <summary>
        /// First ordering column
        /// </summary>
        public int iSortCol_0 { get; set; }

        /// <summary>
        // First ordering column direction
        /// </summary>
        public string sSortDir_0 { get; set; }

        public string[] FilteringColums
        {
            get { return sColumns.Split(','); }
        }

        public string SearchTerms
        {
            get { return string.IsNullOrEmpty(sSearch) ? string.Empty : sSearch; }
        }
    }
}