using Microsoft.AspNetCore.Mvc;
using PetProject.Models;
using System.Text.Json;

namespace PetProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserInfoAsyncOldController : ControllerBase
    {
        private const string USER_FILE_PATH = "data/users.json";
        private const string LOCATIONS_FILE_PATH = "data/locations.json";
        private const string GAMES_FILE_PATH = "data/games.json";

        [HttpGet("user-info")]
        public ActionResult GetUserInfo()
        {
            var userId = GetRandomUserId();
            return Ok(userId.ContinueWith(user =>
            {
                var location = GetUserLocations(user.Result);
                var game = GetUserFavoriteGame(user.Result);
                return new { userId.Result, location, game };
            }).Result);
        }

        Task<int> GetRandomUserId()
        {
            Console.WriteLine("Получение пользователя");
            var result = Task.Run(() =>
            {
                var userJsonTask = System.IO.File.ReadAllTextAsync(USER_FILE_PATH);
                Task.Delay(3000).Wait();
                return userJsonTask;
            });

            // Здесь можем выполнить другой код, пока мы ждем выполнение предыдущей таски.

            return result.ContinueWith(resultTask =>
            {
                var userData = JsonSerializer.Deserialize<UserData>(resultTask.Result);
                Console.WriteLine("Пользователь получен");
                return userData.Users.First().Id;
            });
        }

        string GetUserLocations(int userId)
        {
            Console.WriteLine("Получение локации пользователя");
            var locationJson = System.IO.File.ReadAllText(LOCATIONS_FILE_PATH);
            Task.Delay(3000).Wait();
            var locationData = JsonSerializer.Deserialize<LocationData>(locationJson);
            Console.WriteLine("Локации получены");
            return locationData.Locations.First(x => x.UserId == userId).LocationName;
        }

        string GetUserFavoriteGame(int userId)
        {
            Console.WriteLine("Получение любимой игры пользователя");
            var gamesJson = System.IO.File.ReadAllText(GAMES_FILE_PATH);
            Task.Delay(3000).Wait();
            var gamesData = JsonSerializer.Deserialize<GameData>(gamesJson);
            Console.WriteLine("Игры получены");
            return gamesData.Games.First(x => x.UserId == userId).FavoriteGame;
        }
    }
}
