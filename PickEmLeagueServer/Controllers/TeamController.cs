using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueDomain.Models;
using PickEmLeagueServices.Interfaces;

namespace PickEmLeagueAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TeamController : ControllerBase
    {
        ITeamService _teamService;

        public TeamController(ITeamService teamService) 
        {
            _teamService = teamService;
        }

        [HttpGet("{id}")]
        public Team GetTeam([FromRoute] int id)
        {
            return _teamService.GetTeam(id);
        }

        [HttpGet]
        public List<Team> GetTeams()
        {
            return _teamService.GetTeams();
        }
    }
}
