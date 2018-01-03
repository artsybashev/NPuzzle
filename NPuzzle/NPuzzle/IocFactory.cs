using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MicroResolver;

namespace Amv.NPuzzle.Console
{
    public static class IocProvider
    {
        private static readonly Lazy<ObjectResolver> Resolver = new Lazy<ObjectResolver>(Initialize);

        private static ObjectResolver Initialize()
        {
            var container = ObjectResolver.Create();
            Configure(container);
            return container;
        }

        public static ObjectResolver Container => Resolver.Value;

        public static void Configure(ObjectResolver container)
        {
            //container.Register<>();
        }
    }
}
