using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DobbleManager
{
    public class ApplicationManager : IApplicationManager
    {
        private const int GAME_ID_LENGTH = 3;
        private const int MAX_GAME_LIFETIME_IN_HOURS = 1;
        private readonly int[] VALID_PICTURES_PER_CARD = { 3, 4, 6, 8, 12, 14, 18, 20 };

        public Dictionary<string, GameManager> GameManagers { get; } = new Dictionary<string, GameManager>();

        public void FreeGameManager(string gameId) => GameManagers.Remove(gameId);

        public int JoinGameManager(string gameId) => GameManagers.ContainsKey(gameId) ? GameManagers[gameId].PicturesPerCard : 0;

        public string CreateGameManager(int picturesPerCard, List<string> picturesNames)
        {
            if (!VALID_PICTURES_PER_CARD.Contains(picturesPerCard))
                return string.Empty;
            FreeOldGameManagers(new TimeSpan(MAX_GAME_LIFETIME_IN_HOURS, 0, 0));
            string gameId;
            do gameId = RandomId();
            while (GameManagers.ContainsKey(gameId));
            GameManagers.Add(gameId, new GameManager(picturesPerCard, picturesNames));
            return gameId;
        }

        public TouchResponse Touch(string gameId, string playerGuid, DobbleCard cardPlayed, int valueTouch, DobbleCard centerCard, TimeSpan timeTakenToTouch)
        {
            lock (GameManagers[gameId].GameManagerLock)
            {
                if (cardPlayed != GameManagers[gameId].GetCurrentCard(playerGuid))
                    return new TouchResponse(TouchStatus.CardPlayedDontExist);

                if (centerCard != GameManagers[gameId].CenterCard)
                    return new TouchResponse(TouchStatus.ToLate);

                if (centerCard.PicturesIds.Any(value => value == valueTouch))
                {
                    GameManagers[gameId].CenterCard = cardPlayed;
                    if (GameManagers[gameId].IncreaseCardsCurrentIndex(playerGuid))
                    {
                        FreeGameManager(gameId);
                        return new TouchResponse(TouchStatus.TouchOkAndGameFinish);
                    }
                    return new TouchResponse(TouchStatus.TouchOk, GameManagers[gameId].CenterCard);
                }
                return new TouchResponse(TouchStatus.WrongValueTouch);
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
}
