using System.Web.Mvc;

namespace Coqueta.Mvc.Controllers
{
    public class DataController : Controller
    {
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        public ActionResult Edit(int id)
        {
            return View(id);
        }
    }

}