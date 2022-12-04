using Off.Net.Pdf.Core.Interfaces;

namespace Off.Net.Pdf.Core.Primitives;

public class EitherType<T1, T2> where T1 : IPdfObject where T2 : IPdfObject
{
    private readonly T1? _firstType;
    private readonly T2? _secondType;

    public EitherType(T1 type)
    {
        _firstType = type;
    }

    public EitherType(T2 type)
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

    public static implicit operator EitherType<T1, T2>(T1 type1)
    {
        return new EitherType<T1, T2>(type1);
    }

    public static implicit operator EitherType<T1, T2>(T2 type2)
    {
        return new EitherType<T1, T2>(type2);
    }
}
