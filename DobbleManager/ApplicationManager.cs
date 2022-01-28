namespace DobbleManager;

public class ApplicationManager : IApplicationManager
{
    private const int GAME_ID_LENGTH = 3;
    private const int MAX_GAME_LIFETIME_IN_HOURS = 1;
    private readonly int[] VALID_PICTURES_PER_CARD = { 3, 4, 6, 8, 12, 14, 18, 20 };
    private readonly TimeSpan WAITING_TIME = TimeSpan.FromMilliseconds(333); // latence max entre l'équipement du joueur le plus rapide et le joueur le moins rapide

    private readonly ILogger<ApplicationManager> _logger;

    public ApplicationManager(ILogger<ApplicationManager> logger) => _logger = logger;

    private Dictionary<(string gameId, string cardId), (DateTime receiveTime, int touchDelay)> CardManageTime { get; } = new Dictionary<(string, string), (DateTime, int)>();

    public Dictionary<string, GameManager> GameManagers { get; } = new Dictionary<string, GameManager>();

    public void FreeGameManager(string gameId) => GameManagers.Remove(gameId);

    public int JoinGameManager(string gameId) => GameManagers.ContainsKey(gameId) ? GameManagers[gameId].PicturesPerCard : 0;

    public string CreateGameManager(int picturesPerCard, List<string> picturesNames)
    {
        if (!VALID_PICTURES_PER_CARD.Contains(picturesPerCard)) return string.Empty;
        FreeOldGameManagers(TimeSpan.FromHours(MAX_GAME_LIFETIME_IN_HOURS));
        string gameId;
        do gameId = RandomId();
        while (GameManagers.ContainsKey(gameId));
        GameManagers.Add(gameId, new GameManager(_logger, picturesPerCard, picturesNames));
        return gameId;
    }

    /// <summary>
    /// Gestion du touch (ou clic) sur une image avec prise en compte de la latence réseau 
    /// </summary>
    /// <param name="gameId">identifiant de la partie</param>
    /// <param name="playerGuid">identifiant du joueur</param>
    /// <param name="cardPlayed">carte du joueur</param>
    /// <param name="pictureId">identifiant de l'image touchée</param>
    /// <param name="touchDelay">temps mis par le joueur pour toucher l'image après l'avoir eu affichée sur son ecran</param>
    /// <returns></returns>
    public TouchResponse Touch(string gameId, string playerGuid, DobbleCard cardPlayed, int pictureId, int touchDelay)
    {
        var touchReceiveTime = DateTime.Now;
        DobbleCard centerCard;
        TimeSpan timeToSleep = WAITING_TIME;
        lock (GameManagers[gameId].GameManagerLock)
        {
            centerCard = GameManagers[gameId].CenterCard;
            if (cardPlayed != GameManagers[gameId].GetCurrentCard(playerGuid)) return new TouchResponse(TouchStatus.CardPlayedDontExist);
            if (!centerCard.PicturesIds.Any(id => id == pictureId)) return new TouchResponse(TouchStatus.WrongValueTouch);

            var firstTouch = CardManageTime.TryAdd((gameId, centerCard.ToString()), (touchReceiveTime, touchDelay));
            if (!firstTouch)
            {
                _logger.LogInformation($"-- Touch After - player {playerGuid} TouchReceiveTime : {touchReceiveTime:hh:mm:ss.fff} - TouchDelay {touchDelay}");
                timeToSleep = TimeSpan.MinValue;
                var firstTouchReceiveTime = CardManageTime[(gameId, centerCard.ToString())].receiveTime;
                var firstPlayerTouchDelay = CardManageTime[(gameId, centerCard.ToString())].touchDelay;

#if DEBUG
                if (touchReceiveTime - firstTouchReceiveTime <= WAITING_TIME && touchDelay < firstPlayerTouchDelay)
                {
                    _logger.LogInformation($"-- Call faster");
                    touchDelay = 10;
                    CardManageTime[(gameId, centerCard.ToString())] = (touchReceiveTime, touchDelay);
                }
                else
                {
                    _logger.LogInformation($"-- Call slower");
                }
#else
                    if (touchReceiveTime - firstTouchReceiveTime <= WAITING_TIME && touchDelay < firstPlayerTouchDelay)
                    {
                        CardManageTime[(gameId, centerCard.ToString())] = (touchReceiveTime, touchDelay);
                    }
#endif                 
            }
#if DEBUG
            else
            {
                _logger.LogInformation($"-- Touch First - player {playerGuid} TouchReceiveTime : {touchReceiveTime:hh:mm:ss.fff} - TouchDelay {touchDelay}");
            }
#endif
        }
        // synchronisation de tous les appels
        if (timeToSleep == WAITING_TIME)
            lock (GameManagers[gameId].SynchronisePlayersTouch)
                Thread.Sleep(timeToSleep);
        else
            lock (GameManagers[gameId].SynchronisePlayersTouch)
                _ = 0;

        //retardement des appels par les joueurs les - rapides pour donner priorité au joueur le + rapide
        if (CardManageTime[(gameId, centerCard.ToString())] != (touchReceiveTime, touchDelay))
            Thread.Sleep(5);

        lock (GameManagers[gameId].SynchronisePlayersTouch)
        {
            centerCard = GameManagers[gameId].CenterCard;
            CardManageTime.Remove((gameId, centerCard.ToString()));
            if (cardPlayed != GameManagers[gameId].GetCurrentCard(playerGuid)) return new TouchResponse(TouchStatus.CardPlayedDontExist);
            if (!centerCard.PicturesIds.Any(id => id == pictureId)) return new TouchResponse(TouchStatus.WrongValueTouch);

            GameManagers[gameId].CenterCard = cardPlayed;
            if (GameManagers[gameId].IncreaseCardsCurrentIndex(playerGuid))
            {
                FreeGameManager(gameId);
                return new TouchResponse(TouchStatus.TouchOkAndGameFinish, cardPlayed);
            }
            return new TouchResponse(TouchStatus.TouchOk, cardPlayed);
        }
    }

    private static string RandomId()
    {
        const string src = "ABCDEFGHJKLMNPQRSTUVWXYZ123456789";
        var Rand = new Random();
        var sb = new StringBuilder();
        for (var i = 0; i < GAME_ID_LENGTH; i++)
            sb.Append(src[Rand.Next(0, src.Length)]);
        return sb.ToString();
    }

    private void FreeOldGameManagers(TimeSpan maxLifetime) => GameManagers.Where(gm => DateTime.Now - gm.Value.CreateDate > maxLifetime).Select(gm => gm.Key).ToList().ForEach(id => FreeGameManager(id));
}