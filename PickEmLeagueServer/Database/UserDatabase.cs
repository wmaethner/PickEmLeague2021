﻿using System;
using System.Collections.Generic;
using System.Linq;
using PickEmLeagueServer.Models;

namespace PickEmLeagueServer.Database
{
    public class UserDatabase : IDatabase<User>
    {
        Dictionary<string, User> _users;

        #region Constructor
        public UserDatabase()
        {
            _users = new Dictionary<string, User>();
            User user = new User()
            {
                FirstName = "Alice",
                LastName = "Sanders",
                Email = "asanders@gmail.com"
            };
            _users.Add(user.Id.ToString(), user);

            user = new User()
            {
                FirstName = "Bob",
                LastName = "Smith",
                Email = "bsmith@gmail.com"
            };
            _users.Add(user.Id.ToString(), user);

            user = new User()
            {
                FirstName = "Caleb",
                LastName = "Johnson",
                Email = "cjohnson@gmail.com"
            };
            _users.Add(user.Id.ToString(), user);
        }
        #endregion

        #region IDatabase Interface
        public void Initialize()
        {
            
        }

        #region CRUD Operations
        public User Create(User item)
        {
            User newUser = (User)item.Clone();
            _users.Add(newUser.Id, newUser);
            return newUser;
        }

        public User Read(string id)
        {
            VerifyId(id);
            return _users[id];
        }

        public IEnumerable<User> Read()
        {
            return _users.Select(x => x.Value).ToList();
        }

        public User Update(User item)
        {
            VerifyId(item.Id);
            _users[item.Id] = item;
            return item;
        }

        public bool Delete(string id)
        {
            VerifyId(id);
            return _users.Remove(id);
        }
        #endregion
        #endregion

        private void VerifyId(string id)
        {
            if (!_users.ContainsKey(id))
            {
                throw new Exception($"No user with id {id}");
            }
        }

        
    }
}
