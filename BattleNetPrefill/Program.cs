﻿namespace BattleNetPrefill
{
    // TODO - Add publish build pipeline 
    // TODO - Add summary table
    // TODO - Setup mkdocs and copy from SteamPrefill.  Update docs in general
    // TODO - Dedupe codebase with other prefills as well
    public static class Program
    {
        private const string Description = "Automatically fills a Lancache with games from Battle.net, so that subsequent downloads will be \n" +
                                                     "  served from the Lancache, improving speeds and reducing load on your internet connection.";

        public static async Task<int> Main()
        {
            // Checking to see if the user double clicked the exe in Windows, and display a message on how to use the app
            OperatingSystemUtils.DetectDoubleClickOnWindows("BattleNetPrefill");

            //TODO dedupe exception handling at the top level. 
            var cliArgs = ParseHiddenFlags();
            return await new CliApplicationBuilder()
                         .AddCommandsFromThisAssembly()
                         .SetTitle("BattleNetPrefill")
                         .SetExecutableNamePlatformAware("BattleNetPrefill")
                         .SetDescription(Description)
                         .SetVersion($"v{ThisAssembly.Info.InformationalVersion}")
                         .Build()
                         .RunAsync(cliArgs);
        }

        /// <summary>
        /// Adds hidden flags that may be useful for debugging/development, but shouldn't be displayed to users in the help text
        /// </summary>
        private static List<string> ParseHiddenFlags()
        {
            // Have to skip the first argument, since its the path to the executable
            var args = Environment.GetCommandLineArgs().Skip(1).ToList();

            // Enables SteamKit2 debugging as well as SteamPrefill verbose logs
            if (args.Any(e => e.Contains("--compare-requests")))
            {
                AnsiConsole.Console.LogMarkupLine($"Using {LightYellow("--compare-requests")} flag.  Running comparison logic...");
                // Need to enable SkipDownloads as well in order to get this to work well
                AppConfig.CompareAgainstRealRequests = true;
                args.Remove("--compare-requests");
            }

            // Will skip over downloading logic.  Will only download manifests
            if (args.Any(e => e.Contains("--no-download")))
            {
                AnsiConsole.Console.LogMarkupLine($"Using {LightYellow("--no-download")} flag.  Will skip downloading chunks...");
                AppConfig.SkipDownloads = true;
                args.Remove("--no-download");
            }

            return args;
        }
    }
}