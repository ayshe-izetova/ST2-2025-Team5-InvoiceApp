namespace ST2_2025_Team5_InvoiceApp.Services
{
    // Design Pattern: Singleton
    // Ensures there is only one shared instance of LogService in the entire application.
    public class LogService
    {
        private static LogService? _instance;
        public static LogService Instance => _instance ??= new LogService();

        private LogService() { }

        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}
