using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TimeToShineBackend.Models;

namespace TimeToShineBackend.Controllers.Api
{
    public class RandomColorController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/RandomColor
        [ResponseType(typeof(UserColor))]
        public IHttpActionResult GetRandomColor()
        {
            var userColor = PickRandomColor();
            if (userColor == null)
            {
                return NotFound();
            }

            return Ok(userColor);
        }

        // GET: api/RandomColor/5
        [ResponseType(typeof(UserColor))]
        public IHttpActionResult GetRandomColor(int id)
        {
            var userColor = PickRandomColor(id);
            if (userColor == null)
            {
                return NotFound();
            }

            return Ok(userColor);
        }

        private UserColor PickRandomColor(int minutesToSearchIn = 10)
        {
            IEnumerable<UserColor> colorsInTimeFrame = new List<UserColor>();

            // pick a random color submitted in the last X minutes
            // if there are less than 4? colors submitted in that time frame, keep doubling it until we get more.
            // Drop out if we are searching for more than 7 days back (to stop infinite searching)
            while (colorsInTimeFrame.Count() < 4 && minutesToSearchIn < 60 * 24 * 7)
            {
                var dateTimeSearch = DateTime.Now.AddMinutes(-minutesToSearchIn);
                colorsInTimeFrame = db.UserColors.Where(c => c.Approved == true && c.Submitted > dateTimeSearch);
                minutesToSearchIn *= 2;
            }

            // if there aren't any items, return null
            if (!colorsInTimeFrame.Any())
                return null;

            // pick random item, by just assigning each a random GUID and sorting by that
            var selectedColor = colorsInTimeFrame.OrderBy(qu => Guid.NewGuid()).First();
            return selectedColor;
        }
    }
}
