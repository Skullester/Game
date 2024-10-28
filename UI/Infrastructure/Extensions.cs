using Models;
using Ninject;
using Ninject.Extensions.Conventions;

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

    public static void BindAllBaseClassesTo<T>(this IKernel? kernel, bool inSingletonScope = true)
    {
        kernel.Bind(x =>
        {
            var bindAllBaseClasses = x.FromThisAssembly()
                .Select(typeof(T).IsAssignableFrom)
                .BindAllBaseClasses();
            if (inSingletonScope)
                bindAllBaseClasses.Configure(y => y.InSingletonScope());
        });
    }

    public static void BindAllBaseClassesFromTo<TFromAssembly, TBind>(this IKernel? kernel,
        bool inSingletonScope = true)
    {
        kernel.Bind(x =>
        {
            var bindAllBaseClasses = x.FromAssemblyContaining<TFromAssembly>()
                .Select(typeof(TBind).IsAssignableFrom)
                .BindAllBaseClasses();
            if (inSingletonScope)
                bindAllBaseClasses.Configure(y => y.InSingletonScope());
        });
    }
}