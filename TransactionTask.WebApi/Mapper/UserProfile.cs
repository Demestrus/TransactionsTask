using System;
using AutoMapper;
using TransactionTask.Core.Models;
using TransactionTask.WebApi.Models;

namespace TransactionTask.WebApi.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, ExistingUserDto>();

            CreateMap<UserDto, User>();

            CreateMap<long, DateTime>()
                .ConvertUsing(s => new DateTime(s).ToLocalTime());
            //в идеале преобразование в локальное время следует делать
            //на фронтэнде, чтобы учесть часовой пояс клиента);
        }
    }
}