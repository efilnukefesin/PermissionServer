using System;
using SadConsole;
using Microsoft.Xna.Framework;
using Console = SadConsole.Console;
using ConsoleApp.Consoles;

namespace ConsoleApp
{
    public static class Program
    {
        private static MenuConsole menuConsole;

        [STAThread]
        static void Main()
        {
            // Setup the engine and create the main window.
            SadConsole.Game.Create(80, 25);

            // Hook the start event so we can add consoles to the system.
            SadConsole.Game.OnInitialize = Init;

            // Start the game.
            SadConsole.Game.Instance.Run();
            SadConsole.Game.Instance.Dispose();
        }

        static void Init()
        {
            // Any setup
            if (Settings.UnlimitedFPS)
                SadConsole.Game.Instance.Components.Add(new SadConsole.Game.FPSCounterComponent(SadConsole.Game.Instance));

            SadConsole.Game.Instance.Window.Title = "PermissionServer Demo Client";

            menuConsole = new MenuConsole();
            Global.CurrentScreen = menuConsole;
        }
    }
}
