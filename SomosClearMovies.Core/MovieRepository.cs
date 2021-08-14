﻿using System;
using System.Linq;
using System.Collections.Generic;
using SomosClearMovies.Models.View;
using SomosClearMovies.Core.Interfaces;
using SomosClearMovies.Infrastructure.Interfaces;

namespace SomosClearMovies.Core
{
    /// <summary>
    /// Movie Repository
    /// </summary>
    public class MovieRepository : IMovieRepository
    {
        private readonly IMoviesDbContext _moviesDbContext;

        /// <summary>
        /// Create new instance of <see cref="MovieRepository"/> class.
        /// </summary>
        /// <param name="moviesDbContext"></param>
        public MovieRepository(IMoviesDbContext moviesDbContext)
        {
            _moviesDbContext = moviesDbContext ?? throw new ArgumentNullException(nameof(moviesDbContext));
        }

        /// <summary>
        /// Get Movies
        /// </summary>
        /// <param name="request">Get Movies Request</param>
        /// <returns>A List of <see cref="MovieDetailed"/></returns>
        public IEnumerable<MovieDetailed> GetMovies(GetMoviesRequest request)
        {
            var result = _moviesDbContext.GetMovies(request.MovieTitle, request.MovieGenere, request.ActorName)
                .GroupBy(movie => movie.IdMovie)
                .Select(movie => new MovieDetailed
                {
                    Title = movie.FirstOrDefault().Movie.Title,
                    Genere = movie.FirstOrDefault().Movie.Genere,
                    Actors = movie.Select(x => x.Actor.Name)
                });

            return result;
        }
    }
}
