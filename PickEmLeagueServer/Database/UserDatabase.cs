using System;
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
        }
        #endregion

        #region IDatabase Interface
        public void Initialize()
        {
            
        }

        #region CRUD Operations
        public bool Create(User item)
        {
            //item = (User)item.Clone();
            _users.Add(item.Id, item);
            return true;
        }

        public User Read(string id)
        {
            VerifyId(id);
            return _users[id];
        }

        public IEnumerable<User> Read()
        {
            List<User> users = _users.Select(x => x.Value).ToList();
            return users;
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
