
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using FlexisourceApplicationTest.Models;

namespace FlexisourceApplicationTest.Controllers
{
    public class CustomerController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        public IQueryable<Customers> GetAll()
        {
            return db.Customers;
        }

        [ResponseType(typeof(Customers))]
        public async Task<IHttpActionResult> Get(int id)
        {
            Customers customers = await db.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(customers).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!customers.Id.HasValue)
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

        [ResponseType(typeof(Customers))]
        public async Task<IHttpActionResult> Post(Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Customers.Add(customers);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = customers.Id }, customers);
        }

        [ResponseType(typeof(Customers))]
        public async Task<IHttpActionResult> Delete(Customers customers)
        {
            Customers result = await db.Customers.FindAsync(customers.Id);
            if (result == null)
            {
                return NotFound();
            }

            db.Customers.Remove(result);
            await db.SaveChangesAsync();

            return Ok(result);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Exists(int id)
        {
            return db.Customers.Count(e => e.Id == id) > 0;
        }
    }
}