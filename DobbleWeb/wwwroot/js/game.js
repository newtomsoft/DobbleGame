$(document).ready(async function () {
    Url = new URL(window.location.href);
    GameId = Url.searchParams.get("game");
    if (GameId !== null) ModeInvitGame = true;
    CallSignalR();
    Init();
});

var Url
var ModeInvitGame
var GameId;
var ThisPseudo;
var PseudosInGame = [];
var ThisAdditionalDevice;
var AdditionalDevices = [];
var PlayerCards;
var PicturesPerCard;
var ThisPlayerGuid;
var CenterCard;
var DateEvent;
var PicturesNames = [];
var IntervalDecounterLunchGame;
var Decounter;

function Init() {
    $('#createGameForm').submit(function () { CreateGame(); });
    $('#joinGameForm').submit(function () { JoinGame(); });
    $('#invitGameForm').submit(function () { InvitGame(); });
    //$('#joinGameAsAdditionalDeviceForm').submit(function () { JoinGameAsAdditionalDevice(); }); // voir pour autre sprint si principe retenu
    $('#joinDeviceGameForm').submit(function () { JoinGameAsAdditionalDevice(); });
    $('#startGameButton').click(function () { StartGame(); });
    ShowOrHideSections();
}

function PictureClickSubscribe() { $('.pictureClick').click(function () { TouchCard(this.attributes['value'].value); }); }

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
    else if (ThisPseudo === undefined || GameId === null) {
        HideGameSection();
        HideInvitGameSection();
        ShowWelcomeSection();
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
    if (mode === "additionalDevice") {
        $('#startGame').hide();
        $('#startGameWait').hide();
        $('#playerCard').hide();
    }
    else if (PseudosInGame[0].pseudo !== ThisPseudo) {
        $('#startGame').hide();
        $('#startGameWait').html(`<b>En attente du lancement de la partie par ${PseudosInGame[0].pseudo}</b>`);
        $('#startGameWait').show();
    }
    else {
        $('#startGame').show();
        $('#startGameWait').hide();
    }
    $("#gameSection").show();
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
    if (cardType === "centerCard") {
        card = CenterCard;
        domPictureId = 'centerCardPicture';
    }
    else if (cardType === "playerCard") {
        card = PlayerCards[0];
        domPictureId = 'playerCardPicture';
    }
    else {
        console.error("erreur sur le type de carte à afficher")
        return;
    }
    let classClick = click ? 'pictureClick' : '';
    let cursor = click ? 'cursor-click' : '';
    $(`#${domPictureId}`).html("");
    let picture = '';
    for (let i = 0; i < PicturesPerCard; i++) {
        let url = Url.href.replace(Url.search, '').replace(Url.hash, '');;
        let pictureUrl = `'${url}/pictures/cardPictures/${PicturesNames[card.picturesIds[i]]}'`;
        picture += `<section id="${domPictureId}${i}" class="img-picture ${classClick} ${cursor}" style="background-image: url(${pictureUrl})" value="${card.picturesIds[i]}"></section>`;
    }
    $(picture).appendTo(`#${domPictureId}`);
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

function ShowPlayersInGame(pseudos) {
    PseudosInGame = [];
    pseudos.forEach(pseudo => PseudosInGame.push({ pseudo: pseudo, cardsNumber: 0 }))
    let pseudosString = [];
    PseudosInGame.forEach(item => pseudosString.push(item.pseudo + " "))
    $('#pseudos').html('<h4>Joueurs présents:</h4>' + pseudosString);
}

function ShowGameIdInfo() {
    if (ThisPlayerGuid == "") {
        $('#gameIdInfo').html(`<h3>La partie n° ${GameId} n'est plus disponible</h3>`);
        return;
    }
    let url = Url
    if (url.searchParams.get("game" === null))
        url.searchParams.append('game', GameId);
    else
        url.searchParams.set('game', GameId);
    let whatsappLink = `https://api.whatsapp.com/send?text=Je viens de lancer une nouvelle partie de Dobble. Voici le lien : ${url.href}`;
    $('#gameIdInfo').html(`<h3>Partie n° <a href="${url.href}">${GameId}</a> <a href="${whatsappLink}" target="_blank"><img alt="Whats'app" src="pictures/others/whatsApp.svg" width="30" height="30"</a></h3>`);
}

function PreloadAllCardPictures() {
    let totalPicturesNumber = PicturesPerCard * PicturesPerCard - PicturesPerCard + 1;
    let url = Url.href.replace(Url.search, '');
    //for (var i = 0; i < totalPicturesNumber; i++)
    //    jQuery.get(url + `/pictures/cardPictures/${PicturesNames[i]}`)
    let pics = [];
    for (var i = 0; i < totalPicturesNumber; i++) {
        pics[i] = `${url}pictures/cardPictures/${PicturesNames[i]}`;
    }
    $.preload(pics, { ordered: false });
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
        DomFlashDecounter();
        setTimeout(function () { DomRemoveDecounter(); }, 1200);
        clearInterval(IntervalDecounterLunchGame);
        LunchGame();
    }
    Decounter--;
}

function DomAddDecounter() { $('<div id="decounter"></div>').appendTo('#gameSection'); }
function DomRemoveDecounter() { $('#decounter').remove(); }
function ShowDecounter(countNumber) {
    if (countNumber > 3) $('#decounter').attr('style', 'color:green;');
    else if (countNumber > 1) $('#decounter').attr('style', 'color:orange;');
    else $('#decounter').attr('style', 'color:red;');
    $('#decounter').html(countNumber);
}
function DomFlashDecounter() {
    for (let i = 0; i < 15; i++) {
        $('#decounter').fadeToggle(70, function () {
            $(this).fadeToggle(70);
        });
    }
}

function ScrollTo(hash) { location.hash = `#${hash}`; }

function ShowError(domId, errorText) {
    $(`#${domId}`).html(`${errorText[0]} : ${errorText[1]}`);
}