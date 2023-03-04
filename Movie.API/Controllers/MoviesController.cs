using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using System.Data.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.API.Data;
using Movies.API.Models;
using Common;
using Constant = Common.Constant;

namespace Movies.API.Controllers
{
    [Route(Constant.Movies_Api_Route_Name)]
    [ApiController]
   // [Authorize(Constant.Client_Id_Policy)]
    public class MoviesController : ControllerBase
    {
        private readonly MoviesContext _context;

        public MoviesController(MoviesContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        [HttpGet(Constant.Movies_Api_Route_Id)]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            return movie;
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);

            await _context.SaveChangesAsync();

            return CreatedAtAction(Constant.Movies_Api_Action_Get_Movie, new { id = movie.Id }, movie);
        }

        [HttpPut(Constant.Movies_Api_Route_Id)]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Edited Successfully");
        }

        [HttpDelete(Constant.Movies_Api_Route_Id)]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return Ok("Deleted");
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}