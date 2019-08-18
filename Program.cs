using System;
using PirateShip.Project;

namespace PirateShip
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // instantiate project (App app = new App)
      GameService gs = new GameService();
      gs.Setup();
      gs.StartGame();
    }
  }
}


// TODO Rooms- Create, add items, add exits
// Bow- compass
// Crow's nest- spyglass
// Gangway- null
// Stern- null
// Poopdeck- smuggled coins 
// Crew- Gold dubloon
// Captain- cursed medallion
// Hold(change from stores)- gems


// TODO Rewrite UseItem fn, each item has a value, which is added to total findings when you give item back (use) to ghost of captain,
// If you use cursed medallion, you lose all your money, and all items are put back 

