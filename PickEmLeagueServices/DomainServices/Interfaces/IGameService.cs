﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using PickEmLeagueModels.Models;

namespace PickEmLeagueServices.DomainServices.Interfaces
{
    public interface IGameService
    {
        Task<Game> CreateForWeek(int week);
        IEnumerable<Game> GetForWeek(int week);
        Task<bool> DeleteGame(long id);

        Task AddScheduleAsync(StreamReader stream);

        bool IsWeekDone(int week);
    }
}
