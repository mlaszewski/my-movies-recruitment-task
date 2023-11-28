using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyMoviesAPI.Services.MovieService;

namespace MyMoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Movie>>> GetAllMovies()
        {
            return Ok(await _movieService.GetAllMovies());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetSingleMovie(int id)
        {
            var result = await _movieService.GetSingleMovie(id);

            if (result is null)
                return NotFound("Movie not found.");

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<List<Movie>>> AddMovie(Movie movie)
        {
            var result = await _movieService.AddMovie(movie);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Movie>>> UpdateMovie(int id, Movie request)
        {
            var result = await _movieService.UpdateMovie(id, request);

            if (result is null)
                return NotFound("Movie not found.");

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Movie>>> DeleteMovie(int id)
        {
            var result = await _movieService.DeleteMovie(id);

            if (result is null)
                return NotFound("Movie not found.");

            return Ok(result);
        }
    }
}
