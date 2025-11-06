namespace ST2_2025_Team5_InvoiceApp.Services
{
    // Design Pattern: Singleton
    // This service is registered as a singleton in Program.cs
    // but must have a public constructor so the DI system can instantiate it.
    public class LogService
    {
        public LogService() { } // ✅ публичен конструктор

        public void Log(string message)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {message}");
        }
    }
}
