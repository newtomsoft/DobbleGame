function CreateGame() {
    PicturesPerCard = $('#picturesNumber').val();
    ThisPseudo = $('#pseudoCreateGame').val();
    $.ajax({
        url: '/Game/Create',
        type: 'POST',
        data: { picturesPerCard: PicturesPerCard },
        success: function (data) { CallbackCreateOrJoinGame(data); },
    });
}

function JoinGame() {
    GameId = $('#gameIdJoinGame').val().toUpperCase();
    ThisPseudo = $('#pseudoJoinGame').val();
    $.ajax({
        url: '/Game/Join',
        type: 'POST',
        data: { gameId: GameId },
        success: function (data) { CallbackCreateOrJoinGame(data); },
        error: function (err) { ShowError('errorJoinGame', err.responseText); },
    });
}

function InvitGame() {
    ThisPseudo = $('#pseudoInvitGame').val();
    $.ajax({
        url: '/Game/Join',
        type: 'POST',
        data: { gameId: GameId },
        success: function (data) { CallbackCreateOrJoinGame(data); },
    });
}

function JoinGameAsAdditionalDevice() {
    GameId = $('#gameIdJoinGameAsAdditionalDevice').val().toUpperCase();
    $.ajax({
        url: '/Game/JoinAsAdditionalDevice',
        type: 'POST',
        data: { gameId: GameId },
        success: function (data) { CallbackJoinGameAsAdditionalDevice(data); },
    });
}

function StartGame() {
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
        data: { gameId: GameId, playerGuid: ThisPlayerGuid },
        success: function (data) { CallbackGetCardsPlayer(data); },
    });
}

function TouchCard(valueTouch) {
    PictureClickUnsubscribe();
    $.ajax({
        url: '/Game/Touch',
        type: 'POST',
        data: { gameId: GameId, playerGuid: ThisPlayerGuid, cardPlayed: PlayerCards[0], valueTouch: valueTouch, centerCard: CenterCard/*, timeTakenToTouch: 500*/ },
        success: function (data) { CallbackTouch(data); },
    });
}