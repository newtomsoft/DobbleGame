function CallbackCreateOrJoinGame(data) {
    GameId = data.gameId;
    PicturesPerCard = data.picturesPerCard;
    GamePicturesNumber = PicturesPerCard * PicturesPerCard - PicturesPerCard + 1;
    PicturesNames = data.picturesNames;
    LoadAllCardPictures();
    SendPlayerInGame();
    PlayerAdded = data.playerAdded;
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
}

function CallbackGetCardsPlayer(data) {
    PlayerCards = data;
    $('#startGame').hide();
    $('#startGameWait').hide();
}

function CallbackTouch(data) {
    if (data.status === 1) { // status ok
        PictureClickSubscribe();
        ChangePlayerCard();
        SendChangeCenterCard(data.centerCard);
    }
    else if (data.status === 2) {// game finished
        SendGameFinished(Pseudo);
    }
    else {
        let delayInMilliseconds = 1000;
        setTimeout(function () { PictureClickSubscribe(); }, delayInMilliseconds);
    }
}
