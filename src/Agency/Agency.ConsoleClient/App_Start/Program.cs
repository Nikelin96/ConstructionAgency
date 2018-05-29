namespace Agency.ConsoleClient
{
    using System;
    using System.Security.Cryptography.X509Certificates;
    using Agency.BLL.Services;
    using BLL.DTOs;
    using Infrastructure;
    using Ninject;
    using Services;
    using Services.Commands;
    using Services.Factories;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("start");

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            var serviceModule = new ServiceModule("name=DBConnection");
            var kernel = new StandardKernel(serviceModule);

            var consoleService = kernel.Get<IConsoleService>();

            var agencyTerminal = new AgencyWorkflowService(kernel.Get<ICommandFactory<ApartmentEditDto>>(), consoleService);

            try
            {
                agencyTerminal.StartEditLoop();
            }
            catch (Exception e)
            {
                consoleService.Print(e);
                //throw;

                // todo: log error
                return;
            }
        }


    }
}