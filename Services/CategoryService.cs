using System.Collections.Generic;
using System.Linq;
using FlowerInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerInventory.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly FlowerInventoryDbContext _context;

        public CategoryService(FlowerInventoryDbContext context)
        {
            _context = context;
        }

        public List<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            var category = _context.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {id} was not found.");
            return category;
        }

        public void Add(Category category)
        {
            try
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error adding the category to the database.", ex);
            }
        }

        public void Update(Category category)
        {
            if (!_context.Categories.Any(c => c.Id == category.Id))
                throw new KeyNotFoundException($"Category with ID {category.Id} does not exist.");

            try
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error updating the category.", ex);
            }
        }

        public void Delete(int id)
        {
            var category = _context.Categories.Find(id);
            if (category == null)
                throw new KeyNotFoundException($"Category with ID {id} does not exist.");

            try
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error deleting the category.", ex);
            }
        }
    }
}