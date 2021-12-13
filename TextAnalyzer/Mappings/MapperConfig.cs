using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using TextAnalyzer.DTO;
using TextAnalyzer.Models;

namespace TextAnalyzer.Mappings
{
    public class MapperConfig
    {
        public static MapperConfiguration Configure()
        {
            var config = new MapperConfiguration
            (
                cfg =>
                {
                    cfg.CreateMap<ConcordanceItem, ConcordanceItemsDTO>().ReverseMap();
                }
            );
            return config;
        }
    }
}
