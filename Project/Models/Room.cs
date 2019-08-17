using System.Collections.Generic;
using PirateShip.Project.Interfaces;
using System;

namespace PirateShip.Project.Models
{
  public class Room : IRoom
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Item> Items { get; set; }
    public Dictionary<string, IRoom> Exits { get; set; }

    public void AddExit(string direction, IRoom room)
    {
      Exits.Add(direction, room);
    }
    public void AddItem(Item item)
    {
      Items.Add(item);
    }

    public IRoom Go(string direction)
    // This method must stop player from entering Stores room until a key is in the inventory, or until key is no longer in Captain.Items
    {
      if (Exits.ContainsKey(direction))
      {
        return Exits[direction];
      }
      Console.WriteLine("That's wrong");
      return this;

    }

    // public void PrintRoom(Room room)
    // {
    //   Console.WriteLine(room.Name);
    //   Console.WriteLine(room.Description);
    // }

    public Room(string name, string description)
    {
      Name = name;
      Description = description;
      Items = new List<Item>();
      Exits = new Dictionary<string, IRoom>();
      // string = direction, IRoom = room in that direction
    }
  }
}