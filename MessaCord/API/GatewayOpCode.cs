namespace MessaCord.API
{
    public class GatewayOpCode 
    {
        /// <summary> C←S - Used to send most events. </summary>
        public  const int Dispatch = 0;
        /// <summary> C↔S - Used to keep the connection alive and measure latency. </summary>
        public  const int Heartbeat = 1;
        /// <summary> C→S - Used to associate a connection with a token and specify configuration. </summary>
        public  const int Identify = 2;
        /// <summary> C→S - Used to update client's status and current game id. </summary>
        public  const int StatusUpdate = 3;
        /// <summary> C→S - Used to join a particular voice channel. </summary>
        public  const int VoiceStateUpdate = 4;
        /// <summary> C→S - Used to ensure the guild's voice server is alive. </summary>
        public  const int VoiceServerPing = 5;
        /// <summary> C→S - Used to resume a connection after a redirect occurs. </summary>
        public  const int Resume = 6;
        /// <summary> C←S - Used to notify a client that they must reconnect to another gateway. </summary>
        public  const int Reconnect = 7;
        /// <summary> C→S - Used to request members that were withheld by large_threshold </summary>
        public  const int RequestGuildMembers = 8;
        /// <summary> C←S - Used to notify the client that their session has expired and cannot be resumed. </summary>
        public  const int InvalidSession = 9;
        /// <summary> C←S - Used to provide information to the client immediately on connection. </summary>
        public  const int Hello = 10;
        /// <summary> C←S - Used to reply to a client's heartbeat. </summary>
        public  const int HeartbeatAck = 11;

        /// <summary> C→S - Used to request presence updates from particular guilds. </summary>
        public  const int GuildSync = 12;
    }
}
