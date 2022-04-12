﻿using System;
using System.Diagnostics;
using Konsole;

namespace BuildBackup
{
    /// <summary>
    /// Documentation :
    ///   https://wowdev.wiki/TACT
    ///   https://github.com/d07RiV/blizzget/wiki
    /// </summary>
    public class Program
    {
        private static TactProduct[] ProductsToProcess = new[]{ TactProducts.Starcraft2 };

        public static bool UseCdnDebugMode = true;
        public static bool WriteOutputFiles = true;
        
        public static void Main()
        {
            foreach (var product in ProductsToProcess)
            {
                ProductHandler.ProcessProduct(product, new Writer(), UseCdnDebugMode, WriteOutputFiles);
            }
            Console.WriteLine("Pre-load Complete!\n");

            if (Debugger.IsAttached)
            {
                Console.WriteLine("Press any key to continue . . .");
                Console.ReadLine();
            }
        }
    }
}
