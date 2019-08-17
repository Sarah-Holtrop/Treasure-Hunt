using System.Collections.Generic;
using PirateShip.Project.Models;

namespace PirateShip.Project.Interfaces
{
  public interface IPlayer
  {
    string PlayerName { get; set; }
    List<Item> Inventory { get; set; }
  }
}
