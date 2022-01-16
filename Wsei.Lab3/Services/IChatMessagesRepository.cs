using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wsei.Lab3.Entities;

namespace Wsei.Lab3.Services
{
    public interface IChatMessagesRepository
    {
        void Add(ChatMessageEntity message);

        IEnumerable<ChatMessageEntity> GetAll();
    }
}
