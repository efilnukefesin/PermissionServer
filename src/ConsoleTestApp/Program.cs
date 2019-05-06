using BootStrapper;
using Interfaces;
using Models;
using PermissionServer.Client.Interfaces;
using PermissionServer.Models;
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
        private static string message;

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
                    menuEntries[choice].Execute();
                }
            }
        }
        #endregion Main

        #region initMenu: inits the menu, adds menu items
        /// <summary>
        /// inits the menu, adds menu items
        /// </summary>
        private static void initMenu()
        {
            menuEntries = new Dictionary<ConsoleKeyInfo, SimpleMenuItem>();
            menuEntries.Add(new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false), new SimpleMenuItem("Get me an identity!", () => { bool wasSuccessful = requestIdentity(); if (wasSuccessful) { message = "Identity fetched!"; menuEntries[new ConsoleKeyInfo('1', ConsoleKey.D1, false, false, false)].IsActive = false; menuEntries[new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false)].IsActive = true; menuEntries[new ConsoleKeyInfo('3', ConsoleKey.D3, false, false, false)].IsActive = true; menuEntries[new ConsoleKeyInfo('4', ConsoleKey.D4, false, false, false)].IsActive = true; menuEntries[new ConsoleKeyInfo('g', ConsoleKey.G, false, false, false)].IsActive = true; } else { message = "Identity NOT fetched!"; } }));  //TODO: inlining is bad; find a better way to do this
            menuEntries.Add(new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false), new SimpleMenuItem("Get Permissions", () => { bool wasSuccessful = requestPermissions(); if (wasSuccessful) { message = "User fetched!"; menuEntries[new ConsoleKeyInfo('2', ConsoleKey.D2, false, false, false)].IsActive = false; } }, false));
            menuEntries.Add(new ConsoleKeyInfo('3', ConsoleKey.D3, false, false, false), new SimpleMenuItem("Request SuperHotFeatureServer", () => { bool wasSuccessful = requestSuperHotFeatureServer(); if (wasSuccessful) { /*message = "Values fetched!";*/ } }, false));
            menuEntries.Add(new ConsoleKeyInfo('4', ConsoleKey.D4, false, false, false), new SimpleMenuItem("Request SuperHotOtherFeatureServer", () => { bool wasSuccessful = requestSuperHotOtherFeatureServer(); if (wasSuccessful) { /*message = "Values fetched!";*/ } }, false));
            menuEntries.Add(new ConsoleKeyInfo('g', ConsoleKey.G, false, false, false), new SimpleMenuItem("GetGivenPermissions on Permission Server", () => { bool wasSuccessful = requestGetGivenPermissionsPermissionServer(); if (wasSuccessful) { /*message = "Values fetched!";*/ } }, false));
            menuEntries.Add(new ConsoleKeyInfo('q', ConsoleKey.Q, false, false, false), new SimpleMenuItem("Quit", () => { quit(); }));
        }
        #endregion initMenu

        #region renderMenuEntry: renders a menu entry
        /// <summary>
        /// renders a menu entry
        /// </summary>
        /// <param name="keyInfo">the key info to help the user find the best key for the entry</param>
        /// <param name="item">the Menu Item to show</param>
        private static void renderMenuEntry(ConsoleKeyInfo keyInfo, SimpleMenuItem item)
        {
            ConsoleColor color = ConsoleColor.White;
            if (!item.IsActive)
            {
                color = ConsoleColor.Gray;
            }
            Console.ForegroundColor = color;
            Console.WriteLine($"{keyInfo.KeyChar}) {item.Caption}");
        }
        #endregion renderMenuEntry

        #region renderMenu: renders the menu for the app
        /// <summary>
        /// renders the menu for the app
        /// </summary>
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

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"\n{message}");

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\nChoose wisely.");
        }
        #endregion renderMenu

        #region quit: quits the app in a nice manner
        /// <summary>
        /// quits the app in a nice manner
        /// </summary>
        private static void quit()
        {
            isQuitting = true;
            message = "Bye!";
        }
        #endregion quit

        #region requestIdentity: requests an identity from the identity server
        /// <summary>
        /// requests an identity from the identity server
        /// </summary>
        /// <returns>true, if successful</returns>
        private static bool requestIdentity()
        {
            bool result = DiHelper.GetService<IIdentityService>().FetchIdentity();
            return result;
        }
        #endregion requestIdentity

        #region requestPermissions
        private static bool requestPermissions()
        {
            PermissionServer.SDK.Client permissionServerClient = DiHelper.GetService<PermissionServer.SDK.Client>(DiHelper.GetService<IConfigurationService>().PermissionServerEndpoint, DiHelper.GetService<ISessionService>().AccessToken);
            var requestResult = permissionServerClient.GetUserAsync();
            return requestResult != null;
        }
        #endregion requestPermissions

        #region requestSuperHotFeatureServer
        private static bool requestSuperHotFeatureServer()
        {
            bool result = false;

            SuperHotFeatureServer.SDK.Client superHotFeatureServerClient = DiHelper.GetService<SuperHotFeatureServer.SDK.Client>(DiHelper.GetService<IConfigurationService>().SuperHotFeatureServerEndpoint, DiHelper.GetService<ISessionService>().AccessToken);
            string requestResult = superHotFeatureServerClient.GetValueAsync().Result;

            if (requestResult is string)
            {
                message = $"Fetched API Value successfully: '{requestResult}'";
                result = true;
            }
            return result;
        }
        #endregion requestSuperHotFeatureServer

        #region requestSuperHotOtherFeatureServer
        private static bool requestSuperHotOtherFeatureServer()
        {
            bool result = false;

            SuperHotOtherFeatureServer.SDK.Client superHotOtherFeatureServerClient = DiHelper.GetService<SuperHotOtherFeatureServer.SDK.Client>(DiHelper.GetService<IConfigurationService>().SuperHotOtherFeatureServerEndpoint, DiHelper.GetService<ISessionService>().AccessToken);
            string requestResult = superHotOtherFeatureServerClient.GetValueAsync().Result;

            if (requestResult is string)
            {
                message = $"Fetched API Value successfully: '{requestResult}'";
                result = true;
            }
            return result;
        }
        #endregion requestSuperHotOtherFeatureServer

        #region requestGetGivenPermissionsPermissionServer
        private static bool requestGetGivenPermissionsPermissionServer()
        {
            bool result = false;

            PermissionServer.SDK.Client permissionServerClient = DiHelper.GetService<PermissionServer.SDK.Client>(DiHelper.GetService<IConfigurationService>().PermissionServerEndpoint, DiHelper.GetService<ISessionService>().AccessToken);
            IEnumerable<Permission> requestResult = permissionServerClient.GetGivenPermissionsAsync().Result;

            if (requestResult is IEnumerable<Permission>)
            {
                message = $"Fetched API Value successfully: '{requestResult}'";
                result = true;
            }
            return result;
        }
        #endregion requestGetGivenPermissionsPermissionServer

        #endregion Methods
    }
}
