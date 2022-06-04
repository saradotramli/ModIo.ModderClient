using Microsoft.Extensions.Configuration;
using NLog;

namespace ModIoModderClient
{

    internal static class Program
    {
        public static IConfiguration? Configuration;

        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Add handler to handle the exception raised by main threads
            Application.ThreadException +=
            new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            // Add handler to handle the exception raised by additional threads
            AppDomain.CurrentDomain.UnhandledException +=
            new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            Configuration = builder.Build();

            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new FormMain());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            ShowExceptionDetails(e.ExceptionObject as Exception);

            // Suspend the current thread for now to stop the exception from throwing.
            Thread.CurrentThread.Suspend();
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            ShowExceptionDetails(e.Exception);
        }

        static void ShowExceptionDetails(Exception ex)
        {
            // Do logging of exception details
            MessageBox.Show(ex.Message, "Unexpected error occured",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

            var logger = LogManager.GetCurrentClassLogger();
            logger.Error(ex, "Unhandled error");
        }
    }
}