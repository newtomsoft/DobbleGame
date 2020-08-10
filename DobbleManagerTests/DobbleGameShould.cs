using DobbleManager;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DobbleManagerTest
{
    public class GenerateAllCardsShould
    {
        [Fact]
        public void Have_all_values_in_each_card_present_in_each_other_card_only_1_time_when_03_values()
        {
            int valuesNumber = 3;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertAllValuesInEachCardPresentInEachOtherOnly1time(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Have_all_values_in_each_card_present_in_each_other_card_only_1_time_when_04_values()
        {
            int valuesNumber = 4;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertAllValuesInEachCardPresentInEachOtherOnly1time(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Have_all_values_in_each_card_present_in_each_other_card_only_1_time_when_06_values()
        {
            int valuesNumber = 6;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertAllValuesInEachCardPresentInEachOtherOnly1time(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Have_all_values_in_each_card_present_in_each_other_card_only_1_time_when_08_values()
        {
            int valuesNumber = 8;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertAllValuesInEachCardPresentInEachOtherOnly1time(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Have_all_values_in_each_card_present_in_each_other_card_only_1_time_when_12_values()
        {
            int valuesNumber = 12;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertAllValuesInEachCardPresentInEachOtherOnly1time(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Have_all_values_in_each_card_present_in_each_other_card_only_1_time_when_14_values()
        {
            int valuesNumber = 14;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertAllValuesInEachCardPresentInEachOtherOnly1time(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Count_007_cards_when_values_number_03()
        {
            int valuesNumber = 3;
            int expected = valuesNumber * valuesNumber - valuesNumber + 1;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            Assert.Equal(expected, DobbleCards.Count);
        }

        [Fact]
        public void Count_013_cards_when_values_number_04()
        {
            int valuesNumber = 4;
            int expected = valuesNumber * valuesNumber - valuesNumber + 1;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            Assert.Equal(expected, DobbleCards.Count);
        }

        [Fact]
        public void Count_031_cards_when_values_number_06()
        {
            int valuesNumber = 6;
            int expected = valuesNumber * valuesNumber - valuesNumber + 1;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            Assert.Equal(expected, DobbleCards.Count);
        }

        [Fact]
        public void Count_057_cards_when_values_number_08()
        {
            int valuesNumber = 8;
            int expected = valuesNumber * valuesNumber - valuesNumber + 1;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            Assert.Equal(expected, DobbleCards.Count);
        }

        [Fact]
        public void Count_133_cards_when_values_number_12()
        {
            int valuesNumber = 12;
            int expected = valuesNumber * valuesNumber - valuesNumber + 1;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            Assert.Equal(expected, DobbleCards.Count);
        }

        [Fact]
        public void Count_183_cards_when_values_number_14()
        {
            int valuesNumber = 14;
            int expected = valuesNumber * valuesNumber - valuesNumber + 1;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            Assert.Equal(expected, DobbleCards.Count);
        }

        [Fact]
        public void Count_8011_cards_when_values_number_98()
        {
            int valuesNumber = 98;
            int expected = valuesNumber * valuesNumber - valuesNumber + 1;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            Assert.Equal(expected, DobbleCards.Count);
        }

        [Fact]
        public void Have_each_values_present_in_03_cards_When_values_number_3()
        {
            int valuesNumber = 3;
            Dictionary<int, int> presence = PresenceOfEachValue(valuesNumber);
            foreach (var presenceOfValue in presence)
                Assert.Equal(valuesNumber, presenceOfValue.Value);
        }

        [Fact]
        public void Have_each_values_present_in_04_cards_when_values_number_4()
        {
            int valuesNumber = 4;
            Dictionary<int, int> presence = PresenceOfEachValue(valuesNumber);
            foreach (var presenceOfValue in presence)
                Assert.Equal(valuesNumber, presenceOfValue.Value);
        }

        [Fact]
        public void Have_each_values_present_in_06_Cards_when_values_number_6()
        {
            int valuesNumber = 6;
            Dictionary<int, int> presence = PresenceOfEachValue(valuesNumber);
            foreach (var presenceOfValue in presence)
                Assert.Equal(valuesNumber, presenceOfValue.Value);
        }

        [Fact]
        public void Have_each_values_present_in_08_Cards_when_values_number_8()
        {
            int valuesNumber = 8;
            Dictionary<int, int> presence = PresenceOfEachValue(valuesNumber);
            foreach (var presenceOfValue in presence)
                Assert.Equal(valuesNumber, presenceOfValue.Value);
        }

        [Fact]
        public void Have_each_values_present_in_12_cards_when_values_number_12()
        {
            int valuesNumber = 12;
            Dictionary<int, int> presence = PresenceOfEachValue(valuesNumber);
            foreach (var presenceOfValue in presence)
                Assert.Equal(valuesNumber, presenceOfValue.Value);
        }

        [Fact]
        public void Have_each_values_present_in_14_cards_when_values_number_14()
        {
            int valuesNumber = 14;
            Dictionary<int, int> presence = PresenceOfEachValue(valuesNumber);
            foreach (var presenceOfValue in presence)
                Assert.Equal(valuesNumber, presenceOfValue.Value);
        }

        [Fact]
        public void Have_each_values_present_in_90_cards_when_values_number_98()
        {
            int valuesNumber = 98;
            Dictionary<int, int> presence = PresenceOfEachValue(valuesNumber);
            foreach (var presenceOfValue in presence)
                Assert.Equal(valuesNumber, presenceOfValue.Value);
        }

        [Fact]
        public void Generate_unique_pair_values_when_values_number_03()
        {
            int valuesNumber = 3;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertPairValue(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Generate_unique_pair_values_when_values_number_04()
        {
            int valuesNumber = 4;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertPairValue(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Generate_unique_pair_values_when_values_number_06()
        {
            int valuesNumber = 6;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertPairValue(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Generate_unique_pair_values_when_values_number_08()
        {
            int valuesNumber = 8;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertPairValue(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Generate_unique_pair_values_when_values_number_12()
        {
            int valuesNumber = 12;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertPairValue(valuesNumber, DobbleCards);
        }

        [Fact]
        public void Generate_unique_pair_values_when_values_number_14()
        {
            int valuesNumber = 14;
            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;
            AssertPairValue(valuesNumber, DobbleCards);
        }

        #region Only test if you make the GenerateCardsWithSameFirstValue method public
#pragma warning disable S125
        //public class GenerateShould
        // Sections of code should not be commented out
        //{
        //    const string skip = "GenerateCardsWithSameFirstValue method now private";
        //    [Fact]
        //    public void When_valuesNumber_3_and_firstValue_0_Return_Card0_012()
        //    {
        //        int valuesNumber = 3;
        //        int firstValue = 0;
        //        int iCard = 0;
        //        var DobbleGame = new DobbleGame(valuesNumber);
        //        var cardExpected = new DobbleCard { Values = new List<int> { 0, 1, 2 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_3_and_firstValue_0_Return_Card1_034()
        //    {
        //        int valuesNumber = 3;
        //        int firstValue = 0;
        //        int iCard = 1;

        //        var DobbleGame = new DobbleGame(valuesNumber);
        //        var cardExpected = new DobbleCard { Values = new List<int> { 0, 3, 4 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_3_and_firstValue_0_Return_Card2_056()
        //    {
        //        int valuesNumber = 3;
        //        int firstValue = 0;
        //        int iCard = 2;

        //        var DobbleGame = new DobbleGame(valuesNumber);
        //        var cardExpected = new DobbleCard { Values = new List<int> { 0, 5, 6 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_0_Return_Card0_0123()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 0;
        //        int iCard = 0;

        //        var DobbleGame = new DobbleGame(valuesNumber);
        //        var cardExpected = new DobbleCard { Values = new List<int> { 0, 1, 2, 3 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_0_Return_Card1_0456()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 0;
        //        int iCard = 1;

        //        var DobbleGame = new DobbleGame(valuesNumber);
        //        var cardExpected = new DobbleCard { Values = new List<int> { 0, 4, 5, 6 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);


        //        Assert.Equal(valuesNumber, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_3_and_firstValue_1_Return_Card0_135()
        //    {
        //        int valuesNumber = 3;
        //        int firstValue = 1;
        //        int iCard = 0;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 1, 3, 5 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_3_and_firstValue_1_Return_Card1_146()
        //    {
        //        int valuesNumber = 3;
        //        int firstValue = 1;
        //        int iCard = 1;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 1, 4, 6 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_1_Return_Card0_147A()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 1;
        //        int iCard = 0;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 1, 4, 7, 10 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_1_Return_Card1_158B()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 1;
        //        int iCard = 1;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 1, 5, 8, 11 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_1_Return_Card2_169C()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 1;
        //        int iCard = 2;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 1, 6, 9, 12 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_2_Return_Card0_248C()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 2;
        //        int iCard = 0;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 2, 4, 8, 12 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_2_Return_Card1_259A()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 2;
        //        int iCard = 1;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 2, 5, 9, 10 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_2_Return_Card2_267B()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 2;
        //        int iCard = 2;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 2, 6, 7, 11 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_3_Return_Card0_349B()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 3;
        //        int iCard = 0;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 3, 4, 9, 11 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_3_Return_Card1_357C()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 3;
        //        int iCard = 1;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 3, 5, 7, 12 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }

        //    [Fact]
        //    public void When_valuesNumber_4_and_firstValue_3_Return_Card2_368A()
        //    {
        //        int valuesNumber = 4;
        //        int firstValue = 3;
        //        int iCard = 2;

        //        var DobbleGame = new DobbleGame(valuesNumber); var cardExpected = new DobbleCard { Values = new List<int> { 3, 6, 8, 10 } };
        //        var DobbleCards = DobbleGame.GenerateCardsWithSameFirstValue(valuesNumber, firstValue);

        //        Assert.Equal(valuesNumber - 1, DobbleCards.Count);
        //        Assert.Equal(cardExpected.Values, DobbleCards[iCard].Values);
        //    }
        //}
#pragma warning restore S125 // Sections of code should not be commented out
        #endregion


        private static void AssertAllValuesInEachCardPresentInEachOtherOnly1time(int valuesNumber, List<DobbleCard> DobbleCards)

        {
            for (int firstCardIndex = 0; firstCardIndex < DobbleCards.Count; firstCardIndex++)
                for (int secondCardIndex = firstCardIndex + 1; secondCardIndex < DobbleCards.Count; secondCardIndex++)
                    Assert.Equal(valuesNumber - 1, DobbleCards[secondCardIndex].PicturesIds.Except(DobbleCards[firstCardIndex].PicturesIds).Count());
        }

        private static Dictionary<int, int> PresenceOfEachValue(int valuesNumber)
        {
            int CardsNumberExpected = valuesNumber * valuesNumber - valuesNumber + 1;

            var DobbleCards = new DobbleCardsGame(valuesNumber).Cards;

            Dictionary<int, int> presence = new Dictionary<int, int>();
            for (int i = 0; i < CardsNumberExpected; i++)
            {
                presence.Add(i, 0);
            }
            foreach (var DobbleCard in DobbleCards)
            {
                foreach (var value in DobbleCard.PicturesIds)
                {
                    presence[value]++;
                }
            }
            return presence;
        }

        private static void AssertPairValue(int valuesNumber, List<DobbleCard> DobbleCards)
        {
            for (int firstCardIndex = 0; firstCardIndex < DobbleCards.Count; firstCardIndex++)
            {
                for (int secondCardIndex = firstCardIndex + 1; secondCardIndex < DobbleCards.Count; secondCardIndex++)
                {
                    for (int firstValueIndex = 0; firstValueIndex < valuesNumber; firstValueIndex++)
                    {
                        for (int secondValueIndex = firstValueIndex + 1; secondValueIndex < valuesNumber; secondValueIndex++)
                        {
                            var card1Value1 = DobbleCards[firstCardIndex].PicturesIds[firstValueIndex];
                            var card1Value2 = DobbleCards[firstCardIndex].PicturesIds[secondValueIndex];
                            var card2Value1 = DobbleCards[secondCardIndex].PicturesIds[firstValueIndex];
                            var card2Value2 = DobbleCards[secondCardIndex].PicturesIds[secondValueIndex];
                            Assert.False(card1Value1 == card2Value1 && card1Value2 == card2Value2
                                      || card1Value1 == card2Value2 && card1Value2 == card2Value1);
                        }
                    }
                }
            }
        }
    }
}
