using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NLSearchWeb.src.Models;

namespace NLSearchWeb.src.Data
{
    public class Seed
    {
        public static async Task SeedMovies(NLSDbContext context)
        {
            if (await context.Movies.AnyAsync()) return;

            var movieData = await File.ReadAllTextAsync("src/Data/MovieSeed.json");

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            var movies = JsonSerializer.Deserialize<List<Movie>>(movieData);

            foreach (var movie in movies)
            {
                Console.WriteLine(movie.Title);

                context.Movies.Add(movie);
            }

            await context.SaveChangesAsync();
        }
    }
}