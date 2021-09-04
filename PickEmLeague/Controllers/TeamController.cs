using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PickEmLeagueModels.Models;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeague.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public TeamController(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public IEnumerable<Team> GetTeams()
        {
            return _mapper.Map<IEnumerable<Team>>(_teamRepository.GetAll());
        }
    }
}
