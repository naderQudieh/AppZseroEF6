using AppZseroEF6.Data.Infrastructure;
using AppZseroEF6.Data.Repositories;
using AppZseroEF6.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace AppZseroEF6.Service
{
    public interface ICategoryService
    {
        IQueryable<Category> GetCategories();
        Category GetCategory(long id);
        void CreateCategory(Category  category);
        void SaveChanges();
    }
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _repCategory;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService( IUnitOfWork unitOfWork)
        {
            _repCategory =    _unitOfWork.GetRepository<Entities.Category>() ;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Category> GetCategories()
        {
            return _repCategory.GetAll();
        }

        public Category GetCategory(long id)
        {
            return _repCategory.GetById(id);
        }
        public void CreateCategory(Category  category)
        {

            _repCategory.AddAsync(category);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public async Task AddAsync(Category  _category)
        {
            // Update user from DB
            using (var dbContextTransaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    _repCategory.AddAsync(_category); 
                    await _unitOfWork.CommitAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    ExceptionDispatchInfo.Capture(ex).Throw();
                }
            }
        }
    }
}
