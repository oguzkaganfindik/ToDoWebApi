using Microsoft.AspNetCore.Mvc;
using ToDoWebApi.Context;
using ToDoWebApi.Entities;
using ToDoWebApi.Models;

namespace ToDoWebApi.Controllers
{
    // WebApi için genellikle "ToDos" tarzında yani çoğul açılır.
    // Tekil açılmasında da bir sakınca yok.

    [Route("api/[controller]")] // api/actionadi şeklinde istek atılacak.
    [ApiController] // Bunun bir api controller olduğunu belirtiyorum.
    public class ToDosController : Controller
    {
        // GET -> Kayıt ve kayıtları çekmek
        // POST -> Yeni bir kayıt eklemek
        // PATCH -> Olan bir kaydı güncellemek
        // PUT -> Olan bir kaydın tüm özelliklerini (temel yapı) güncellemek
        // DELETE -> Olan bir kaydı silmek

        // Status Codes
        // 200 -> Ok (Success)
        // 400 -> Bad Request
        // 403 -> NoPerm
        // 404 -> Not Found
        // 500 -> Server Error

        private readonly ToDoContext _db;
        public ToDosController(ToDoContext db)
        {
            _db = db;
        }

        [HttpPost]
        public IActionResult AddToDo(AddToDoRequest request)
        {
            var entity = new ToDoEntity()
            {
                Title = request.Title,
                Content = request.Content
            };

            _db.ToDos.Add(entity);

            try
            {
                _db.SaveChanges();
                return Ok(); // StatusCode(200)
            }
            catch (Exception)
            {
                return StatusCode(500); // Server Error
            }
        }

        [HttpGet]
        public IActionResult GetAllToDos()
        {
            var entites = _db.ToDos.ToList();

            return Ok(entites);

            // Toplu çekimlerde genelde NotFound dönülmez. - Boş Json dönmesi tercih edilir.
            // Yine de dönmek istenirse:
            // if (entites.Count == 0)
            // return NotFound();
            // else
            // return Ok(entities);
        }

        [HttpGet("{id}")]
        public IActionResult GetToDo(int id)
        {
            var entity = _db.ToDos.Find(id);

            if (entity is null)
                return NotFound(); // StatusCodes(404); BULAMAMA KODU

            return Ok(entity);
        }

        [HttpPatch("{id}")]
        public IActionResult CheckToDo(int id)
        {
            var entity = _db.ToDos.Find(id);

            if (entity is null)
                return NotFound();

            entity.IsDone = !entity.IsDone;
            _db.ToDos.Update(entity);

            try
            {
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateToDo(int id, UpdateToDoRequest request)
        {
            var entity = _db.ToDos.Find(id);

            if(entity is null)
                return NotFound(); // StatusCodes(404); Bulamama Kodu

            entity.Title = request.Title;
            entity.Content = request.Content;

            _db.ToDos.Update(entity);

            try
            {
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            var entity = _db.ToDos.Find(id);

            if (entity is null)
                return BadRequest(); // Olmayan veri silinmek istenirse BadRequest(400) dönüyorum.

            _db.ToDos.Remove(entity); // Hard Delete

            try
            {
                _db.SaveChanges();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
