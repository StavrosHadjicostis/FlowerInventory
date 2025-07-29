using System.Collections.Generic;
using System.Linq;
using FlowerInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerInventory.Services
{
    public class FlowerService : IFlowerService
    {
        private readonly FlowerInventoryDbContext _context;

        public FlowerService(FlowerInventoryDbContext context)
        {
            _context = context;
        }

        public List<Flower> GetAll()
        {
            return _context.Flowers.Include(f => f.Category).ToList();
        }

        public Flower GetById(int id)
        {
            return _context.Flowers.Include(f => f.Category).FirstOrDefault(f => f.Id == id);
        }

        public void Add(Flower flower)
        {
            _context.Flowers.Add(flower);
            _context.SaveChanges();
        }

        public void Update(Flower flower)
        {
            _context.Flowers.Update(flower);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var flower = _context.Flowers.Find(id);
            if (flower != null)
            {
                _context.Flowers.Remove(flower);
                _context.SaveChanges();
            }
        }
    }
}
