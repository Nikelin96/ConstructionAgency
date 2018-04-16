namespace Agency.ConsoleClient
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Agency.BLL.Infrastructure;
    using Agency.BLL.Services;
    using Agency.DAL.EF;
    using Agency.DAL.Interfaces;
    using Agency.DAL.Model.Entities;
    using AutoMapper;
    using DotNetCraft.Common.DataAccessLayer.UnitOfWorks.SimpleUnitOfWorks;
    using Ninject;
    using Ninject.Activation;
    using Ninject.Parameters;

    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("start");

            AppDomain.CurrentDomain.SetData("DataDirectory", System.IO.Directory.GetCurrentDirectory());
            var serviceModule = new ServiceModule("name=DBConnection");
            var kernel = new StandardKernel(serviceModule);

            var agencyTerminal = new AgencyTerminal(kernel.Get<IApartmentService>());

            agencyTerminal.Start();
        }
    }
}