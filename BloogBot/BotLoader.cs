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
    class BotLoader
    {
        // change this to point to wherever Warrior.dll exists on your disk
        const string BOT_PATH = @"C:\Users\tommy\Documents\GitHub\Aigewen\Bots\FishingBot.dll";

        [Import(typeof(IBot))]
        IBot bot = null;

        AggregateCatalog catalog;
        CompositionContainer container;

        public BotLoader()
        {
            AppDomain.CurrentDomain.AssemblyResolve += (sender, args) => Assembly.GetExecutingAssembly();
        }

        internal IBot ReloadBot()
        {
            var assembly = Assembly.Load(File.ReadAllBytes(BOT_PATH));
            catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(assembly));
            container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            return bot;
        }
    }
}
