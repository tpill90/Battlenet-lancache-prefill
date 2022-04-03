﻿using System;
using System.Collections.Generic;
using ByteSizeLib;
using Spectre.Console;

namespace Shared.Models
{
    //TODO comment what these fields mean
    public class ComparisonResult
    {
        public int RequestMadeCount { get; set; }
        public int DuplicateRequests { get; set; }

        public int RealRequestCount { get; set; }

        public List<Request> Misses { get; set; }
        public List<Request> UnnecessaryRequests { get; set; }

        public ByteSize RequestTotalSize { get; set; }
        public ByteSize RealRequestsTotalSize { get; set; }

        public int RequestsWithoutSize { get; set; }
        public int RealRequestsWithoutSize { get; set; }

        public int MissCount => Misses.Count;

        public void PrintOutput()
        {
            // Formatting output to table
            var table = new Table();
            table.AddColumn(new TableColumn("").LeftAligned());
            table.AddColumn(new TableColumn(SpectreColors.Blue("Current")).Centered());
            table.AddColumn(new TableColumn(SpectreColors.Blue("Expected")).Centered());
            //TODO
            // table.AddColumn(new TableColumn(SpectreColors.Blue("Matches")).Centered()); , ((char)0x2713).ToString()

            table.AddRow("Requests made", RequestMadeCount.ToString(), RealRequestCount.ToString());
            table.AddRow("Bandwidth required", RequestTotalSize.ToString(), RealRequestsTotalSize.ToString());
            table.AddRow("Requests missing size", RequestsWithoutSize.ToString(), RealRequestsWithoutSize.ToString());
            AnsiConsole.Write(table);

            Console.WriteLine($"Total Misses : {Colors.Red(MissCount)}");
            Console.WriteLine($"Unnecessary Requests : {Colors.Yellow(UnnecessaryRequests.Count)}");
            Console.WriteLine($"Total Dupes : {Colors.Yellow(DuplicateRequests)}");
            Console.WriteLine();
        }
    }

    public class DiffResults
    {
        public List<ComparedRequest> MatchedRequests { get; set; }
        public List<ComparedRequest> MissedRequests { get; set; }

        public List<Request> UnnecessaryRequests { get; set; }
    }
}