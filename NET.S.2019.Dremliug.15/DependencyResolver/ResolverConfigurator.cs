using Ninject;

namespace DependencyResolver
{
    public static class ResolverConfigurator
    {
        public static void Configure(ref IKernel kernel, bool useFake)
        {
            if (useFake)
            {
                kernel = new StandardKernel(new ConfigModules.FakeModule());
            }
        }
    }
}
