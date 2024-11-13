using Ninject;
using Ninject.Extensions.Conventions;
using Ninject.Extensions.Conventions.Syntax;

namespace Extensions;

public static class KernelEx
{
    #region BindConstant

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

    #endregion

    #region BindClasses

    public static void BindAllBaseClassesTo<TBind>(this IKernel? kernel, bool inSingletonScope = true)
    {
        kernel.Bind(x => x.FromThisAssembly()
            .SyntaxBindHelperClasses<TBind>(inSingletonScope));
    }

    public static void BindAllBaseClassesFromTo<TFromAssembly, TBind>(this IKernel? kernel,
        bool inSingletonScope = true)
    {
        kernel.Bind(x => x.FromAssemblyContaining<TFromAssembly>()
            .SyntaxBindHelperClasses<TBind>(inSingletonScope));
    }

    private static void SyntaxBindHelperClasses<TBind>(this IIncludingNonPublicTypesSelectSyntax syntax,
        bool inSingletonScope)
    {
        var bindAllBaseClasses = syntax
            .Select(typeof(TBind).IsAssignableFrom)
            .BindAllBaseClasses();
        if (inSingletonScope)
            bindAllBaseClasses.Configure(y => y.InSingletonScope());
    }

    #endregion

    #region BindInterfaces

    public static void BindAllInterfacesFromTo<TFromAssembly, TBind>(this IKernel? kernel,
        bool inSingletonScope = true)
    {
        kernel.Bind(x => x.FromAssemblyContaining<TFromAssembly>()
            .SyntaxBindHelperInterfaces<TBind>(inSingletonScope));
    }

    public static void BindAllInterfacesTo<TBind>(this IKernel? kernel, bool inSingletonScope = true)
    {
        kernel.Bind(x => x.FromThisAssembly()
            .SyntaxBindHelperInterfaces<TBind>(inSingletonScope));
    }

    private static void SyntaxBindHelperInterfaces<TBind>(this IIncludingNonPublicTypesSelectSyntax syntax,
        bool inSingletonScope)
    {
        var bindAllBaseClasses = syntax
            .Select(typeof(TBind).IsAssignableFrom)
            .BindAllInterfaces();
        if (inSingletonScope)
            bindAllBaseClasses.Configure(y => y.InSingletonScope());
    }

    #endregion
}