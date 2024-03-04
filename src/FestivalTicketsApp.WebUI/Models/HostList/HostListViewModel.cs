﻿using FestivalTicketsApp.Application.HostService.DTO;

namespace FestivalTicketsApp.WebUI.Models.HostList;

public class HostListViewModel
{
    public List<string>? CityNames { get; set; }
    
    public List<HostDto>? Hosts { get; set; }
    
    public HostListQuery? QueryState { get; set; }
}