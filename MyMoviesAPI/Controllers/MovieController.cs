using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMoviesAPI.Models;

namespace MyMoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private static List<Movie> movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "The Matrix", Director = "The Wachowski Brothers", YearOfRelease =  1997 },
                new Movie { Id = 2, Title = "The Godfather", Director = "Francis Ford Coppola", YearOfRelease =  1997 },
                new Movie { Id = 3, Title = "The Dark Knight", Director = "Christopher Nolan", YearOfRelease =  1997 }
            };

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetAllMovies()
        {
            return Ok(movies);
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetSingleMovie(int id)
        {
            var movie = movies.Find(m => m.Id == id);

            if (movie == null)
            {
                return NotFound("Movie doesn't exist in our database");
            }

            return Ok(movie);
        }
        
        [HttpPost]
        public async Task<ActionResult<List<Movie>>> AddMovie(Movie movie)
        {
            movies.Add(movie);
            return Ok(movies);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Movie>>> UpdateMovie(int id, Movie request)
        {
            var movie = movies.Find(m => m.Id == id);

            if (movie == null)
                return NotFound("Movie doesn't exist in our database");

            movie.Title = request.Title;
            movie.Director = request.Director;  
            movie.YearOfRelease = request.YearOfRelease;

            return Ok(movies);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Movie>>> DeleteMovie(int id)
        {
            var movie = movies.Find(m => m.Id == id);

            if (movie == null)
                return NotFound("Movie doesn't exist in our database");

            movies.Remove(movie);
            return Ok(movies);
        }
    }
}
