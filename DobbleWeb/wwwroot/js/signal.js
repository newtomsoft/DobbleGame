//Hub Connection
var ConnectionHubGame;
async function CallSignalR() {
    ConnectionHubGame = new signalR.HubConnectionBuilder().withUrl("/hubGame").withAutomaticReconnect().build();
    await ConnectionHubGame.start().catch(function (err) { return console.error(err.toString()); });
    PlayerId = ConnectionHubGame.connectionId;
    ConnectionHubGame.on("ReceivePlayersInGame", function (pseudos, graphicMode) { ReceivePlayersInGame(pseudos, graphicMode); });
    ConnectionHubGame.on("ReceiveAdditionalDeviceInGame", function (additionalDevices) { ReceiveAdditionalDeviceInGame(additionalDevices); });
    ConnectionHubGame.on("ReceivePicturesLoaded", function () { ReceivePicturesLoaded(); });
    ConnectionHubGame.on("ReceiveStartGame", function (centerCard) { ReceiveStartGame(centerCard); });
    ConnectionHubGame.on("ReceiveChangeCenterCard", function (pseudo, centerCard) { ReceiveChangeCenterCard(pseudo, centerCard); });
    ConnectionHubGame.on("ReceiveGameFinished", function (pseudo) { ReceiveGameFinished(pseudo); });

}

//Send
function SendPlayerInGame() {
    console.log("SendPlayerInGame");
    ConnectionHubGame.invoke("HubPlayerInGame", GameId, Pseudo, GraphicMode).catch(function (err) { return console.error(err.toString()); });
}

function SendAdditionalDeviceInGame() {
    ConnectionHubGame.invoke("HubAdditionalDeviceInGame", GameId, ThisAdditionalDevice).catch(function (err) { return console.error(err.toString()); });
}

function SendPicturesLoaded() {
    ConnectionHubGame.invoke("HubPicturesLoaded", GameId).catch(function (err) { return console.error(err.toString()); });
}

function SendStartGame(centerCard) {
    ConnectionHubGame.invoke("HubStartGame", GameId, centerCard).catch(function (err) { return console.error(err.toString()); });
}

function SendChangeCenterCard(centerCard) {
    ConnectionHubGame.invoke("HubChangeCenterCard", GameId, Pseudo, centerCard).catch(function (err) { return console.error(err.toString()); });
}

function SendGameFinished() {
    ConnectionHubGame.invoke("HubGameFinished", GameId, Pseudo).catch(function (err) { return console.error(err.toString()); });
}

//Receive
async function ReceivePlayersInGame(players, graphicMode) {
    GraphicMode = graphicMode;
    ShowPlayersInGame(players);
    ShowOrHideSections();
    HideGameReady();
}

async function ReceivePicturesLoaded() {
    ShowGameReady();
}

async function ReceiveAdditionalDeviceInGame(additionalDevices) {
    AdditionalDevices = additionalDevices;
    $('#centerCard').hide();
    //todo ShowAdditionalDevicesInGame(additionalDevices);
    //autre chose ?
}

async function ReceiveStartGame(centerCard) {
    if (PlayerAdded) {
        Decounter = 3;
        DomAddDecounter();
        DecounterLunchGame();
        IntervalDecounterLunchGame = setInterval(function () { DecounterLunchGame(); }, 1000);
        GetCardsPlayer();
        GetCenterCard(centerCard);
    }
    else { //todo IntervalDecounterLunchGame idem au dessus
        GetCenterCard(centerCard);
        $('#startGame').hide();
        $('#startGameWait').hide();
        PrepareCard("centerCard");
        PreparePlayersInfos();
        ShowPlayersInfos();
    }
}

async function ReceiveChangeCenterCard(pseudo, centerCard) {
    ChangeCenterCard(centerCard);
    PrepareCards();
    ShowCards()
    ShowPlayerPutDownCard(pseudo);
}

async function ReceiveGameFinished(pseudo) {
    ShowPlayerPutDownCard(pseudo);
    PrepareCards();
    ShowCards()
    ShowGameFinished(pseudo);
}

