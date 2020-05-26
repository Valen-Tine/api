using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using proanimafilms_api.Models;
using proanimafilms_api.Services;

namespace proanimafilms_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class proanimaFilmsController : ControllerBase
    {
        HttpClient client = new HttpClient();

        private readonly FilmService _filmService;

        public proanimaFilmsController(FilmService filmService)
        {
            _filmService = filmService;
        }

        public string key = "&api_key=24f9ae38246fbb20936517d9af12c71e";
        public string language = "&language=ru-RU";

        // GET: api/proanimaFilms/movie_id
        [HttpGet("{movie_id}", Name = "Get_movie")] //Поиск фильма по названию(id)
        public async Task<string> Get_movie(string movie_id)
        {
            string q = $"https://api.themoviedb.org/3/search/movie?query=" + movie_id + key + language;
            HttpResponseMessage response = await client.GetAsync(q);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        //GET: api/proanimaFilms/tvshow/tvshows_id
        [HttpGet("tvshow/{tvshows_id}", Name = "Get_tvshows")] //Поиск сериала по названию(id)
        public async Task<string> Get_tvshows(string tvshows_id)
        {
            string q = $"https://api.themoviedb.org/3/search/tv?query=" + tvshows_id + key + language;
            HttpResponseMessage response = await client.GetAsync(q);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        //GET: api/proanimaFilms/popular
        [HttpGet("popular", Name = "Get_popular")] //Поиск фильма по названию(id)
        public async Task<string> Get_popular(string movie_id)
        {
            string q = $"https://api.themoviedb.org/3/movie/popular?" + key + language;
            HttpResponseMessage response = await client.GetAsync(q);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        //GET: api/proanimaFilms/genre/{genre}
        [HttpGet("genre/{genre}", Name = "Get_movies_by_genre")] //Поиск фильма по жанру
        public async Task<string> Get_movies_by_genre(int genre)
        {
            string q = $"https://api.themoviedb.org/3/discover/movie?with_genres=" + genre + key + language;
            HttpResponseMessage response = await client.GetAsync(q);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            return responseBody;
        }

        // GET: api/proanimaFilms/get/the/entire/database
        [HttpGet("get/the/entire/database")] //Получить всю базу данных
        public ActionResult<List<FeaturedFims>> Get() =>
            _filmService.Get();

        // GET: api/proanimaFilms/featured/films/{id}
        [HttpGet("featured/films/{id}", Name = "Get_favorites")] //Получить данные определенного пользователя
        public ActionResult<FeaturedFims> Get(int id)
        {
            var film = _filmService.Get(id);

            if (film == null)
            {
                return NotFound();
            }
            return film;
        }

        // POST: api/proanimaFilms
        [HttpPost] //Добавить пользователя в базу данных
        public ActionResult<FeaturedFims> Create(FeaturedFims featuredFims)
        {
            _filmService.Create(featuredFims);
            return CreatedAtRoute("GetFilm", new { id = featuredFims._id}, featuredFims);
        }

        // PUT: api/proanimaFilms/{id}
        [HttpPut("{id}")] //Обновить информацию пользователя 
        public IActionResult Update(int id, FeaturedFims filmIn)
        {
            var film = _filmService.Get(id);

            if (film == null)
            {
                return NotFound();
            }

            _filmService.Update(id, filmIn);

            return NoContent();
        }
    }
}
