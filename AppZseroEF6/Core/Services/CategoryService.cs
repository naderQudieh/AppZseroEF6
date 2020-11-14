using AppZseroEF6.Data.Infrastructure;
using AppZseroEF6.Data.Repositories;
using AppZseroEF6.Entities;
using AppZseroEF6.ModelsDtos;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppZseroEF6.Service
{
    public interface ICategoryService
    {
        IQueryable<Category> GetCategories();
        Category GetCategory(long id);
        void CreateCategory(CategoryDto category);
        void SaveChanges();
    }
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public IQueryable<Category> GetCategories()
        {
            return _repository.GetAll();
        }

        public Category GetCategory(long id)
        {
            return _repository.GetById(id);
        }
        public void CreateCategory(CategoryDto category)
        {
            Category item = category.Adapt<Category>(); 
            _repository.Add(item);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }
    }
}
