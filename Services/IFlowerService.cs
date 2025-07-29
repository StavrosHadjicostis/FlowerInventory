using System.Collections.Generic;
using FlowerInventory.Models;

namespace FlowerInventory.Services
{
    public interface IFlowerService
    {
        List<Flower> GetAll();
        Flower GetById(int id);
        void Add(Flower flower);
        void Update(Flower flower);
        void Delete(int id);
    }
}
