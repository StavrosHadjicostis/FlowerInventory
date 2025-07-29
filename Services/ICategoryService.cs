using System.Collections.Generic;
using FlowerInventory.Models;

namespace FlowerInventory.Services
{
    public interface ICategoryService
    {
        List<Category> GetAll();
        Category GetById(int id);
    }
}
