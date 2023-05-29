using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banking.Cqrs.Core.Messages
{
    public abstract class Message
    {
        public string Id { get; set; } = String.Empty;

        public Message(string id)
        {
            Id = id;
        }
    }
}
