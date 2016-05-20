using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using TimeToShineBackend.Models;

namespace TimeToShineBackend.Controllers
{
    public class UserColorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // Approval actions
        // GET: UserColors/Approve
        public async Task<ActionResult> Approve()
        {
            // Show only the items that haven't been approved or rejected
            return View(await db.UserColors.Where(c => c.Approved == null).OrderByDescending(p => p.Submitted).ToListAsync());
        }

        // GET: UserColors/ApproveColor/5
        public async Task<ActionResult> ApproveColor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserColor userColor = await db.UserColors.FindAsync(id);
            if (userColor == null)
            {
                return HttpNotFound();
            }

            userColor.Approved = true;
            await db.SaveChangesAsync();
            return RedirectToAction("Approve");
        }

        // GET: UserColors/RejectColor/5
        public async Task<ActionResult> RejectColor(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserColor userColor = await db.UserColors.FindAsync(id);
            if (userColor == null)
            {
                return HttpNotFound();
            }

            userColor.Approved = false;
            await db.SaveChangesAsync();
            return RedirectToAction("Approve");
        }


        // Standard actions
        // GET: UserColors
        public async Task<ActionResult> Index()
        {
            return View(await db.UserColors.ToListAsync());
        }

        // GET: UserColors/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserColor userColor = await db.UserColors.FindAsync(id);
            if (userColor == null)
            {
                return HttpNotFound();
            }
            return View(userColor);
        }

        // GET: UserColors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserColors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,ColorName,Approved,Submitted,SubmitterName,SubmitterAge,SubmitterLocation,Red,Green,Blue")] UserColor userColor)
        {
            if (ModelState.IsValid)
            {
                userColor.Submitted = userColor.Submitted ?? DateTime.Now;
                db.UserColors.Add(userColor);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(userColor);
        }

        // GET: UserColors/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserColor userColor = await db.UserColors.FindAsync(id);
            if (userColor == null)
            {
                return HttpNotFound();
            }
            return View(userColor);
        }

        // POST: UserColors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,ColorName,Approved,Submitted,SubmitterName,SubmitterAge,SubmitterLocation,Red,Green,Blue")] UserColor userColor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userColor).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(userColor);
        }

        // GET: UserColors/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserColor userColor = await db.UserColors.FindAsync(id);
            if (userColor == null)
            {
                return HttpNotFound();
            }
            return View(userColor);
        }

        // POST: UserColors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            UserColor userColor = await db.UserColors.FindAsync(id);
            db.UserColors.Remove(userColor);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
