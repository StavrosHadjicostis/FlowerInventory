using System;
using System.Collections.Generic;
using System.Linq;
using FlowerInventory.Models;
using FlowerInventory.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class CategoryServiceTests
{
    private FlowerInventoryDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<FlowerInventoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new FlowerInventoryDbContext(options);
        context.Categories.AddRange(
            new Category { Id = 1, Name = "Roses" },
            new Category { Id = 2, Name = "Lilies" }
        );
        context.SaveChanges();
        return context;
    }

    [Fact]
    public void GetAll_ReturnsAllCategories()
    {
        var context = GetDbContext();
        var service = new CategoryService(context);

        var categories = service.GetAll();

        Assert.Equal(2, categories.Count);
    }

    [Fact]
    public void GetById_ReturnsCategory_WhenExists()
    {
        var context = GetDbContext();
        var service = new CategoryService(context);

        var category = service.GetById(1);

        Assert.NotNull(category);
        Assert.Equal("Roses", category.Name);
    }

    [Fact]
    public void GetById_Throws_WhenNotFound()
    {
        var context = GetDbContext();
        var service = new CategoryService(context);

        Assert.Throws<KeyNotFoundException>(() => service.GetById(999));
    }

    [Fact]
    public void Add_AddsCategorySuccessfully()
    {
        var context = GetDbContext();
        var service = new CategoryService(context);

        var newCategory = new Category { Name = "Tulips" };
        service.Add(newCategory);

        Assert.Equal(3, context.Categories.Count());
    }

    [Fact]
    public void Update_UpdatesCategory_WhenExists()
    {
        var context = GetDbContext();
        var service = new CategoryService(context);

        var category = context.Categories.First();
        category.Name = "Updated Roses";
        service.Update(category);

        Assert.Equal("Updated Roses", context.Categories.First().Name);
    }

    [Fact]
    public void Update_Throws_WhenCategoryNotExists()
    {
        var context = GetDbContext();
        var service = new CategoryService(context);

        var fakeCategory = new Category { Id = 999, Name = "Ghost Category" };

        Assert.Throws<KeyNotFoundException>(() => service.Update(fakeCategory));
    }

    [Fact]
    public void Delete_RemovesCategory_WhenExists()
    {
        var context = GetDbContext();
        var service = new CategoryService(context);

        service.Delete(1);

        Assert.Single(context.Categories);
    }

    [Fact]
    public void Delete_Throws_WhenCategoryNotExists()
    {
        var context = GetDbContext();
        var service = new CategoryService(context);

        Assert.Throws<KeyNotFoundException>(() => service.Delete(999));
    }
}
