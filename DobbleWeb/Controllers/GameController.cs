namespace DobbleWeb.Controllers;

public class GameController : Controller
{
    private readonly ILogger<GameController> _logger;
    private readonly IApplicationManager _applicationManager;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GameController(ILogger<GameController> logger, IApplicationManager applicationManager, IWebHostEnvironment webHostEnvironment)
    {
        _logger = logger;
        _applicationManager = applicationManager;
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost]
    public IActionResult Create(int picturesPerCard, string playerId)
    {
        var picturesNames = GetRandomPicturesNames(picturesPerCard * picturesPerCard - picturesPerCard + 1);
        var gameId = _applicationManager.CreateGameManager(picturesPerCard, picturesNames);
        if (gameId == string.Empty) return new BadRequestObjectResult(new { error = $"Le nombre d'images par carte {picturesPerCard} n'est pas valide !" });
        _logger.LogInformation($"New game to create {gameId} by playerId {playerId} with {picturesPerCard} pictures per card");
        var playerAdded = AddNewPlayer(gameId, playerId);
        _logger.LogInformation($"New game created {gameId} by playerId {playerId} with {picturesPerCard} pictures per card");
        return new JsonResult(new { gameId, playerAdded, picturesPerCard, picturesNames });
    }

    [HttpPost]
    public IActionResult Join(string gameId, string playerId)
    {
        var picturesPerCard = _applicationManager.JoinGameManager(gameId);
        var picturesNames = _applicationManager.GameManagers[gameId].PicturesNames;
        if (picturesPerCard == 0) return new BadRequestObjectResult(new { error = $"La partie d'id {gameId} n'existe pas !" });
        var playerAdded = AddNewPlayer(gameId, playerId);
        return new JsonResult(new { gameId, playerAdded, picturesPerCard, picturesNames });
    }

    [HttpPost]
    public JsonResult JoinAsAdditionalDevice(string gameId)
    {
        var picturesPerCard = _applicationManager.JoinGameManager(gameId);
        var picturesNames = _applicationManager.GameManagers[gameId].PicturesNames;
        var additionalDevice = _applicationManager.GameManagers[gameId].GetNewDevice();
        return new JsonResult(new { additionalDevice, picturesPerCard, picturesNames });
    }

    [HttpPost]
    public JsonResult Start(string gameId)
    {
        _applicationManager.GameManagers[gameId].DistributeCards();
        var centerCard = _applicationManager.GameManagers[gameId].CenterCard;
        var picturesNames = _applicationManager.GameManagers[gameId].PicturesNames;
        return new JsonResult(new { centerCard, picturesNames });
    }

    [HttpGet]
    public JsonResult GetCardsPlayer(string gameId, string playerId) => new(_applicationManager.GameManagers[gameId].GetCards(playerId));

    [HttpGet]
    public JsonResult GetCenterCard(string gameId) => new(_applicationManager.GameManagers[gameId].CenterCard);

    [HttpPost]
    public JsonResult Touch(string gameId, string playerId, DobbleCard cardPlayed, int pictureId, int touchDelay) => new(_applicationManager.Touch(gameId, playerId, cardPlayed, pictureId, touchDelay));

    private List<string> GetRandomPicturesNames(int picturesNumber) => Directory.GetFiles(Path.Combine(_webHostEnvironment.WebRootPath, "pictures", "cardPictures")).OrderBy(_ => Guid.NewGuid()).Take(picturesNumber).Select(Path.GetFileName).ToList();
    private bool AddNewPlayer(string gameId, string playerId) => _applicationManager.GameManagers[gameId].AddNewPlayer(playerId);
}