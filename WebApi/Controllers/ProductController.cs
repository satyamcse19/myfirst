using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ProductController : ApiController
    {

        private TestEntities db = new TestEntities();
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                var data = await db.Products.ToListAsync();
                return Ok(data);
            }
            catch (Exception ex)
            {

                return Ok(ex.Message);
            }

        }

        [HttpPost]
        public async Task<IHttpActionResult> Create([FromBody] Product product)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    return CreatedAtRoute("DefaultApi", new { id = product.Id }, product);
                }

                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {

                return Ok(ex.StackTrace + "" + ex.Message);
            }




        }
        [HttpPut]
        public async Task<IHttpActionResult> Update(Product Product, int Id)
        {
            try
            {
                var record = await db.Products.Where(f => f.Id == Id).FirstOrDefaultAsync();
                if (ModelState.IsValid && record != null)
                {
                    record.Name = Product.Name;
                    record.Description = Product.Description;
                    record.Price = Product.Price;
                    await db.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return Ok(ex.StackTrace + "" + ex.Message);
            }




        }
        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int Id)
        {
            try
            {
                var record = await db.Products.Where(f => f.Id == Id).FirstOrDefaultAsync();
                if (record != null)
                {
                    db.Products.Remove(record);
                    await db.SaveChangesAsync();
                    return Ok();
                }
                return NotFound();
            }
            catch (Exception ex)
            {

                return Ok(ex.StackTrace + "" + ex.Message);
            }




        }
    }
}
