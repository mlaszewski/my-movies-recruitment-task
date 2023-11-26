﻿namespace MyMoviesAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int YearOfRelease { get; set; }
        public string Director { get; set; } = string.Empty;
    }
}
