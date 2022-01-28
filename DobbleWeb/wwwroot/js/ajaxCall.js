function CallCreateGame() {
    $.ajax({
        url: '/Game/Create',
        type: 'POST',
        data: { picturesPerCard: PicturesPerCard, playerId: PlayerId },
        success: function (data) { CallbackCreateOrJoinGame(data); },
    });
}

function CallJoinGame() {
    $.ajax({
        url: '/Game/Join',
        type: 'POST',
        data: { gameId: GameId, playerId: PlayerId },
        success: function (data) { CallbackCreateOrJoinGame(data); },
        error: function (err) { ShowError('errorJoinGame', err.responseText); },
    });
}

function CallInvitGame() {
    $.ajax({
        url: '/Game/Join',
        type: 'POST',
        data: { gameId: GameId, playerId: PlayerId },
        success: function (data) { CallbackCreateOrJoinGame(data); },
    });
}

function CallJoinGameAsAdditionalDevice() {
    GameId = $('#gameIdJoinGameAsAdditionalDevice').val().toUpperCase();
    $.ajax({
        url: '/Game/JoinAsAdditionalDevice',
        type: 'POST',
        data: { gameId: GameId },
        success: function (data) { CallbackJoinGameAsAdditionalDevice(data); },
    });
}

function CallStartGame() {
    $.ajax({
        url: '/Game/Start',
        type: 'POST',
        data: { gameId: GameId },
        success: function (data) { CallbackStartGame(data); },
    });
}

function GetCenterCard(card) {
    if (card === undefined)
        $.ajax({
            url: '/Game/GetCenterCard',
            data: { gameId: GameId },
            success: function (data) { CallbackGetCenterCard(data); },
        });
    else
        CenterCard = card;
}

function GetCardsPlayer() {
    $.ajax({
        url: '/Game/GetCardsPlayer',
        data: { gameId: GameId, playerId: PlayerId },
        success: function (data) { CallbackGetCardsPlayer(data); },
    });
}

function CallTouchCard(pictureId) {
    $.ajax({
        url: '/Game/Touch',
        type: 'POST',
        data: { gameId: GameId, playerId: PlayerId, cardPlayed: PlayerCards[0], pictureId: pictureId, touchDelay: DateCardTouched - DateCardsShown },
        success: function (data) { CallbackTouch(data, pictureId); },
    });
}
