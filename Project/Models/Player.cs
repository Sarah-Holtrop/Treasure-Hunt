using System.Collections.Generic;
using PirateShip.Project.Interfaces;

namespace PirateShip.Project.Models
{
  public class Player : IPlayer
  {
    public string PlayerName { get; set; }
    public List<Item> Inventory { get; set; }
    public int Findings { get; set; }

    // private int Findings { get; set; }

    public Player(string name)
    {
      PlayerName = name;
      Inventory = new List<Item>();
      Findings = 0;
    }
  }
}