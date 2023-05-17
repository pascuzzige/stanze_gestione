using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stanze_management.Data;
using Stanze_management.Data.Entities;
using Stanze_management.DBContexts;

namespace Stanze_management.Controllers
{
    //creo l'API chiamata UfficiController
    [ApiController]
    [Route("[controller]")]
    public class UfficiController:ControllerBase
    {
        private readonly Stanze_dbContext _Stanze_dbContext;
        public UfficiController(Stanze_dbContext Stanze_dbContext)
        {
            _Stanze_dbContext= Stanze_dbContext;
        }
        //operazione di lettura
        [HttpGet]        
        public async Task<IActionResult> GetAsync() 
        {
           var stanze = await _Stanze_dbContext.Uffici.ToListAsync();
           return Ok(stanze);
        }

        //Leggo per id
        [HttpGet]
        [Route("getbyid/{id}")]
        public  async Task<IActionResult> GetStanzaById(int id)
        {
            var uffici= await _Stanze_dbContext.Uffici.FindAsync(id);
            return Ok(uffici);  
        }

        //Aggiungo stanze alla mia tabella e ordino per id
        [HttpPost]
        public async Task<IActionResult> PostAsync(Uffici uffici)
        {
            try
            {
                _Stanze_dbContext.Uffici.Add(uffici);
                await _Stanze_dbContext.SaveChangesAsync();
                return Created($"/getbyid/{uffici.Id}", uffici);
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); return BadRequest(ex.Message); }
            
            
        }
        //Aggiornare propietà degli items
        [HttpPut]
        public async Task<IActionResult> PutAsync(Uffici ufficioToUpdate)
        {
            try
            {
                _Stanze_dbContext.Uffici.Update(ufficioToUpdate);
                await _Stanze_dbContext.SaveChangesAsync();
                return NoContent();

            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); return BadRequest(ex.Message); } 
            
        }
        //Per cancellare attraverso un id
        [HttpDelete]
        [Route("{id:int}")]
        public  async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                var ufficioToDelete = await _Stanze_dbContext.Uffici.FindAsync(id);

                if (ufficioToDelete == null)
                {
                    return NotFound();
                }
                _Stanze_dbContext.Uffici.Remove(ufficioToDelete);
                await _Stanze_dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); return BadRequest(ex.Message); }



        }
    }
}
