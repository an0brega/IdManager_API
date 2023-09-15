using MeuTodo.Data;
using MeuTodo.Models;
using MeuTodo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MeuTodo.Controllers
{
    [ApiController]
    [Route("v1")] //Creates a route prefix
    public class Controller : ControllerBase
    {
        /// <summary>
        /// This method will get something
        /// and it is async
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        [HttpGet] //If I don't explicity define the method(verb) here, the default is "Get"
        [Route("todo")] //In this case, the URL is "v1/todos"
        public async Task<IActionResult> GetAsync(
            [FromServices] AppDbContext context) //calling the DbContext created in the Startup
        {
            var todos = await context
                .Todos
                .AsNoTracking() // it will not track nothing from this query
                .ToListAsync();
            return Ok(todos);
        }

        [HttpGet]
        [Route("todo/{id}")] //In this case, the URL is "v1/todos/{id}"
        public async Task<IActionResult> GetByIdAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id) //[FromRoute] is letting explicit that the parameter comes from the route
        {
            var todo = await context
                .Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id); //bring the id I want or a default one
            return todo == null // if
                ? NotFound() //result of the if
                : Ok(todo); // else
        }

        [HttpPost("todo")]
        public async Task<IActionResult> PostAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateIdViewModel model)
        {
            if (!ModelState.IsValid) //this ModelState apply the validations on the 'model'
                return BadRequest();

            var todo = new IdModel
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };

            try
            {
                await context.Todos.AddAsync(todo); //creating the todo in the memory
                await context.SaveChangesAsync(); //saving on the database

                return Created($"v1/todo/{todo.Id}", todo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpPut("todo/{id}")]
        public async Task<IActionResult> PutAsync(
            [FromServices] AppDbContext context,
            [FromBody] CreateIdViewModel model,
            [FromRoute] int id)
        {
            if (!ModelState.IsValid) //this ModelState apply the validations on the 'model'
                return BadRequest();

            var todo = await context.
                Todos.
                FirstOrDefaultAsync(x => x.Id == id);

            if (todo == null)
                return NotFound();

            try
            {
                todo.Title = model.Title;

                context.Todos.Update(todo);
                await context.SaveChangesAsync();

                return Ok(todo);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpDelete("todo/{id}")]
        public async Task<IActionResult> DeleteAsync(
            [FromServices] AppDbContext context,
            [FromRoute] int id)
        {
            var todo = await context.
                Todos.
                FirstOrDefaultAsync(x => x.Id == id);

            try
            {
                context.Todos.Remove(todo);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (Exception)
            {

                return BadRequest(); // when something "bad" comes from the client, not necessarily from server
            }



        }
    }
}