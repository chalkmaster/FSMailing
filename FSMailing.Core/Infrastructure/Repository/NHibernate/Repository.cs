using System;
using NHibernate;

namespace FSMailing.Core.Infrastructure.Repository.NHibernate
{
    public class Repository<T> where T : PersistentEntity 
    {
        protected ISession Session { get; set; }

        protected Repository(ISession session)
        {
            Session = session;
        }

        public int CreateOrUpdate(T obj)
        {
            using (var transaction = Session.BeginTransaction())
            {
                obj.CreatedAt = DateTime.Now;
                Session.Save(obj);
                transaction.Commit();
            }
            return obj.Id;
        }
        public void Remove(T obj)
        {
            using (var transaction = Session.BeginTransaction())
            {
                Session.Delete(obj);
                transaction.Commit();
            }
        }

        public T GetById(int id)
        {
            return Session.QueryOver<T>().Where(t => t.Id == id).SingleOrDefault();
        }
    }
}
