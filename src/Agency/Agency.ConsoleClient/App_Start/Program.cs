﻿namespace Agency.ConsoleClient
{
    using System;
    using Agency.BLL.Services;
    using Infrastructure;
    using Ninject;
    using Services;
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("start");

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            var serviceModule = new ServiceModule("name=DBConnection");
            var kernel = new StandardKernel(serviceModule);

            var agencyTerminal = new AgencyWorkflowService(kernel.Get<IConsoleService>(), kernel.Get<IApartmentWorkflowService>());

            try
            {
                agencyTerminal.StartEditLoop();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                //throw;

                // todo: log error
                return;
            }
        }
    }
}