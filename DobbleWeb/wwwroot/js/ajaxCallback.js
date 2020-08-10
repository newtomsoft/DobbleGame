function CallbackCreateOrJoinGame(data) {
    GameId = data.gameId;
    PicturesPerCard = data.picturesPerCard;
    SendPlayerInGame();
    ThisPlayerGuid = data.playerGuid;
    ShowGameIdInfo();
}

function CallbackJoinGameAsAdditionalDevice(data) {
    ThisAdditionalDevice = data.additionalDevice;
    PicturesPerCard = data.picturesPerCard;
    SendAdditionalDeviceInGame();
    ShowOrHideSections("additionalDevice");
}

function CallbackStartGame(data) {
    SendStartGame(data.centerCard, data.picturesNames);
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
        SendGameFinished(ThisPseudo);
    }
    else {
        let delayInMilliseconds = 1000;
        setTimeout(function () { PictureClickSubscribe(); }, delayInMilliseconds);
    }
}
