using Microsoft.EntityFrameworkCore;

namespace MyMoviesAPI.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private readonly DataContext _context;
        public MovieService(DataContext context) 
        {
            _context = context;
        }

        public async Task<List<Movie>> GetAllMovies()
        {
            var movies = await _context.Movies.ToListAsync();
            return movies;
        }

        public async Task<Movie?> GetSingleMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie is null)
                return null;

            return movie;
        }

        public async Task<List<Movie>> AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();
            return await GetAllMovies();
        }

        public async Task<List<Movie>?> UpdateMovie(int id, Movie request)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie is null)
                return null;

            movie.Title = request.Title;
            movie.Director = request.Director;
            movie.YearOfRelease = request.YearOfRelease;

            await _context.SaveChangesAsync();

            return await GetAllMovies();
        }

        public async Task<List<Movie>?> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (movie is null)
                return null;

            _context.Movies.Remove(movie);

            await _context.SaveChangesAsync();

            return await GetAllMovies();
        }
    }
}
