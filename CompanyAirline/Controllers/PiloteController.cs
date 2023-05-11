using CompanyAirline.Models.Company;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CompanyAirline.Controllers
{
    public class PiloteController : Controller
    {
        // GET: PiloteController
        CompanyDbContext ctx;
        public PiloteController(CompanyDbContext context)
        {
            ctx = context;
        }
        public ActionResult Index()
        {
            return View(ctx.Pilotes.ToList());
        }

        // GET: PiloteController/Details/5
        public ActionResult Details(int id)
        {
            return View(ctx.Pilotes.Find(id));
        }

        // GET: PiloteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PiloteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Pilote pilote)
        {
            try
            {
                ctx.Pilotes.Add(pilote);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult PilotesAndTheirVols()
        {
            var CompanyDbContext = ctx.Vols.ToList();
            return View(ctx.Pilotes.ToList());
        }
        // GET: PiloteController/Edit/5
        public ActionResult Edit(int id)
        {
            Pilote pilote = ctx.Pilotes.Find(id);
            return View(pilote);
        }


        // POST: PiloteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Pilote pilote)
        {
            try
            {
                ctx.Pilotes.Update(pilote);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PiloteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(ctx.Pilotes.Find(id));
        }

        // POST: PiloteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection,Pilote pilote)
        {
            try
            {

                ctx.Pilotes.Remove(pilote);
                ctx.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
