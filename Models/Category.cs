using System;
using System.Collections.Generic;

namespace FlowerInventory.Models;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Flower> Flowers { get; set; } = new List<Flower>();
}
