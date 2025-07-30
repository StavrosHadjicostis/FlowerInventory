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
            var flower = _context.Flowers.Include(f => f.Category).FirstOrDefault(f => f.Id == id);
            if (flower == null)
                throw new KeyNotFoundException($"Flower with ID {id} was not found.");
            return flower;
        }

        public void Add(Flower flower)
        {
            try
            {
                _context.Flowers.Add(flower);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error adding the flower to the database.", ex);
            }
        }

        public void Update(Flower flower)
        {
            if (!_context.Flowers.Any(f => f.Id == flower.Id))
                throw new KeyNotFoundException($"Flower with ID {flower.Id} does not exist.");

            try
            {
                _context.Flowers.Update(flower);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error updating the flower.", ex);
            }
        }

        public void Delete(int id)
        {
            var flower = _context.Flowers.Find(id);
            if (flower == null)
                throw new KeyNotFoundException($"Flower with ID {id} does not exist.");

            try
            {
                _context.Flowers.Remove(flower);
                _context.SaveChanges();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("Error deleting the flower.", ex);
            }
        }
    }
}
