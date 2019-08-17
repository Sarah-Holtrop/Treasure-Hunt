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
// Bow- start,  (Go down)
// Crew's quarters-2nd room
// Captain's quarters(stern) - key

// Stores- need key to get in- treasure

// Stretch Goals-
//  Crow's nest- fun view
//  Compass- bonus treasure

