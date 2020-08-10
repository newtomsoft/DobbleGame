using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DobbleManager
{
    public class DobbleCardsGame
    {
        public List<DobbleCard> Cards { get; private set; }

        public DobbleCardsGame(int picturesPerCardNumber)
        {
            GenerateAllCards(picturesPerCardNumber);
            ShuffleCards();
            ShufflePicturesCards();
        }

        private void ShufflePicturesCards()
        {
            foreach (var card in Cards)
                card.ShufflePictures();
        }

        private void ShuffleCards() => Cards = Cards.OrderBy(_ => Guid.NewGuid()).ToList();

        private void GenerateAllCards(int picturesPerCardNumber) => Cards = GenerateCardsWithSameFirstPicture(picturesPerCardNumber, picturesPerCardNumber - 1);

        private List<DobbleCard> GenerateCardsWithSameFirstPicture(int picturesNumber, int firstPicture)
        {
            var dobbleCards = new List<DobbleCard>();
            if (picturesNumber <= 0) return dobbleCards;

            if (firstPicture == 0)
            {
                for (int i = 0; i < picturesNumber; i++)
                    dobbleCards.Add(new DobbleCard(0));

                int iPicture = 0;
                for (int iCard = 0; iCard < picturesNumber; iCard++)
                {
                    for (int i = 1; i < picturesNumber; i++)
                    {
                        iPicture++;
                        dobbleCards[iCard].PicturesIds.Add(iPicture);
                    }
                }
                return dobbleCards;
            }

            List<DobbleCard> referenceCards;
            if (firstPicture == 1)
            {
                referenceCards = GenerateCardsWithSameFirstPicture(picturesNumber, firstPicture - 1);
                for (int i = 0; i < picturesNumber - 1; i++)
                    dobbleCards.Add(new DobbleCard(firstPicture));

                for (int iCard = 0; iCard < picturesNumber - 1; iCard++)
                    for (int iPicture = 0; iPicture < picturesNumber - 1; iPicture++)
                        dobbleCards[iCard].PicturesIds.Add(referenceCards[iPicture + 1].PicturesIds[iCard + 1]);

                dobbleCards.AddRange(referenceCards);
                return dobbleCards;
            }

            referenceCards = GenerateCardsWithSameFirstPicture(picturesNumber, firstPicture - 1);
            for (int i = 0; i < picturesNumber - 1; i++)
                dobbleCards.Add(new DobbleCard(firstPicture));

            for (int iCard = 0; iCard < picturesNumber - 1; iCard++)
                for (int iPicture = 0; iPicture < picturesNumber - 1; iPicture++)
                    dobbleCards[iCard].PicturesIds.Add(referenceCards[(iCard + iPicture) % (picturesNumber - 1)].PicturesIds[iPicture + 1]);

            dobbleCards.AddRange(referenceCards);
            return dobbleCards;
        }

        public override string ToString()
        {
            int picturesPerCardNumber = Cards[0].PicturesIds.Count;
            const string separator = "   ";
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < Cards.Count - picturesPerCardNumber; i++)
            {
                stringBuilder.Append(Cards[i].ToString());
                if ((i + 1) % (picturesPerCardNumber - 1) == 0)
                    stringBuilder.Append("\n");
                else
                    stringBuilder.Append(separator);
            }
            stringBuilder.Remove(stringBuilder.Length - "\n".Length, "\n".Length);
            stringBuilder.Append("\n");
            for (int i = Cards.Count - picturesPerCardNumber; i < Cards.Count; i++)
            {
                stringBuilder.Append(Cards[i].ToString());
                stringBuilder.Append(separator);
            }
            return stringBuilder.ToString();
        }
    }
}
