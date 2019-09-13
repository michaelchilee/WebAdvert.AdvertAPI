﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertAPI.Models;
using AutoMapper;

namespace AdvertAPI.Services
{
    public class AdvertProfile : Profile
    {
        public AdvertProfile()
        {
            CreateMap<AdvertModel, AdvertDBModel>(); // will map API to DB by attribute names. As long as same, it will map them
        }
    }
}
