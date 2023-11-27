namespace MyMoviesAPI.Services.MovieService
{
    public interface IMovieService
    {
        List<Movie> GetAllMovies();
        Movie GetSingleMovie(int id);
        List<Movie> AddMovie(Movie movie);
        List<Movie>? UpdateMovie(int id, Movie request);
        List<Movie>? DeleteMovie(int id);
    }
}
