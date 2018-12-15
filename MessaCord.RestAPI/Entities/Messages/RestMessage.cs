using System;
using System.Collections.Generic;
using System.Text;
using MessaCord.RestAPI.API.Common;
using MessaCord.RestAPI.Entities.Channels;
using MessaCord.RestAPI.Entities.User;

namespace MessaCord.RestAPI.Entities.Messages
{
    public class RestMessage
    {
        public RestChannel Channel { get; set; }
        public string Content { get; set; }
        public RestUser Author { get; set; }

        public static RestMessage CreateMessage(Message m)
        {
            return new RestMessage
            {
                Channel = new RestChannel(m.ChannelId),
                Content = m.Content,
                Author = new RestUser { Username = m.Author.Username }
            };
        }
    }
}
