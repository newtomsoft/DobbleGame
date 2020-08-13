$(document).ready(async function () {
    Url = new URL(window.location.href);
    GameId = Url.searchParams.get("game");
    if (GameId !== null) ModeInvitGame = true;
    Init();
});

var Url;
var ModeInvitGame;
var GameId;
var PlayerId;
var Pseudo;
var GraphicMode; // row, dubbleLike
var PseudosInGame = [];
var ThisAdditionalDevice;
var AdditionalDevices = [];
var PlayerCards;
var CenterCard;
var PicturesPerCard;
var GamePicturesNumber;
var PlayerAdded = false;
var DateEvent;
var PicturesNames = [];
var PicturesPlayer = [];
var PicturesCenter = [];
var IntervalDecounterLunchGame;
var Decounter;
var GameOwner = false;

function Init() {
    $('#createGameForm').submit(function () { CreateGame(); });
    $('#joinGameForm').submit(function () { JoinGame(); });
    $('#invitGameForm').submit(function () { InvitGame(); });
    $('#joinDeviceGameForm').submit(function () { JoinGameAsAdditionalDevice(); });
    $('#startGameButton').click(function () { CallStartGame(); });
    ShowOrHideSections();
}

async function CreateGame() {
    await CallSignalR();
    PicturesPerCard = $('#picturesNumber').val();
    Pseudo = $('#pseudoCreateGame').val();
    GraphicMode = $('#gameMode').val();
    GameOwner = true;
    CallCreateGame();
}

async function JoinGame() {
    await CallSignalR();
    GameId = $('#gameIdJoinGame').val().toUpperCase();
    Pseudo = $('#pseudoJoinGame').val();
    CallJoinGame();
}

async function InvitGame() {
    await CallSignalR();
    Pseudo = $('#pseudoInvitGame').val();
    CallInvitGame();
}
async function JoinGameAsAdditionalDevice() {
    await CallSignalR();
    CallJoinGameAsAdditionalDevice();
}

function PictureClickSubscribe() { $('.pictureClick').click(function () { CallTouchCard(this.attributes['value'].value); }); }

function PictureClickUnsubscribe() { $('.pictureClick').off("click"); }

function ShowOrHideSections(mode) {
    if (mode === "additionalDevice") {
        HideWelcomeSection();
        HideInvitGameSection();
        ShowGameSection(mode);
    }
    else if (ModeInvitGame !== undefined) {
        ModeInvitGame = undefined;
        HideGameSection();
        HideWelcomeSection();
        ShowInvitGameSection();
    }
    else if (Pseudo === undefined || GameId === null) {
        HideGameSection();
        HideInvitGameSection();
        ShowWelcomeSection();
    }
    else if (!PlayerAdded) {
        HideWelcomeSection();
        HideInvitGameSection();
        HideGameSection();
    }
    else {
        HideWelcomeSection();
        HideInvitGameSection();
        ShowGameSection(mode);
    }
}

function ShowInvitGameSection() {
    $('#invitGameSectionTitle').html(`Rejoindre la partie ${GameId}`);
    $("#invitGameSection").show();
}

function HideInvitGameSection() { $("#invitGameSection").hide(); }

function ShowWelcomeSection() { $("#createJoinGameSection").show(); }

function HideWelcomeSection() { $("#createJoinGameSection").hide(); }

function ShowGameSection(mode) {
    if (mode === "additionalDevice")
        $('#playerCard').hide();
    else if (!GameOwner)
        $('#startGameWait').html(`<b>En attente du lancement de la partie par ${PseudosInGame[0].pseudo}</b>`);
    $("#gameSection").show();
}

function ShowGameReady() {
    $('#loadingGame').hide();
    if (!GameOwner)
        $('#startGameWait').show();
    else
        $('#startGame').show();
}

function HideGameReady() {
    $('#loadingGame').show();
    $('#startGameWait').hide();
    $('#startGame').hide();
}

function HideGameSection() { $("#gameSection").hide(); }

function PrepareCards() {
    PrepareCard("centerCard");
    PrepareOpponentsCards();
    if (ThisAdditionalDevice === undefined) {
        PrepareCard("playerCard", true);
    }
}

function PrepareCard(cardType, click) {
    let card;
    let domPictureId;
    let pictures;
    if (cardType === "centerCard") {
        card = CenterCard;
        domPictureId = 'centerCardPicture';
        pictures = PicturesCenter;
    }
    else if (cardType === "playerCard") {
        card = PlayerCards[0];
        domPictureId = 'playerCardPicture';
        pictures = PicturesPlayer;
    }
    else {
        console.error("erreur sur le type de carte à afficher")
        return;
    }
    let classClick = click ? 'pictureClick' : '';
    let cursor = click ? 'cursor-click' : '';
    $(`#${domPictureId}`).html("");
    for (let i = 0; i < PicturesPerCard; i++) {
        let imageDom = pictures[card.picturesIds[i]];
        imageDom.id = `${domPictureId}${i}`;
        imageDom.className = `img-picture ${classClick} ${cursor}`;
        imageDom.setAttribute('value', `${card.picturesIds[i]}`);
        $(imageDom).appendTo(`#${domPictureId}`);
    }
    if (cardType === "playerCard")
        PictureClickSubscribe();
}

function ShowCards() {
    ShowCenterCard();
    ShowOpponentsCards();
    if (ThisAdditionalDevice === undefined) {
        ShowPlayerCard();
        ShowPlayerCardsNumber();
        ScrollTo('centerCard');
    }
}

function ShowCenterCard() { $('#centerCardPicture').show(); }
function ShowPlayerCard() { $('#playerCardPicture').show(); }
function ShowOpponentsCards() { ; } //todo

function HideCards() {
    HidePlayerCard();
    HideCenterCard();
    HideOpponentsCards();
}

function HidePlayerCard() {
    for (var i = 0; i < PicturesPerCard; i++) {
        $(`#cardPicture${i}`).hide();
    }
    $('#playerCardPicture').hide();
}

function HideCenterCard() {
    for (let i = 0; i < PicturesPerCard; i++)
        $(`#centerCardPicture${i}`).hide();
    $('#centerCardPicture').hide();
}

function HideOpponentsCards() { ; }//todo

function PrepareOpponentsCards() { ; } //todo


function ChangePlayerCard() {
    PlayerCards = PlayerCards.slice(1);
    ShowPlayerCardsNumber();
}

function ShowPlayerCardsNumber() { $('#cardsNumber').html(`Il vous reste : ${PlayerCards.length} cartes`); }

function ShowGameFinished(winner) {
    const pseudosRankings = PseudosInGame.sort(ComparePseudosCardsNumber);
    let text = '';
    pseudosRankings.forEach(item => text += item.pseudo + " a " + item.cardsNumber + " carte(s)\n");
    alert(`Partie gagnée par ${winner}\n${text}`);
    //todo show popup
}

function ChangeCenterCard(card) { CenterCard = card; }

function ShowPlayersInGame(players) {
    PseudosInGame = [];
    players.forEach(player => PseudosInGame.push({ pseudo: player.pseudo, cardsNumber: 0 }))
    let pseudosString = [];
    PseudosInGame.forEach(item => pseudosString.push(item.pseudo + " "))
    $('#pseudos').html('<h4>Joueurs présents:</h4>' + pseudosString);
}

function ShowGameIdInfo() {
    if (!PlayerAdded) {
        $('#gameDontExist').html(`<h3>La partie n° ${GameId} n'est plus disponible</h3>`);
        $('#gameDontExist').show();
        return;
    }
    let url = Url
    if (url.searchParams.get("game" === null))
        url.searchParams.append('game', GameId);
    else
        url.searchParams.set('game', GameId);
    let whatsappLink = `https://api.whatsapp.com/send?text=Je viens de lancer une nouvelle partie de Dobble. Voici le lien : ${url.href}`;
    $('#gameIdInfo').html(`<h3>Partie n° <a href="${url.href}">${GameId}</a> <a href="${whatsappLink}"><img alt="Whats'app" src="pictures/others/whatsApp.svg" width="30" height="30"></a></h3>`);
}

function LoadAllCardPictures() {
    let picturesLoadedNumber = 0;
    let url = Url.href.replace(Url.search, '');
    for (var i = 0; i < GamePicturesNumber; i++) {
        PicturesPlayer[i] = new Image();
        $(PicturesPlayer[i]).on('load', function () {
            picturesLoadedNumber++;
            if (picturesLoadedNumber === GamePicturesNumber)
                SendPicturesLoaded();
        });
        PicturesPlayer[i].src = `${url}pictures/cardPictures/${PicturesNames[i]}`;
        PicturesCenter[i] = new Image();
        PicturesCenter[i].src = PicturesPlayer[i].src;
    }
}

function PreparePlayersInfos() {
    PseudosInGame.forEach(item => item.cardsNumber = PlayerCards.length);
}

function ShowPlayersInfos() {
    let pseudosString = [];
    if (ThisAdditionalDevice !== undefined)
        PseudosInGame.forEach(item => pseudosString.push(`${item.pseudo} (${item.cardsNumber}) `));
    else
        PseudosInGame.forEach(item => pseudosString.push(`${item.pseudo} `)); // todo temp en attendant de recevoir le nombre de carte
    $('#pseudos').html('<h4>Joueurs présents:</h4>' + pseudosString);
}

function ShowPlayerPutDownCard(pseudo) {
    const indexFound = PseudosInGame.findIndex(item => item.pseudo == pseudo);
    PseudosInGame[indexFound].cardsNumber--;
    ShowPlayersInfos();
}

function ComparePseudosCardsNumber(pseudo1, pseudo2) { return pseudo1.cardsNumber - pseudo2.cardsNumber; }

function ShowAdditionalDevicesInGame(additionalDevices) { ; } //todo

function LunchGame() {
    PrepareCards();
    PreparePlayersInfos();
    ShowCards();
    ShowPlayersInfos();
}

function DecounterLunchGame() {
    ShowDecounter(Decounter);
    if (Decounter === 0) {
        DomRemoveDecounter();
        clearInterval(IntervalDecounterLunchGame);
        LunchGame();
    }
    Decounter--;
}

function DomAddDecounter() { $('<div id="decounter"></div>').appendTo('#gameSection'); }
function DomRemoveDecounter() { $('#decounter').remove(); }
function ShowDecounter(countNumber) {
    if (countNumber == 3) $('#decounter').attr('style', 'color:green;');
    else if (countNumber == 2) $('#decounter').attr('style', 'color:orange;');
    else $('#decounter').attr('style', 'color:red;');
    $('#decounter').html(countNumber);
}

function ScrollTo(hash) { location.hash = `#${hash}`; }

function ShowError(domId, errorText) {
    $(`#${domId}`).html(`${errorText[0]} : ${errorText[1]}`);
}

IncrementpicturesLoadNumber = (function () {
    let variableName = 0;
    let init = function () {
        variableName += 1;
        if (variableName === GamePicturesNumber)
            alert(variableName);
    }
    return init;
})();