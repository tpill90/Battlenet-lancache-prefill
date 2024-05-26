﻿global using BattleNetPrefill.EncryptDecrypt;
global using BattleNetPrefill.Extensions;
global using BattleNetPrefill.Handlers;
global using BattleNetPrefill.Parsers;
global using BattleNetPrefill.Structs;
global using BattleNetPrefill.Structs.Enums;
global using BattleNetPrefill.Utils;
global using BattleNetPrefill.Utils.Debug;
global using BattleNetPrefill.Web;
global using ByteSizeLib;
global using CliFx;
global using CliFx.Attributes;
global using CliFx.Exceptions;
global using CliFx.Extensibility;
global using CliFx.Infrastructure;
global using JetBrains.Annotations;
global using LancachePrefill.Common;
global using LancachePrefill.Common.Enums;
global using LancachePrefill.Common.Extensions;
global using LancachePrefill.Common.SelectAppsTui;
global using Microsoft.IO;
global using Polly;
global using Polly.Retry;
global using Spectre.Console;
global using System;
global using System.Buffers.Binary;
global using System.Collections;
global using System.Collections.Concurrent;
global using System.Collections.Generic;
global using System.Diagnostics;
global using System.Diagnostics.CodeAnalysis;
global using System.IO;
global using System.IO.Compression;
global using System.Linq;
global using System.Net.Http;
global using System.Net.Http.Headers;
global using System.Runtime.CompilerServices;
global using System.Text;
global using System.Text.Json;
global using System.Text.Json.Serialization;
global using System.Text.RegularExpressions;
global using System.Threading;
global using System.Threading.Tasks;
global using Terminal.Gui;
global using static LancachePrefill.Common.SpectreFormatters;
global using AnsiConsoleExtensions = LancachePrefill.Common.Extensions.AnsiConsoleExtensions;