using System;
using System.Collections.Generic;
using System.Text;

namespace SignalR
{
    public struct Player
    {
        public string ConnectionId { get; set; }
        public string Pseudo { get; set; }
        public bool GameOwner { get; set; }
        public bool Connected { get; set; }


        public Player(string connectionId, string pseudo, bool gameOwner = false, bool connected = true)
        {
            ConnectionId = connectionId;
            Pseudo = pseudo;
            GameOwner = gameOwner;
            Connected = connected;
        }

        public bool Equals(Player player)
        {
            return ConnectionId == player.ConnectionId;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ConnectionId);
        }
    }
}
