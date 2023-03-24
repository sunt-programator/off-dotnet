// <copyright file="Program.cs" company="Sunt Programator">
// Copyright (c) Sunt Programator. All rights reserved.
// Licensed under the GPL-3.0 license. See LICENSE file in the project root for full license information.
// </copyright>

using Off.Net.Pdf.Core.Benchmarks;
#if !DEBUG
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters.Json;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Running;
#endif

#if DEBUG
const int pagesCount = 10000;

SimplePdfBenchmarks simplePdfBenchmarks = new();
simplePdfBenchmarks.Setup();

var stream = simplePdfBenchmarks.BasicPdfWithMultiplePagesAndDifferentContentStreams(pagesCount);

if (stream == null)
{
    return;
}

string filePath = Path.Combine(Environment.CurrentDirectory, $"off.net-{pagesCount}.pdf");
FileStream fileStream = new(filePath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
fileStream.SetLength(0);

stream.Position = 0;
await stream.CopyToAsync(fileStream).ConfigureAwait(false);
await fileStream.DisposeAsync().ConfigureAwait(false);

simplePdfBenchmarks.Cleanup();

#else

var config = DefaultConfig.Instance
    .AddDiagnoser(MemoryDiagnoser.Default)
    .AddLogger(ConsoleLogger.Default)
    .AddExporter(JsonExporter.Default);
BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args, config);

#endif
