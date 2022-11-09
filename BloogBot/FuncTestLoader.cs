using BloogBot.Game;
using BloogBot.AI;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;


namespace BloogBot
{
    class FuncTestLoader
    {
        // change this to point to wherever Warrior.dll exists on your disk
        const string PATH = @"C:\Users\tommy\Documents\GitHub\Aigewen\Bots\FuncTest.dll";

        [Import(typeof(IFuncTest))]
        IFuncTest funcTest = null;

        AggregateCatalog catalog;
        CompositionContainer container;

        public FuncTestLoader()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => Assembly.GetExecutingAssembly();
        }

        internal IFuncTest ReloadFuncTest()
        {
            var assembly = Assembly.Load(File.ReadAllBytes(PATH));
            catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(assembly));
            container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            return funcTest;
        }
    }
}
