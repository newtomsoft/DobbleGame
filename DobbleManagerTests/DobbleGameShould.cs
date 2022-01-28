using DobbleManager;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Shouldly;

namespace DobbleManagerTests;

public class GenerateAllCardsShould
{
    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(12)]
    [InlineData(14)]
    public void Have_all_values_in_each_card_present_in_each_other_card_only_1_time(int valuesNumber)
    {
        var dobbleCards = new DobbleCardsGame(valuesNumber).Cards;
        for (var firstCardIndex = 0; firstCardIndex < dobbleCards.Count; firstCardIndex++)
            for (var secondCardIndex = firstCardIndex + 1; secondCardIndex < dobbleCards.Count; secondCardIndex++)
                dobbleCards[secondCardIndex].PicturesIds.Except(dobbleCards[firstCardIndex].PicturesIds).Count().ShouldBe(valuesNumber - 1);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(12)]
    [InlineData(14)]
    [InlineData(98)]
    public void Count_n_cards(int valuesNumber)
    {
        var dobbleCards = new DobbleCardsGame(valuesNumber).Cards;
        var expected = valuesNumber * valuesNumber - valuesNumber + 1;
        dobbleCards.Count.ShouldBe(expected);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(12)]
    [InlineData(14)]
    [InlineData(98)]
    public void Have_each_values_present_in_valuesNumber_cards(int valuesNumber)
    {
        var cardsNumberExpected = valuesNumber * valuesNumber - valuesNumber + 1;
        var dobbleCards = new DobbleCardsGame(valuesNumber).Cards;

        Dictionary<int, int> presence = new();
        for (var i = 0; i < cardsNumberExpected; i++) presence.Add(i, 0);
        foreach (var value in dobbleCards.SelectMany(dobbleCard => dobbleCard.PicturesIds)) presence[value]++;
        foreach (var presenceOfValue in presence) presenceOfValue.Value.ShouldBe(valuesNumber);
    }

    [Theory]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(8)]
    [InlineData(12)]
    [InlineData(14)]
    public void Generate_unique_pair_values(int valuesNumber)
    {
        var dobbleCards = new DobbleCardsGame(valuesNumber).Cards;

        for (var firstCardIndex = 0; firstCardIndex < dobbleCards.Count; firstCardIndex++)
        {
            for (var secondCardIndex = firstCardIndex + 1; secondCardIndex < dobbleCards.Count; secondCardIndex++)
            {
                for (var firstValueIndex = 0; firstValueIndex < valuesNumber; firstValueIndex++)
                {
                    for (var secondValueIndex = firstValueIndex + 1; secondValueIndex < valuesNumber; secondValueIndex++)
                    {
                        var card1Value1 = dobbleCards[firstCardIndex].PicturesIds[firstValueIndex];
                        var card1Value2 = dobbleCards[firstCardIndex].PicturesIds[secondValueIndex];
                        var card2Value1 = dobbleCards[secondCardIndex].PicturesIds[firstValueIndex];
                        var card2Value2 = dobbleCards[secondCardIndex].PicturesIds[secondValueIndex];
                        Assert.False(card1Value1 == card2Value1 && card1Value2 == card2Value2 || card1Value1 == card2Value2 && card1Value2 == card2Value1);
                    }
                }
            }
        }
    }

    #region Only test if you make the GenerateCardsWithSameFirstValue method public
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
    #endregion
}