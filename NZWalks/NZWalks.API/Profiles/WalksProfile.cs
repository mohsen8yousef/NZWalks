﻿using AutoMapper;

namespace NZWalks.API.Profiles
{
    public class WalksProfile : Profile
    {
        public WalksProfile() 
        {
            CreateMap<Models.Domain.Walk, Models.DTO.Walk>()
                .ReverseMap();
            CreateMap<Models.Domain.WalkDefficulty, Models.DTO.WalkDefficulty>()
                .ReverseMap();
        }
    }
}
