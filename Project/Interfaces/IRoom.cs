using System.Collections.Generic;
using PirateShip.Project.Models;

namespace PirateShip.Project.Interfaces
{
  public interface IRoom
  {
    string Name { get; set; }
    string Description { get; set; }

    List<Item> Items { get; set; }
    Dictionary<string, IRoom> Exits { get; set; }

    IRoom Go(string direction);
  }
}
