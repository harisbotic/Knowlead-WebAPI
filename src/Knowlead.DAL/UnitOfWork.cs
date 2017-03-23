using System;
using Knowlead.DomainModel.LibraryModels;

namespace Knowlead.DAL
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext _context;
        private GenericRepository<Notebook> _notebookRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }


        public GenericRepository<Notebook> NotebookRepository
        {
            get
            {
                return _notebookRepository = _notebookRepository ?? new GenericRepository<Notebook>(_context);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //For generic getting of repositories
        // public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        // {
        //     //if (ServiceLocator.IsLocationProviderSet)
        //     //{
        //     //    return ServiceLocator.Current.GetInstance<IRepository<TEntity>>();
        //     //}

        //     if (_repositories == null)
        //     {
        //         _repositories = new Dictionary<string, dynamic>();
        //     }

        //     var type = typeof(TEntity).Name;

        //     if (_repositories.ContainsKey(type))
        //     {
        //         return (IRepository<TEntity>)_repositories[type];
        //     }

        //     var repositoryType = typeof(Repository<>);

        //     _repositories.Add(type, Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dataContext, this));

        //     return _repositories[type];
        // }
    }
}