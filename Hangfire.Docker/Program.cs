using System;
using System.Threading;
using Hangfire;
using Hangfire.PostgreSql;
namespace Hangfire.Docker
{
    class Program
    {
        private static readonly AutoResetEvent autoResetEvent = new AutoResetEvent(false);
        static void Main(string[] args)
        {

            GlobalConfiguration.Configuration.UsePostgreSqlStorage(Environment.GetEnvironmentVariable("HANGFIREDB_CONN"));

            using (var server = new BackgroundJobServer())
            {
                Console.WriteLine("Hangfire Server started.");

                RecurringJob.AddOrUpdate(() => Run()
                , Cron.Minutely);

                AppDomain.CurrentDomain.ProcessExit += (o, e) =>
                {
                    server.Dispose();
                    Console.WriteLine("Terminating...");
                    autoResetEvent.Set();
                };

                autoResetEvent.WaitOne();
            }
        }

        public static void Run()
        {
            Console.WriteLine("Minute Job");
        }
    }
}
