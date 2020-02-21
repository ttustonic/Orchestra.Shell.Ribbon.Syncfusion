// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainViewModel.cs" company="WildGums">
//   Copyright (c) 2008 - 2014 WildGums. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Orchestra.Examples.Ribbon.ViewModels
{
    using Catel.MVVM;

    public class MainViewModel : ViewModelBase
    {
        public string Version { get; set; }

        public MainViewModel()
        {
#if NETCOREAPP
            Version = "Core";
#else
            Version = "Framework";
#endif
        }

    }
}