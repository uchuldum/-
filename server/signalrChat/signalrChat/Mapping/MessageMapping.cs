using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using signalrChat.DTO;
using signalrChat.Models;

namespace signalrChat.Mapping
{
    public class MessageMapping : Profile
    {
        public MessageMapping()
        {
            CreateMap<Message, MessageDTO>().ReverseMap();
        }
    }
}
