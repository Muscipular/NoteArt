using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Composition.Hosting.Core;
using NoteArt.ViewModel;
using NoteArt.ViewModel.Windows;

namespace NoteArt.Lib
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator : IDisposable
    {
        CompositionHost container;

        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            container = new ContainerConfiguration()
                .WithAssembly(typeof(ViewModelLocator).Assembly)
                .CreateContainer();
        }

        public TExport GetExport<TExport>()
        {
            return container.GetExport<TExport>();
        }

        public TExport GetExport<TExport>(string contractName)
        {
            return container.GetExport<TExport>(contractName);
        }

        public bool TryGetExport(Type exportType, string contractName, out object export)
        {
            return container.TryGetExport(exportType, contractName, out export);
        }

        public bool TryGetExport(Type exportType, out object export)
        {
            return container.TryGetExport(exportType, out export);
        }

        public bool TryGetExport<TExport>(out TExport export)
        {
            return container.TryGetExport(out export);
        }

        public bool TryGetExport<TExport>(string contractName, out TExport export)
        {
            return container.TryGetExport(contractName, out export);
        }

        public object GetExport(Type exportType)
        {
            return container.GetExport(exportType);
        }

        public object GetExport(Type exportType, string contractName)
        {
            return container.GetExport(exportType, contractName);
        }

        public object GetExport(CompositionContract contract)
        {
            return container.GetExport(contract);
        }

        public IEnumerable<object> GetExports(Type exportType)
        {
            return container.GetExports(exportType);
        }

        public IEnumerable<object> GetExports(Type exportType, string contractName)
        {
            return container.GetExports(exportType, contractName);
        }

        public IEnumerable<TExport> GetExports<TExport>()
        {
            return container.GetExports<TExport>();
        }

        public IEnumerable<TExport> GetExports<TExport>(string contractName)
        {
            return container.GetExports<TExport>(contractName);
        }

        public bool TryGetExport(CompositionContract contract, out object export)
        {
            return container.TryGetExport(contract, out export);
        }

        public void Dispose()
        {
            container.Dispose();
        }

//        public static ViewModelLocator _ModelLocator;
        public static ViewModelLocator Locator { get { return App.Current.Resources["Locator"] as ViewModelLocator; } }


        public MainViewModel Main
        {
            get { return container.GetExport<MainViewModel>(); }
        }

        public MainVM MainVM
        {
            get { return container.GetExport<MainVM>(); }
        }
    }
}