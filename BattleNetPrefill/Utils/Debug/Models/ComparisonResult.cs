﻿using System;
using System.Collections.Generic;
using System.Linq;
using ByteSizeLib;
using Spectre.Console;
using static BattleNetPrefill.Utils.SpectreColors;

namespace BattleNetPrefill.Utils.Debug.Models
{
    public class ComparisonResult
    {
        /// <summary>
        /// The total amount of time taken for the prefill process to run.  Excludes time taken to run comparison logic.
        /// </summary>
        public TimeSpan ElapsedTime { get; set; }

        /// <summary>
        /// Total number of requests made by our application
        /// </summary>
        public int RequestMadeCount { get; init; }

        /// <summary>
        /// Number of requests made by the actual Battle.Net client.
        /// </summary>
        public int RealRequestCount { get; init; }

        /// <summary>
        /// This is the remainder of any "real" requests that should have been made, leftover from subtracting generated requests from the real requests.
        /// </summary>
        public List<Request> Misses { get; set; }
        public ByteSize MissedBandwidth => Misses.SumTotalBytes();
        public int MissCount => Misses.Count;

        /// <summary>
        /// Requests that were generated by our application, that the real Battle.Net client does not do.
        /// </summary>
        public List<Request> UnnecessaryRequests { get; set; }
        public ByteSize WastedBandwidth => UnnecessaryRequests.SumTotalBytes();
        public int UnnecessaryRequestCount => UnnecessaryRequests.Count;

        /// <summary>
        /// Total bandwidth usage of the requests generated by our app.
        /// </summary>
        public ByteSize GeneratedRequestTotalSize { get; set; }

        /// <summary>
        /// Total bandwidth usage of the requests generated by the Battle.Net client
        /// </summary>
        public ByteSize RealRequestsTotalSize { get; set; }

        /// <summary>
        /// Number of requests generated by our app, that don't have a byte range.  These are all "DownloadWholeFile" requests
        /// </summary>
        public int RequestsWithoutSize { get; set; }

        /// <summary>
        /// Number of requests from the real Battle.Net client, that don't have a byte range. 
        /// </summary>
        public int RealRequestsWithoutSize { get; set; }

        public void PrintOutput()
        {
            // Formatting output to table
            var table = new Table();
            table.AddColumn(new TableColumn("").LeftAligned());
            table.AddColumn(new TableColumn(Blue("Current")));
            table.AddColumn(new TableColumn(Blue("Expected")).Centered());

            table.AddRow("Requests made", RequestMadeCount.ToString(), RealRequestCount.ToString());
            table.AddRow("Bandwidth required", GeneratedRequestTotalSize.ToString(), RealRequestsTotalSize.ToString());
            table.AddRow("Requests missing size", Yellow(RequestsWithoutSize.ToString()), RealRequestsWithoutSize.ToString());

            table.AddRow("Misses", Red(MissCount), "");
            table.AddRow("Misses Bandwidth", Red(MissedBandwidth), "");

            table.AddRow("Unnecessary Requests", Yellow(UnnecessaryRequestCount), "");
            table.AddRow("Wasted Bandwidth", Yellow(WastedBandwidth), "");
            AnsiConsole.Write(table);

            if (MissCount > 0)
            {
                AnsiConsole.MarkupLine(Red("Missed Requests :"));
                foreach (var miss in Misses.Take(10))
                {
                    AnsiConsole.WriteLine($"{miss}");
                }
            }

            if (UnnecessaryRequestCount > 0)
            {
                AnsiConsole.MarkupLine(Yellow("Unnecessary Requests :"));
                foreach (var req in UnnecessaryRequests.Take(10))
                {
                    AnsiConsole.WriteLine($"{req}");
                }
            }

            AnsiConsole.WriteLine();
        }
    }
}