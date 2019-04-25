using BootStrapper;
using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTestApp
{
    class Program
    {
        #region Properties

        private static Dictionary<ConsoleKeyInfo, SimpleMenuItem> menuEntries;
        private static bool isQuitting = false;

        #endregion Properties

        #region Methods

        #region Main
        static void Main(string[] args)
        {
            DiSetup.ConsoleApp();

            initMenu();

            while (!isQuitting)
            {
                renderMenu();
                ConsoleKeyInfo choice = Console.ReadKey(true);
                if (menuEntries.Keys.Any(x => x.Equals(choice)))
                {
                    menuEntries[choice].Action.Invoke();
                }
            }
        }
        #endregion Main

        #region initMenu
        private static void initMenu()
        {
            menuEntries = new Dictionary<ConsoleKeyInfo, SimpleMenuItem>();
            menuEntries.Add(new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false), new SimpleMenuItem("Get me an identity!", () => { requestIdentity(); }));
            menuEntries.Add(new ConsoleKeyInfo('q', ConsoleKey.Q, false, false, false), new SimpleMenuItem("Quit", () => { isQuitting = true; Console.WriteLine("Bye!"); }));
        }
        #endregion initMenu

        #region renderMenuEntry
        private static void renderMenuEntry(ConsoleKeyInfo keyInfo, SimpleMenuItem item)
        {
            Console.WriteLine($"{keyInfo.KeyChar}) {item.Caption}");
        }
        #endregion renderMenuEntry

        #region renderMenu
        private static void renderMenu()
        {
            Console.Clear();
            Console.WriteLine("MENU");
            Console.WriteLine("====");
            Console.WriteLine("");
            foreach (KeyValuePair<ConsoleKeyInfo, SimpleMenuItem> kvp in menuEntries)
            {
                renderMenuEntry(kvp.Key, kvp.Value);
            }
            Console.WriteLine("\nChoose wisely.");
        }
        #endregion renderMenu

        #region requestIdentity
        private static void requestIdentity()
        {
            var x = DiHelper.GetService<IIdentityService>();
        }
        #endregion requestIdentity

        #endregion Methods
    }
}
