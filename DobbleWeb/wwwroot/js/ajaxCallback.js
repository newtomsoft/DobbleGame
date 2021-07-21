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

function CallbackTouch(data, pictureId) {
    if (data.status === 1) { // status ok
        ChangePlayerCard();
        SendChangeCenterCard(data.centerCard);
    }
    else if (data.status === 2) {// game finished
        SendChangeCenterCard(data.centerCard);
        ChangePlayerCard();
        SendGameFinished(Pseudo);
    }
    else {
        ShowWrongPicture(pictureId);
        let delayInMilliseconds = 800;
        setTimeout(function () { PictureClickSubscribe(); }, delayInMilliseconds);
    }
}
