namespace SignalR;

public class HubSignal : Hub
{
    private static readonly Dictionary<string, List<Player>> PlayersPerGame = new();
    private static readonly Dictionary<string, string> GraphicModePerGame = new();
    private static readonly Dictionary<string, List<string>> AdditionalDevicesPerGame = new();

    public override Task OnConnectedAsync()
    {
        var gameId = PlayersPerGame.Where(ppg => ppg.Value.Count(p => p.ConnectionId == Context.ConnectionId) == 1).Select(ppg => ppg.Key).FirstOrDefault();
        if (gameId is not null)
        {
            var player = PlayersPerGame[gameId].FirstOrDefault(player => player.ConnectionId == Context.ConnectionId);
            player.Connected = true;
        }
        return base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception exception)
    {
        var gameId = PlayersPerGame.Where(ppg => ppg.Value.Count(p => p.ConnectionId == Context.ConnectionId) == 1).Select(ppg => ppg.Key).FirstOrDefault();
        if (string.IsNullOrEmpty(gameId)) return base.OnDisconnectedAsync(exception);
        var player = PlayersPerGame[gameId].FirstOrDefault(player => player.ConnectionId == Context.ConnectionId);
        player.Connected = false;
        return base.OnDisconnectedAsync(exception);
    }

    public async Task HubPlayerInGame(string gameId, string pseudo, string graphicMode)
    {
        Player player;
        if (PlayersPerGame.TryAdd(gameId, new List<Player>())) player = new Player(Context.ConnectionId, pseudo, true);
        else player = new Player(Context.ConnectionId, pseudo);

        PlayersPerGame[gameId].Add(player);
        GraphicModePerGame.TryAdd(gameId, graphicMode);
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        await Clients.Group(gameId).SendAsync("ReceivePlayersInGame", PlayersPerGame[gameId], GraphicModePerGame[gameId]);
    }

    public async Task HubAdditionalDeviceInGame(string gameId, string additionalDevice)
    {
        AdditionalDevicesPerGame.TryAdd(gameId, new List<string>());
        AdditionalDevicesPerGame[gameId].Add(additionalDevice);
        await Groups.AddToGroupAsync(Context.ConnectionId, gameId);
        await Clients.Group(gameId).SendAsync("ReceivePlayersInGame", PlayersPerGame[gameId], GraphicModePerGame[gameId]);
        await Clients.OthersInGroup(gameId).SendAsync("ReceiveAdditionalDeviceInGame", AdditionalDevicesPerGame[gameId]);
    }

    public async Task HubPicturesLoaded(string gameId) => await Clients.Group(gameId).SendAsync("ReceivePicturesLoaded");

    public async Task HubStartGame(string gameId, object centerCard) => await Clients.Group(gameId).SendAsync("ReceiveStartGame", centerCard);

    public async Task HubChangeCenterCard(string gameId, string pseudo, object centerCard) => await Clients.Group(gameId).SendAsync("ReceiveChangeCenterCard", pseudo, centerCard);

    public async Task HubGameFinished(string gameId, string pseudo) => await Clients.Group(gameId).SendAsync("ReceiveGameFinished", pseudo);
}