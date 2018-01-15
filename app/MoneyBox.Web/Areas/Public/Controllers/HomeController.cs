using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MoneyBox.Web.Areas.Public.Controllers
{
    using System.Data;

    using MoneyBox.Domain;
    using MoneyBox.Services;
    using MoneyBox.Utils;
    using MoneyBox.Web.Areas.Private.Models;

    using OfficeOpenXml;

    public class HomeController : Controller
    {
        private readonly IRegistrationService registrationService;

        public HomeController(IRegistrationService registrationService)
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

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase uploadFile)
        {
            DataTable dt = new DataTable();

            if (uploadFile != null && uploadFile.ContentLength > 0)
            {
                if (uploadFile.FileName.EndsWith(".xls") || uploadFile.FileName.EndsWith(".xlsx"))
                {
                    try
                    {
                        using (var fileExcel = new ExcelPackage())
                        {
                            fileExcel.Load(uploadFile.InputStream);
                            dt = ExcelUtils.GetDataTableFromExcel(fileExcel, true);
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ModelState.AddModelError(string.Empty, "Error during upload file");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("File", "Only excel files are importable");
                }
            }
            else
            {
                ModelState.AddModelError("File", "You have to select and excel file");
            }

            try
            {
                registrationService.Import(dt, 1);
            }
            catch (Utils.ValidationException valRx)
            {
                foreach (var error in valRx.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Value);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error during importing file");
            }

            return this.View();
        }
    }
}