using System;
using System.Collections.Generic;
using PickEmLeagueDatabase.Entities;
using PickEmLeagueServices.Repositories.Interfaces;

namespace PickEmLeagueServices.Repositories.Mocks
{
    public class MockTeamRepository : ITeamRepository
    {
        public MockTeamRepository()
        {
        }

        public IEnumerable<Team> GetAll()
        {
            return new List<Team>()
            {
                new Team() { Id = 1, City = "Arizona", Name = "Cardinals" },
                new Team() { Id = 2, City = "Atlanta", Name = "Falcons" },
                new Team() { Id = 3, City = "Baltimore", Name = "Ravens" },
                new Team() { Id = 4, City = "Buffalo", Name = "Bills" },
                new Team() { Id = 5, City = "Carolina", Name = "Panthers" },
                new Team() { Id = 6, City = "Chicago", Name = "Bears" },
                new Team() { Id = 7, City = "Cincinnati", Name = "Bengals" },
                new Team() { Id = 8, City = "Cleveland", Name = "Browns" },
                new Team() { Id = 9, City = "Dallas", Name = "Cowboys" },
                new Team() { Id = 10, City = "Denver", Name = "Broncos" },
                new Team() { Id = 11, City = "Detroit", Name = "Lions" },
                new Team() { Id = 12, City = "Green Bay", Name = "Packers" },
                new Team() { Id = 13, City = "Houston", Name = "Texans" },
                new Team() { Id = 14, City = "Indianapolis", Name = "Colts" },
                new Team() { Id = 15, City = "Jacksonville", Name = "Jaguars" },
                new Team() { Id = 16, City = "Kansas City", Name = "Chiefs" },
                new Team() { Id = 17, City = "Las Vegas", Name = "Raiders" },
                new Team() { Id = 18, City = "Los Angeles", Name = "Chargers" },
                new Team() { Id = 19, City = "Los Angeles", Name = "Rams" },
                new Team() { Id = 20, City = "Miami", Name = "Dolphins" },
                new Team() { Id = 21, City = "Minnesota", Name = "Vikings" },
                new Team() { Id = 22, City = "New England", Name = "Patriots" },
                new Team() { Id = 23, City = "New Orleans", Name = "Saints" },
                new Team() { Id = 24, City = "New York", Name = "Giants" },
                new Team() { Id = 25, City = "New York", Name = "Jets" },
                new Team() { Id = 26, City = "Philadelphia", Name = "Eagles" },
                new Team() { Id = 27, City = "Pittsburgh", Name = "Steelers" },
                new Team() { Id = 28, City = "San Francisco", Name = "49ers" },
                new Team() { Id = 29, City = "Seattle", Name = "Seahawks" },
                new Team() { Id = 30, City = "Tampa Bay", Name = "Buccaneers" },
                new Team() { Id = 31, City = "Tennessee", Name = "Titans" },
                new Team() { Id = 32, City = "Washington", Name = "Football Team" }
            };
        }
    }
}
