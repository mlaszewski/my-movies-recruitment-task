
namespace MyMoviesAPI.Services.MovieService
{
    public class MovieService : IMovieService
    {
        private static List<Movie> movies = new List<Movie>
            {
                new Movie { Id = 1, Title = "The Matrix", Director = "The Wachowski Brothers", YearOfRelease =  1997 },
                new Movie { Id = 2, Title = "The Godfather", Director = "Francis Ford Coppola", YearOfRelease =  1997 },
                new Movie { Id = 3, Title = "The Dark Knight", Director = "Christopher Nolan", YearOfRelease =  1997 }
            };

        public List<Movie> GetAllMovies()
        {
            return movies;
        }

        public Movie GetSingleMovie(int id)
        {
            var movie = movies.Find(m => m.Id == id);

            if (movie is null)
                return null;

            return movie;
        }

        public List<Movie> AddMovie(Movie movie)
        {
            movies.Add(movie);
            return movies;
        }

        public List<Movie>? UpdateMovie(int id, Movie request)
        {
            var movie = movies.Find(m => m.Id == id);

            if (movie is null)
                return null;

            movie.Title = request.Title;
            movie.Director = request.Director;
            movie.YearOfRelease = request.YearOfRelease;

            return movies;
        }

        public List<Movie>? DeleteMovie(int id)
        {
            var movie = movies.Find(m => m.Id == id);

            if (movie is null)
                return null;

            movies.Remove(movie);
            return movies;
        }
    }
}
