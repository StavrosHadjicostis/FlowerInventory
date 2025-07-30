using System;
using System.Collections.Generic;
using System.Linq;
using FlowerInventory.Models;
using FlowerInventory.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

public class FlowerServiceTests
{
    private FlowerInventoryDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<FlowerInventoryDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()) // new DB for each test
            .Options;

        var context = new FlowerInventoryDbContext(options);
        context.Categories.Add(new Category { Id = 1, Name = "Roses" });
        context.Flowers.AddRange(
            new Flower { Id = 1, Name = "Red Rose", CategoryId = 1 },
            new Flower { Id = 2, Name = "White Rose", CategoryId = 1 }
        );
        context.SaveChanges();
        return context;
    }

    [Fact]
    public void GetAll_ReturnsAllFlowers()
    {
        var context = GetDbContext();
        var service = new FlowerService(context);

        var flowers = service.GetAll();

        Assert.Equal(2, flowers.Count);
    }

    [Fact]
    public void GetById_ReturnsFlower_WhenExists()
    {
        var context = GetDbContext();
        var service = new FlowerService(context);

        var flower = service.GetById(1);

        Assert.NotNull(flower);
        Assert.Equal("Red Rose", flower.Name);
    }

    [Fact]
    public void GetById_Throws_WhenNotFound()
    {
        var context = GetDbContext();
        var service = new FlowerService(context);

        Assert.Throws<KeyNotFoundException>(() => service.GetById(999));
    }

    [Fact]
    public void Add_AddsFlowerSuccessfully()
    {
        var context = GetDbContext();
        var service = new FlowerService(context);

        var newFlower = new Flower { Name = "Yellow Rose", CategoryId = 1 };
        service.Add(newFlower);

        Assert.Equal(3, context.Flowers.Count());
    }

    [Fact]
    public void Update_UpdatesFlower_WhenExists()
    {
        var context = GetDbContext();
        var service = new FlowerService(context);

        var flower = context.Flowers.First();
        flower.Name = "Updated Rose";
        service.Update(flower);

        Assert.Equal("Updated Rose", context.Flowers.First().Name);
    }

    [Fact]
    public void Update_Throws_WhenFlowerNotExists()
    {
        var context = GetDbContext();
        var service = new FlowerService(context);

        var fakeFlower = new Flower { Id = 999, Name = "Ghost Flower" };

        Assert.Throws<KeyNotFoundException>(() => service.Update(fakeFlower));
    }

    [Fact]
    public void Delete_RemovesFlower_WhenExists()
    {
        var context = GetDbContext();
        var service = new FlowerService(context);

        service.Delete(1);

        Assert.Single(context.Flowers); // Only 1 flower left
    }

    [Fact]
    public void Delete_Throws_WhenFlowerNotExists()
    {
        var context = GetDbContext();
        var service = new FlowerService(context);

        Assert.Throws<KeyNotFoundException>(() => service.Delete(999));
    }
}
