using Models;
using Ninject;

namespace Infrastructure;

public static class MazeExtensions
{
    public static IEnumerable<char> ParseToChar(this IEnumerable<IMazeElement> elements, MazeFormatter formatter)
    {
        return elements.Select(formatter.Format);
    }
}

public static class KernelExtensions
{
    public static void RebindToConstant<T>(this IKernel kernel, T constant)
    {
        kernel.Rebind<T>().ToConstant(constant);
    }
}