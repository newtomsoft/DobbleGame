function CallbackCreateOrJoinGame(data) {
    GameId = data.gameId;
    PlayerAdded = data.playerAdded;
    if (!PlayerAdded) {
        ShowGameIdInfo();
        return;
    }
    PicturesPerCard = data.picturesPerCard;
    GamePicturesNumber = PicturesPerCard * PicturesPerCard - PicturesPerCard + 1;
    PicturesNames = data.picturesNames;
    LoadAllCardPictures();
    SendPlayerInGame();
    ShowGameIdInfo();
}

function CallbackJoinGameAsAdditionalDevice(data) {
    ThisAdditionalDevice = data.additionalDevice;
    PicturesPerCard = data.picturesPerCard;
    GamePicturesNumber = PicturesPerCard * PicturesPerCard - PicturesPerCard + 1;
    PicturesNames = data.picturesNames;
    LoadAllCardPictures();
    SendAdditionalDeviceInGame();
    ShowOrHideSections("additionalDevice");
}

function CallbackStartGame(data) {
    SendStartGame(data.centerCard);
}

function CallbackGetCenterCard(data) {
    CenterCard = data;
    DateCardsShown = Date.now();
}

function CallbackGetCardsPlayer(data) {
    PlayerCards = data;
    $('#startGame').hide();
    $('#startGameWait').hide();
}

function CallbackTouch(data) {
    if (data.status === 1) { // status ok
        console.log('picture touched ok');
        ChangePlayerCard();
        SendChangeCenterCard(data.centerCard);
    }
    else if (data.status === 2) {// game finished
        console.log('picture touched ok. Game finished');
        SendChangeCenterCard(data.centerCard);
        ChangePlayerCard();
        SendGameFinished(Pseudo);
    }
    else {
        console.log(`picture touched ko (code ${data.status})`);
        let delayInMilliseconds = 1000;
        setTimeout(function () { PictureClickSubscribe(); }, delayInMilliseconds);
    }
}
