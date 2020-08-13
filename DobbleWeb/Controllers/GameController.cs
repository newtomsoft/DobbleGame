using DobbleManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace DobblePOC.Controllers
{
    public class GameController : Controller
    {
        private IApplicationManager ApplicationManager { get; }
        private IWebHostEnvironment WebHostEnvironment { get; }

        public GameController(IApplicationManager applicationManager, IWebHostEnvironment webHostEnvironment)
        {
            ApplicationManager = applicationManager;
            WebHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
            => View();

        [HttpPost]
        public IActionResult Create(int picturesPerCard, string playerId)
        {
            var picturesNames = GetRandomPicturesNames(picturesPerCard * picturesPerCard - picturesPerCard + 1);
            string gameId = ApplicationManager.CreateGameManager(picturesPerCard, picturesNames);
            if (gameId == string.Empty)
                return new BadRequestObjectResult(new { error = $"Le nombre d'images par carte {picturesPerCard} n'est pas valide !" });
            var playerAdded = AddNewPlayer(gameId, playerId);
            return new JsonResult(new { gameId, playerAdded, picturesPerCard, picturesNames });
        }

        [HttpPost]
        public IActionResult Join(string gameId, string playerId)
        {
            int picturesPerCard = ApplicationManager.JoinGameManager(gameId);
            var picturesNames = ApplicationManager.GameManagers[gameId].PicturesNames;
            if (picturesPerCard == 0)
                return new BadRequestObjectResult(new { error = $"La partie d'id {gameId} n'existe pas !" });
            var playerAdded = AddNewPlayer(gameId, playerId);
            return new JsonResult(new { gameId, playerAdded, picturesPerCard, picturesNames });
        }

        [HttpPost]
        public JsonResult JoinAsAdditionalDevice(string gameId)
        {
            int picturesPerCard = ApplicationManager.JoinGameManager(gameId);
            var picturesNames = ApplicationManager.GameManagers[gameId].PicturesNames;
            string additionalDevice = ApplicationManager.GameManagers[gameId].GetNewDevice();
            return new JsonResult(new { additionalDevice, picturesPerCard, picturesNames });
        }

        [HttpPost]
        public JsonResult Start(string gameId)
        {
            ApplicationManager.GameManagers[gameId].DistributeCards();
            var centerCard = ApplicationManager.GameManagers[gameId].CenterCard;
            var picturesNames = ApplicationManager.GameManagers[gameId].PicturesNames;
            return new JsonResult(new { centerCard, picturesNames });
        }

        [HttpGet]
        public JsonResult GetCardsPlayer(string gameId, string playerId)
            => new JsonResult(ApplicationManager.GameManagers[gameId].GetCards(playerId));

        [HttpGet]
        public JsonResult GetCenterCard(string gameId)
            => new JsonResult(ApplicationManager.GameManagers[gameId].CenterCard);

        [HttpPost]
        public JsonResult Touch(string gameId, string playerId, DobbleCard cardPlayed, int valueTouch, TimeSpan timeTakenToTouch)
            => new JsonResult(ApplicationManager.Touch(gameId, playerId, cardPlayed, valueTouch, timeTakenToTouch));

        private List<string> GetRandomPicturesNames(int picturesNumber)
        {
            var fullNames = Directory.GetFiles(Path.Combine(WebHostEnvironment.WebRootPath, "pictures", "cardPictures")).OrderBy(_ => Guid.NewGuid()).Take(picturesNumber).ToList();
            return fullNames.Select(fullName => Path.GetFileName(fullName)).ToList();
        }

        private bool AddNewPlayer(string gameId, string playerId)
            //=> ApplicationManager.GameManagers[gameId].GetNewPlayer();
            => ApplicationManager.GameManagers[gameId].AddNewPlayer(playerId);
    }
}
