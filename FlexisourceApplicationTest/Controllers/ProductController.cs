
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
    public class ProductController : ApiController
    {
        private AppDbContext db = new AppDbContext();

        public IQueryable<Products> GetAll()
        {
            return db.Products;
        }

        [ResponseType(typeof(Products))]
        public async Task<IHttpActionResult> Get(int id)
        {
            Products products = await db.Products.FindAsync(id);
            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Put(Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(products).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!products.Id.HasValue)
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

        [ResponseType(typeof(Products))]
        public async Task<IHttpActionResult> Post(Products products)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Products.Add(products);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = products.Id }, products);
        }

        [ResponseType(typeof(Products))]
        public async Task<IHttpActionResult> Delete(Products product)
        {
            Products result = await db.Products.FindAsync(product.Id);
            if (result == null)
            {
                return NotFound();
            }

            db.Products.Remove(result);
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
            return db.Products.Count(e => e.Id == id) > 0;
        }
    }
}