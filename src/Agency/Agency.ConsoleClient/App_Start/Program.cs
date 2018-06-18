namespace Agency.ConsoleClient
{
    using System;
    using Infrastructure;
    using Ninject;
    using NLog;
    using NLog.Config;
    using Services;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("start");

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            var serviceModule = new ServiceModule("name=DBConnection");
            var kernel = new StandardKernel(serviceModule);

            var agencyTerminal = kernel.Get<IAgencyWorkflowService>();

            agencyTerminal.Start();
        }
        
    }
}