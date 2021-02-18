using System;
using System.Collections.Generic;

namespace PickEmLeagueServer.Database
{
    public interface IDatabase<T>
    {
        public void Initialize();

        #region CRUD Ops
        public bool Create(T item);
        public T Read(string id);
        public IEnumerable<T> Read();
        public T Update(T item);
        public bool Delete(string id);
        #endregion
    }
}
