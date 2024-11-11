using Models;
using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;

namespace Infrastructure;

public static class MazeExtensions
{
    public static IEnumerable<(char sym, IMazeElement el)> ParseToCharColor(this IEnumerable<IMazeElement> elements,
        MazeFormatter formatter)
    {
        return elements.Select(x => (formatter.Format(x), x));
    }
}

public static class KernelExtensions
{
    public static void BindToConstant<TBind>(this IKernel kernel, params TBind[] constants)
    {
        foreach (var constant in constants)
        {
            kernel.Bind<TBind>()
                .ToConstant(constant);
        }
    }

    public static void RebindToConstant<T>(this IKernel kernel, T constant)
    {
        kernel.Rebind<T>().ToConstant(constant);
    }

    public static void BindAllBaseClassesTo<TBind>(this IKernel? kernel, bool inSingletonScope = true)
    {
        kernel.Bind(x => x.FromThisAssembly()
            .SyntaxBindHelper<TBind>(inSingletonScope));
    }

    public static void BindAllBaseClassesFromTo<TFromAssembly, TBind>(this IKernel? kernel,
        bool inSingletonScope = true)
    {
        kernel.Bind(x => x.FromAssemblyContaining<TFromAssembly>()
            .SyntaxBindHelper<TBind>(inSingletonScope));
    }

    private static void SyntaxBindHelper<TBind>(this IIncludingNonPublicTypesSelectSyntax syntax,
        bool inSingletonScope)
    {
        var bindAllBaseClasses = syntax
            .Select(typeof(TBind).IsAssignableFrom)
            .BindAllBaseClasses();
        if (inSingletonScope)
            bindAllBaseClasses.Configure(y => y.InSingletonScope());
    }
}