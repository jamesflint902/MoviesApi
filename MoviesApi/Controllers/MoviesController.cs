using Microsoft.AspNetCore.Mvc;
using MoviesApi.DTOs;
using System.Data;
using System.Reflection;
using Dapper;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {

        private readonly IDbConnection _db;



        // Constructor
        public MoviesController(IDbConnection db)
        {
            _db = db;
            //initialize dependencies/services here

        }

        // GET: api/movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
        {
            var sql = @"
                SELECT m.*,g.*,ar.*

                FROM Movie m

                LEFT JOIN Genres g
                    ON m.GenreId = g.Id

                LEFT JOIN AudienceRatings ar
                    ON m.AudienceRatingId = ar.Id";

            var movies = await _db.QueryAsync<Movie, Genre, AudienceRating, Movie>(
                sql,
                (movie, genre, audienceRating) =>
                {
                    movie.Genre = genre;
                    movie.AudienceRating = audienceRating;

                    return movie;
                },
                splitOn: "Id,Id"
            );

            return Ok(movies);
        }



        // GET: api/movies/id#
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Movie>> GetById(int id)
        {
            var sql = @"
                SELECT m.*, g.*, ar.*, r.*, u.*

                FROM Movie m

                LEFT JOIN Genres g
                    ON m.GenreId = g.Id

                LEFT JOIN AudienceRatings ar
                    ON m.AudienceRatingId = ar.Id

                LEFT JOIN Reviews r
                    ON r.MovieId = m.Id
                    AND r.IsPublished = 1

                LEFT JOIN UserSnapshot u
                    ON u.UserId = r.CreatedBy

                WHERE m.Id = @Id";

            Movie? movie = null;

            await _db.QueryAsync<Movie, Genre, AudienceRating, Review, User, Movie>(
                sql,
                (m, g, ar, r, u) =>
                {
                    if (movie == null)
                    {
                        movie = m;
                        movie.Genre = g;
                        movie.AudienceRating = ar;
                        movie.Reviews = new List<Review>();
                    }

                    if (r != null)
                    {
                        r.User = u;
                        movie.Reviews.Add(r);
                    }

                    return movie;
                },
                new { Id = id },
                splitOn: "Id,Id,Id,UserId"
            );

            return Ok(movie);
        }
    }
}