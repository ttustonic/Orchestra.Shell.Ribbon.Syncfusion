﻿using Catel.IoC;
using Catel.Services;
using Orchestra.Services;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
public static partial class ModuleInitializer
{

    #region Methods
    /// <summary>
    /// Initializes the module.
    /// </summary>
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;
        serviceLocator.RegisterType<IShellService, ShellService>();
        serviceLocator.RegisterType<IApplicationInitializationService, ApplicationInitializationServiceBase>();
        serviceLocator.RegisterType<IShellConfigurationService, ShellConfigurationService>();
        serviceLocator.RegisterType<IPleaseWaitService, ProgressPleaseWaitService>();

        var languageService = serviceLocator.ResolveType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource(typeof(ShellService).Assembly.GetName().Name,
            "Orchestra.Properties", "Resources"));

        InitializeSpecific();
    }

    static void InitializeSpecific()
    {
        var serviceLocator = ServiceLocator.Default;

        var thirdPartyNoticesService = serviceLocator.ResolveType<IThirdPartyNoticesService>();
    }


    #endregion

}
