using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOs
{
    public class Movie
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Runtime { get; set; }

        public DateOnly ReleaseDate { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public int GenreId { get; set; }

        public int AudienceRatingId { get; set; }

        public Genre? Genre { get; set; }

        public AudienceRating? AudienceRating { get; set; }

        public List<Review> Reviews { get; set; } = new();
    }
}