using System.Collections.Generic;
using PirateShip.Project.Interfaces;
using PirateShip.Project.Models;
using System;
using System.Threading;


namespace PirateShip.Project
{
  public class GameService : IGameService
  {
    private bool GameIsOn { get; set; } = true;
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }

    public void GetUserInput()
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
          Quit();
          break;
        case "look":
          Look();
          break;
        case "go":
          Console.Clear();
          Go(secondary);
          Console.WriteLine($"You are in {CurrentRoom.Name}.");
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
        case "reset":
          Reset();
          break;
        default:
          Console.WriteLine("Not a valid command. Type \"help\" for directions.");
          break;

      }
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
      Console.WriteLine("You jump ship, abandoning all treasure.");
      GameIsOn = false;
    }

    public void Reset()
    {
      GameIsOn = false;
      Setup();
      StartGame();
    }

    public void Setup()
    {
      // Items
      Item compass = new Item("Compass", "an old compass made of gold.", 300);
      Item spyglass = new Item("Spyglass", "a small spyglass made of gold.", 500);
      Item coins = new Item("Coins", "foreign coins that a crew member smuggled and hid.", 200);
      Item dubloon = new Item("Dubloon", "one large golden dubloon.", 500);
      Item medallion = new Item("Medallion", "an evil looking medallion. It feels warm. Probably cursed.", 0);
      Item gems = new Item("Gems", "piles of emeralds, rubies, sapphires sparkling even in the darkness.", 1500);

      // Rooms
      Room bow = new Room("Bow", "You are on the bow of the ship. Behind you is the ocean, and in front of you is the rest of the ship. Before you, there is a ladder leading up to the crow's nest, or you can go aft to the gangway. Something sparkly is catching the light.");
      Room crowsNest = new Room("Crow's Nest", "You are in the Crow's Nest. You can see for miles up here. There is something gleaming tangled in the ropes.");
      Room gangway = new Room("Gangway", "You are walking along the gangway. Farther aft is the stern");
      Room stern = new Room("Stern", "You are at the stern of the ship. Forward is the gangway, and a small stairway leads up to the poopdeck.");
      Room poopDeck = new Room("Poop Deck", "You are on the Poopdeck. You are at the farthest possible position from the bow. Down the steps is the stern. One of the floorboards seems to be loose.");
      Room crew = new Room("Crew's Quarter's", "You are in the Crew's Quarters. You see rows of sleeping quarters. Aft is a door labeled \"Captain's Quarters\", and at your feet is a trapdoor. One of the old mattresses looks unusually lumpy.");
      Room captain = new Room("Captain's quarters", "You are in the Captain's quarters. There is a large important-looking desk with many drawers. Forward is a door labeled \"Crew's Quarters\"");
      Room hold = new Room("Hold", "You are in the hold. Up the ladder is the Crew's quarters. You see rows of stacks of barrels, and at the end, a very large old-looking treasure chest.");

      // Exits, advancing and backtracking
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

      // Adding Items to Rooms
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
      string intro = "Ahoy there laddie! Who disturbs my peace?\n";
      foreach (char letter in intro)
      {
        Console.Write(letter);
        Thread.Sleep(10);
      }

      string name = Console.ReadLine();
      CurrentPlayer = new Player(name);
      // For faster monologue, uncomment:
      // Console.WriteLine($" Welcome aboard, {CurrentPlayer.PlayerName}! When you climbed aboard my ship, you disturbed my slumber! Me name's Beardy McWeirdy, ye olde captain of this here ship \"Fool's Gold\". I've been haunting her since me crew jumped ship years ago. I cannot go to rest until all me treasure is returned to me! Can you help me by finding the treasures and giving them back? I will reward you with yer currency you call \"dollars\". When you find an item, just give it to me!");
      string monologue = $" Welcome aboard, {CurrentPlayer.PlayerName}! When you climbed aboard my ship, you disturbed my slumber! Me name's Beardy McWeirdy, ye olde captain of this here ship \"Fool's Gold\". I've been haunting her since me crew jumped ship years ago. I cannot go to rest until all me treasure is returned to me! Can you help me by finding the treasures and giving them back? I will reward you with yer currency you call \"dollars\". When you find an item, just give it to me! \n";
      foreach (char letter in monologue)
      {
        Console.Write(letter);
        Thread.Sleep(10);
      }
      Console.WriteLine("If you get stuck, type \"look\" to take a look around.");


      while (GameIsOn)
      {
        GetUserInput();
        if (CurrentPlayer.Findings == 3000)
        {
          string win = "You have found all of my lost treasures! Now please let me be put to rest \n";
          foreach (char letter in win)
          {
            Console.Write(letter);
            Thread.Sleep(10);
          }
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
        Console.WriteLine($"You found {item.Description}");
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
      if (item != null && item.Name.ToLower() != "medallion")
      {
        Console.WriteLine($"{CurrentPlayer.PlayerName} gave the {item.Name} to the ghost of McBeardy.");
        string thank = $"Thank you, {CurrentPlayer.PlayerName}! Here's ${item.Value}\n";
        foreach (char letter in thank)
        {
          Console.Write(letter);
          Thread.Sleep(10);
        }
        CurrentPlayer.Findings += item.Value;
        CurrentPlayer.Inventory.Remove(item);
        Console.WriteLine($"So far, {CurrentPlayer.PlayerName} has ${CurrentPlayer.Findings}");
      }
      // medallion is "cursed" so UseItem("medallion") functions differently
      else if (item == CurrentPlayer.Inventory.Find(i => i.Name.ToLower() == "medallion"))
      {
        Console.WriteLine($"{CurrentPlayer.PlayerName} gave the {item.Name} to the ghost of McBeardy.");
        Console.WriteLine("This medallion is cursed. You have lost all your money, and all the items you have found. You reappear on the bow in a puff of green smoke.");
        CurrentPlayer.Findings = 0;
        CurrentPlayer.Inventory.Clear();
        Setup();

      }
      else if (item == null)
      {
        Console.WriteLine("Can't do that!");
      }
    }

  }
}