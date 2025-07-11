using Microsoft.AspNetCore.Mvc;
using PetProject.Models;
using System.Text.Json;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoAsyncController : ControllerBase
    {
        private const string USER_FILE_PATH = "data/users.json";
        private const string LOCATIONS_FILE_PATH = "data/locations.json";
        private const string GAMES_FILE_PATH = "data/games.json";

        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            var userId = await GetRandomUserIdAsync();
            var location = GetUserLocationsAsync(userId);
            var game = GetUserFavoriteGameAsync(userId);

            await Task.WhenAll(location, game);

            return Ok(new { userId, location, game });
        }

        async Task<int> GetRandomUserIdAsync()
        {
            Console.WriteLine("Получение пользователя");
            var userJson = await System.IO.File.ReadAllTextAsync(USER_FILE_PATH);
            // Дойдя до await, текущий поток освобождается и переходит наверх в вызывающий метод, а не идет дальше по коду
            await Task.Delay(1000);
            var userData = JsonSerializer.Deserialize<UserData>(userJson);
            Console.WriteLine("Пользователь получен");
            return userData.Users.First().Id;
        }

        async Task<string> GetUserLocationsAsync(int userId)
        {
            Console.WriteLine("Получение локации пользователя");
            var locationJson = await System.IO.File.ReadAllTextAsync(LOCATIONS_FILE_PATH);
            await Task.Delay(3000);
            var locationData = JsonSerializer.Deserialize<LocationData>(locationJson);
            Console.WriteLine("Локации получены");
            return locationData.Locations.First(x => x.UserId == userId).LocationName;
        }

        async Task<string> GetUserFavoriteGameAsync(int userId)
        {
            Console.WriteLine("Получение любимой игры пользователя");
            var gamesJson = await System.IO.File.ReadAllTextAsync(GAMES_FILE_PATH);
            await Task.Delay(3000);
            var gamesData = JsonSerializer.Deserialize<GameData>(gamesJson);
            Console.WriteLine("Игры получены");
            return gamesData.Games.First(x => x.UserId == userId).FavoriteGame;
        }
    }
}
