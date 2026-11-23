using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MoviesApi.DTOs
{



        public class Review

        {
            // primary
            public int Id { get; set; }

            //foreign key to movies
      
            public int MovieId { get; set; }

            public string Description { get; set; } = string.Empty;

 
            public int Rating { get; set; }


            public bool IsPublished { get; set; }


            public string CreatedBy { get; set; } = string.Empty;


            public DateOnly CreatedDate { get; set; }


            public User? User { get; set; }

    }
    }




