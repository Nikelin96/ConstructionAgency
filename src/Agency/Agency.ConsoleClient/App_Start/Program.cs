namespace Agency.ConsoleClient
{
    using System;
    using BLL.Infrastructure;
    using DAL.Infrastructure;
    using Infrastructure;
    using Ninject;
    using Services;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("start");

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());

            var kernel = new StandardKernel(
                new ServiceModuleDal("name=DBConnection"),
                new ServiceModuleBll(),
                new ServiceModuleConsoleClient()
            );

            var agencyTerminal = kernel.Get<IAgencyWorkflowService>();

            agencyTerminal.Start();
        }

    }
}