using System.Collections.Generic;
using PirateShip.Project.Interfaces;
using PirateShip.Project.Models;
using System;

// Like "App" from Planets Express

namespace PirateShip.Project
{
  public class GameService : IGameService
  {
    private bool GameIsOn { get; set; } = true;
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    public void GetUserInput()
    {
      Console.ReadLine();
    }

    public void Go(string direction)
    {
      // Console.WriteLine(direction);
      if (direction == "store")
      {
        Console.WriteLine("This door requires a key. Use key?");

        // if (CurrentPlayer.Inventory.Contains()){
        //   UseItem(key)
        // }
      }
      CurrentRoom = CurrentRoom.Go(direction);
    }

    public void Help()
    {
      // maybe a print options/directions for CurrentRoom;
    }

    public void Inventory()
    {
      foreach (var item in CurrentPlayer.Inventory)
      {
        Console.WriteLine($"Inventory = {item.Name}");

      }
    }

    public void Look()
    {
      Console.WriteLine(CurrentRoom.Description);
      // private bool containsItem;
      // if (containsItem){
      foreach (var item in CurrentRoom.Items)
      {

        Console.WriteLine($"Seems like there should be a {item.Name} in here somewhere...");
      }
      // }
    }

    public void Quit()
    {
      Console.WriteLine("L8r sk8r");
      GameIsOn = false;
    }

    public void Reset()
    {
      ;
    }

    public void Setup()
    // Bow- start,  (Go down)
    // Crew's quarters-2nd room
    // Captain's quarters(stern) - key
    // Stores- need key to get in- treasure
    {
      // TODO make this a Console.Readline(); 
      Player player = new Player("Matthew McConaughey");
      Item key = new Item("Key", "A big silver key. It looks very old");

      Room bow = new Room("Bow", "You are on the bow of the ship. Behind you is the ocean, and in front of you is the rest of the ship. There is a small trapdoor at your feet.");
      Room crew = new Room("Crew's Quarter's", "You are in the Crew's Quarters. You see an open doorway across the room labeled \"Captain's Quarters\". On the wall to the left of the Captain's Quarters is a closed door labeled \"Stores\".");
      Room captain = new Room("Captain's quarters", "You are in the Captain's quarters. There is a large important-looking desk.");
      Room stores = new Room("Stores", "You are in the stores. You see rows of stacks of barrels, and at the end, a very large old-looking treasure chest.");

      bow.AddExit("down", crew);
      crew.AddExit("captain", captain);
      // not sure if you can go backwards back to crew
      crew.AddExit("stores", stores);
      crew.AddExit("bow", bow);
      captain.AddExit("crew", crew);

      // change back to captain
      captain.AddItem(key);

      // bow.PrintRoom(bow);
      CurrentRoom = bow;
      CurrentPlayer = player;
    }

    public void StartGame()
    {

      Console.Clear();
      Console.WriteLine($"You are {CurrentPlayer.PlayerName}. You have found at the lost pirate ship 'Fool's Gold' in search of the captain's hidden treasure. You climb aboard and find yourself on the {CurrentRoom.Name.ToLower()}. You need to find the captain's old key to open the treasure chest.");
      Console.WriteLine("");
      Look();
      Console.WriteLine("What do you want to do?");

      while (GameIsOn)
      {
        string[] input = Console.ReadLine().ToLower().Split(' ');
        string command = input[0];
        string secondary = "";
        if (input.Length > 1)
        {
          secondary = input[1];
        }
        switch (command)
        {
          case "quit":
            // Console.WriteLine("In switch case quit");
            Quit();
            break;
          case "look":
            // Console.WriteLine("in switch case look");
            Look();
            break;
          case "go":
            Go(secondary);
            Console.WriteLine($"You entered {CurrentRoom.Name}.");
            break;
          case "take":
            TakeItem(secondary);
            break;
          case "use":
            UseItem(secondary);
            break;

        }

      }

    }

    public void TakeItem(string itemName)
    {
      Item item = CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName.ToLower());
      if (item != null)
      {
        Console.WriteLine(item.Name);
        Console.WriteLine(item.Description);
        CurrentPlayer.Inventory.Add(item);
        CurrentRoom.Items.Remove(item);
        Inventory();
      }
      else
      {
        Console.WriteLine("Can't do that!");
      }

    }

    public void UseItem(string itemName)
    {
      Item item = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName.ToLower());
      if (item != null)
      {
        Console.WriteLine($"{CurrentPlayer.PlayerName} used the {item.Name}");
        CurrentPlayer.Inventory.Remove(item);
      }
      else if (item == null)
      {
        Console.WriteLine("Can't do that!");
      }
    }

  }
}