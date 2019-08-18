using System.Collections.Generic;
using PirateShip.Project.Interfaces;
using PirateShip.Project.Models;
using System;

// Like "App" from Planets Express
// maybe instead of a key to open a door or a chest, you find map pieces or clues that lead you to the treasure on the ship?
// maybe there's treasure hidden in every room, and you take what you can find in each room? and its timed so you have to leave the room before the ghost pirates get you?

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
      CurrentRoom = CurrentRoom.Go(direction);
    }

    public void Help()
    {
      Console.WriteLine("To move around the ship, try typing \"go aft, forward, up or down\". Type \"inventory\" to see if you have any of McBeardy's treasures in your inventory. If you do, type \"give\" + item. Type \"look\" to look about the room to see if there might be treasures hidden. If so, type \"take\" + item. To quit, type \"quit\". ");
    }

    public void Inventory()
    {
      Console.WriteLine($"{CurrentPlayer.PlayerName}'s inventory: ");
      foreach (var item in CurrentPlayer.Inventory)
      {
        Console.WriteLine(item.Name);

      }
    }

    public void Look()
    {
      Console.WriteLine(CurrentRoom.Description);
      foreach (var item in CurrentRoom.Items)
      {
        Console.WriteLine($"Seems like there should be a {item.Name} in here somewhere...");
      }
    }

    public void Quit()
    {
      Console.WriteLine("L8r sk8r");
      GameIsOn = false;
    }

    public void Reset()
    {
      GameIsOn = false;
      Setup();
      StartGame();
    }

    public void Setup()
    // TODO Rooms- Create, add items, add exits
    // Bow- compass
    // Crow's nest- spyglass
    // Gangway- null
    // Stern- null
    // Poopdeck- smuggled coins 
    // Crew- Gold dubloon
    // Captain- cursed medallion
    // Hold(change from stores)- gems
    {
      // TODO make this a Console.Readline(); 
      // Player player = new Player("Matthew McConaughey");


      Item compass = new Item("Compass", "An old compass made of gold", 300);
      Item spyglass = new Item("Spyglass", "A small spyglass made of gold", 500);
      Item coins = new Item("Coins", "Foreign coins that a crew member smuggled and hid", 300);
      Item dubloon = new Item("Dubloon", "One large golden dubloon", 500);
      Item medallion = new Item("Medallion", "An evil looking medallion. It feels warm. Probably cursed.", 400);
      Item gems = new Item("Gems", "Piles of emeralds, rubies, sapphires sparkling even in the darkness", 1000);


      Room bow = new Room("Bow", "You are on the bow of the ship. Behind you is the ocean, and in front of you is the rest of the ship. There is a small trapdoor at your feet.");
      Room crowsNest = new Room("Crow's Nest", "You are in the Crow's Nest. You can see for miles up here.");
      Room gangway = new Room("Gangway", "You are walking along the gangway");
      Room stern = new Room("Stern", "You are at the stern of the ship");
      Room poopDeck = new Room("Poop Deck", "You are on the Poopdeck");
      Room crew = new Room("Crew's Quarter's", "You are in the Crew's Quarters. You see an open doorway across the room labeled \"Captain's Quarters\". On the wall to the left of the Captain's Quarters is a closed door labeled \"Hold\".");
      Room captain = new Room("Captain's quarters", "You are in the Captain's quarters. There is a large important-looking desk.");
      Room hold = new Room("Hold", "You are in the hold. You see rows of stacks of barrels, and at the end, a very large old-looking treasure chest.");

      bow.AddExit("up", crowsNest);
      bow.AddExit("aft", gangway);
      crowsNest.AddExit("down", bow);
      gangway.AddExit("aft", stern);
      gangway.AddExit("forward", bow);
      stern.AddExit("up", poopDeck);
      stern.AddExit("forward", gangway);
      stern.AddExit("down", captain);
      poopDeck.AddExit("down", stern);
      captain.AddExit("forward", crew);
      captain.AddExit("up", stern);
      crew.AddExit("down", hold);
      crew.AddExit("aft", captain);
      hold.AddExit("up", crew);

      bow.AddItem(compass);
      crowsNest.AddItem(spyglass);
      poopDeck.AddItem(coins);
      crew.AddItem(dubloon);
      captain.AddItem(medallion);
      hold.AddItem(gems);

      CurrentRoom = bow;

    }

    public void StartGame()
    {
      Console.Clear();
      Console.WriteLine("Ahoy there laddie! Who disturbs my peace?");
      string name = Console.ReadLine();
      CurrentPlayer = new Player(name);
      Console.WriteLine("When you climbed aboard my ship, you disturbed my slumber! Me name's Beardy McWeirdy, and me treasures were left scattered about when me crew jumped ship years ago. I cannot go to rest until me treasure is returned to me! Can you help me by finding the treasures and giving them back? I will reward you with yer currency you call \"dollars\". When you find an item, just give it to me!");

      // Console.WriteLine($"You are treasure hunter, {CurrentPlayer.PlayerName}. You have found Beardy McWeirdy's lost pirate ship 'Fool's Gold' in search of his hidden treasures. You climb aboard and find yourself on the {CurrentRoom.Name.ToLower()}. ");
      // Console.WriteLine("");
      Look();
      // Console.WriteLine("What do you want to do?");

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
          case "give":
            UseItem(secondary);
            break;
          case "inventory":
            Inventory();
            break;
          case "help":
            Help();
            break;

        }
        if (CurrentPlayer.Findings == 3000)
        {
          Console.WriteLine("You have found all of my lost treasures! Now please let me be put to rest");
          Console.WriteLine("Beardy McWeirdy vanishes in a puff of green smoke.");
          GameIsOn = false;
        }

      }

    }

    public void TakeItem(string itemName)
    {
      Item item = CurrentRoom.Items.Find(i => i.Name.ToLower() == itemName.ToLower());
      if (item != null)
      {
        // Console.WriteLine(item.Name);
        Console.WriteLine(item.Description);
        CurrentPlayer.Inventory.Add(item);
        CurrentRoom.Items.Remove(item);
        Inventory();
        // if (item == CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == "treasure"))
        // {
        //   Console.WriteLine("You found the Captain's hidden treasure! Make your escape on the bow!");
        // }
      }
      else
      {
        Console.WriteLine("Can't do that!");
      }

    }

    public void UseItem(string itemName)
    {
      Item item = CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == itemName.ToLower());
      // if (item == CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == "medallion"))
      // {
      //   Console.WriteLine("This medallion is cursed. You have lost all your money, and all the items you have found.");
      //   CurrentPlayer.Findings = 0;
      //   CurrentPlayer.Inventory.RemoveAll();
      // }
      if (item != null)
      {
        Console.WriteLine($"{CurrentPlayer.PlayerName} gave the {item.Name} to the ghost of McBeardy.");
        Console.WriteLine($"McBeardy gave {CurrentPlayer.PlayerName} ${item.Value}");
        CurrentPlayer.Findings += item.Value;
        CurrentPlayer.Inventory.Remove(item);
        Console.WriteLine($"So far, {CurrentPlayer.PlayerName} has ${CurrentPlayer.Findings}");
      }
      else if (item == null)
      {
        Console.WriteLine("Can't do that!");
      }
    }

  }
}