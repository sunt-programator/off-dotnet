using BenchmarkDotNet.Attributes;
using Off.Net.Pdf.Core.CommonDataStructures;

namespace Off.Net.Pdf.Core.Benchmarks.CommonDataStructures;

public class RectangleBenchmarks
{
    [Benchmark]
    public Rectangle BasicRectangle()
    {
        return new Rectangle(123, 46, 456, 72);
    }

    [Benchmark]
    public ReadOnlyMemory<byte> BasicRectangle_Bytes()
    {
        return new Rectangle(123, 46, 456, 72).Bytes;
    }

    [Benchmark]
    public int BasicRectangle_Length()
    {
        return new Rectangle(123, 46, 456, 72).Length;
    }

    [Benchmark]
    public string BasicRectangle_Content()
    {
        return new Rectangle(123, 46, 456, 72).Content;
    }
}
