using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using signalrChat.Models;
using signalrChat.DTO;

namespace signalrChat.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
