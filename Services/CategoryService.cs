using System.Collections.Generic;
using System.Linq;
using FlowerInventory.Models;

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
            return _context.Categories.FirstOrDefault(c => c.Id == id);
        }
    }
}
