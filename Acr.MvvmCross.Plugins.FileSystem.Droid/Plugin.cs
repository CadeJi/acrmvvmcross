using System;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;


namespace Acr.MvvmCross.Plugins.FileSystem.Droid {

    public class Plugin : IMvxPlugin {

        public void Load() {
            Mvx.RegisterSingleton<IFileSystem>(new FileSystemImpl());
			Mvx.RegisterSingleton<IFileViewer>(new FileViewerImpl());
        }
    }
}