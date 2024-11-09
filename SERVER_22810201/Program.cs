using Microsoft.Extensions.DependencyInjection;

namespace SERVER_22810201
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            // WinForm
            ApplicationConfiguration.Initialize();

            // Tiêm phụ thuộc
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();

            // Khởi chạy ứng dụng
            var mainForm = serviceProvider.GetRequiredService<SERVER_22810201>();
            Application.Run(mainForm);
        }

        private static void ConfigureServices(ServiceCollection services)
        {
            services
                .AddSingleton<SERVER_22810201>()
                .AddTransient<ClientHandler>()
                .AddTransient<IApplicationProviderService, ApplicationProviderService>()
                .AddTransient<IProcessProviderService, ProcessProviderService>()
                .AddTransient<IShutdownService, ShutdownService>()
                .AddTransient<IScreenshotService, ScreenshotService>()
                .AddTransient<IKeyloggerService, KeyloggerService>()
                .AddTransient<IFileService, FileService>();
        }
    }
}
