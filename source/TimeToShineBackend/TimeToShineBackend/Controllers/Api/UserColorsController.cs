using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TimeToShineBackend.Models;

namespace TimeToShineBackend.Controllers.Api
{
    public class UserColorsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/UserColors
        public IQueryable<UserColor> GetUserColors()
        {
            return db.UserColors;
        }

        // GET: api/UserColors/5
        [ResponseType(typeof(UserColor))]
        public async Task<IHttpActionResult> GetUserColor(int id)
        {
            UserColor userColor = await db.UserColors.FindAsync(id);
            if (userColor == null)
            {
                return NotFound();
            }

            return Ok(userColor);
        }

        // PUT: api/UserColors/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUserColor(int id, UserColor userColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userColor.Id)
            {
                return BadRequest();
            }

            db.Entry(userColor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserColorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/UserColors
        [ResponseType(typeof(UserColor))]
        public async Task<IHttpActionResult> PostUserColor(UserColor userColor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            userColor.Submitted = userColor.Submitted ?? DateTime.Now;
            db.UserColors.Add(userColor);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = userColor.Id }, userColor);
        }

        // DELETE: api/UserColors/5
        [ResponseType(typeof(UserColor))]
        public async Task<IHttpActionResult> DeleteUserColor(int id)
        {
            UserColor userColor = await db.UserColors.FindAsync(id);
            if (userColor == null)
            {
                return NotFound();
            }

            db.UserColors.Remove(userColor);
            await db.SaveChangesAsync();

            return Ok(userColor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserColorExists(int id)
        {
            return db.UserColors.Count(e => e.Id == id) > 0;
        }
    }
}