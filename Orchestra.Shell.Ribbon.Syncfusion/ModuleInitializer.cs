using Catel;
using Catel.IoC;
using Catel.Services;
using Orc.Theming;
using Orchestra.Services;

/// <summary>
/// Used by the ModuleInit. All code inside the Initialize method is ran as soon as the assembly is loaded.
/// </summary>
[System.Diagnostics.CodeAnalysis.SuppressMessage("Major Bug", "S3903:Types should be defined in named namespaces", Justification = "<Pending>")]
public static partial class ModuleInitializer
{
    #region Methods
    /// <summary>
    /// Initializes the module.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Minor Code Smell", "S3251:Implementations should be provided for \"partial\" methods", Justification = "<Pending>")]
    public static void Initialize()
    {
        var serviceLocator = ServiceLocator.Default;

        serviceLocator.RegisterType<IAccentColorService, SfAccentColorService>();
        serviceLocator.RegisterType<IBaseColorSchemeService, SfBaseColorService>();

        serviceLocator.RegisterType<IShellService, ShellService>();
        serviceLocator.RegisterType<IApplicationInitializationService, ApplicationInitializationServiceBase>();
        serviceLocator.RegisterType<IShellConfigurationService, ShellConfigurationService>();
        serviceLocator.RegisterType<IPleaseWaitService, ProgressPleaseWaitService>();
        serviceLocator.RegisterType<IXamlResourceService, XamlResourceService>();

        var languageService = serviceLocator.ResolveType<ILanguageService>();
        languageService.RegisterLanguageSource(new LanguageResourceSource(typeof(ShellService).Assembly.GetName().Name,
            "Orchestra.Properties", "Resources"));
        InitializeSpecific();
    }

    static partial void InitializeSpecific();

    #endregion

}
