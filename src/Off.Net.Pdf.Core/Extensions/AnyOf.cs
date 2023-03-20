using Off.Net.Pdf.Core.Interfaces;
using Off.Net.Pdf.Core.Primitives;

namespace Off.Net.Pdf.Core.Extensions;

public class AnyOf<T1, T2> where T1 : IPdfObject where T2 : IPdfObject
{
    private readonly T1? _firstType;
    private readonly T2? _secondType;

    public AnyOf(T1 type)
    {
        _firstType = type;
    }

    public AnyOf(T2 type)
    {
        _secondType = type;
    }

    public IPdfObject PdfObject
    {
        get
        {
            if (_firstType != null)
            {
                return _firstType;
            }

            if (_secondType != null)
            {
                return _secondType;
            }

            return new PdfNull();
        }
    }

    public static implicit operator AnyOf<T1, T2>(T1 type1)
    {
        return new AnyOf<T1, T2>(type1);
    }

    public static implicit operator AnyOf<T1, T2>(T2 type2)
    {
        return new AnyOf<T1, T2>(type2);
    }
}
