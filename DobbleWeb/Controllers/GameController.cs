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
        public IActionResult Create(int picturesPerCard)
        {
            var picturesNames = GetRandomPicturesNames(picturesPerCard * picturesPerCard - picturesPerCard + 1);
            string gameId = ApplicationManager.CreateGameManager(picturesPerCard, picturesNames);
            if (gameId == string.Empty)
                return new BadRequestObjectResult(new { error = $"Le nombre d'images par carte {picturesPerCard} n'est pas valide !" });
            string playerGuid = AddNewPlayer(gameId);
            return new JsonResult(new { gameId, playerGuid, picturesPerCard });
        }

        [HttpPost]
        public IActionResult Join(string gameId)
        {
            int picturesPerCard = ApplicationManager.JoinGameManager(gameId);
            if (picturesPerCard == 0)
                return new BadRequestObjectResult(new { error = $"La partie d'id {gameId} n'existe pas !" });
            string playerGuid = AddNewPlayer(gameId);
            return new JsonResult(new { gameId, playerGuid, picturesPerCard });
        }

        [HttpPost]
        public JsonResult JoinAsAdditionalDevice(string gameId)
        {
            int picturesPerCard = ApplicationManager.JoinGameManager(gameId);
            string additionalDevice = ApplicationManager.GameManagers[gameId].GetNewDevice();
            return new JsonResult(new { additionalDevice, picturesPerCard });
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
        public JsonResult GetCardsPlayer(string gameId, string playerGuid)
            => new JsonResult(ApplicationManager.GameManagers[gameId].GetCards(playerGuid));

        [HttpGet]
        public JsonResult GetCenterCard(string gameId)
            => new JsonResult(ApplicationManager.GameManagers[gameId].CenterCard);

        [HttpPost]
        public JsonResult Touch(string gameId, string playerGuid, DobbleCard cardPlayed, int valueTouch, DobbleCard centerCard, TimeSpan timeTakenToTouch)
            => new JsonResult(ApplicationManager.Touch(gameId, playerGuid, cardPlayed, valueTouch, centerCard, timeTakenToTouch));

        private List<string> GetRandomPicturesNames(int picturesNumber)
        {
            var fullNames = Directory.GetFiles(Path.Combine(WebHostEnvironment.WebRootPath, "pictures", "cardPictures")).OrderBy(_ => Guid.NewGuid()).Take(picturesNumber).ToList();
            //var filesNames = new List<string>();
            //fullNames.ForEach(fullName => filesNames.Add(Path.GetFileName(fullName)));


            return fullNames.Select(fullName => Path.GetFileName(fullName)).ToList();

            //return filesNames;
        }

        private string AddNewPlayer(string gameId)
            => ApplicationManager.GameManagers[gameId].GetNewPlayer();
    }
}
