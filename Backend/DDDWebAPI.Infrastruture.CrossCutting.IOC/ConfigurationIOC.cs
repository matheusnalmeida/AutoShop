using Autofac;
using AutoShop.Application.Interfaces;
using AutoShop.Application.Services;
using AutoShop.Domain.Interfaces.Repositories;
using AutoShop.Domain.Interfaces.Services;
using AutoShop.Domain.Service.Services;
using AutoShop.Infra.Repositories;
using System;

namespace DDDWebAPI.Infrastruture.CrossCutting.IOC
{
    public class ConfigurationIOC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region Registra IOC

            #region IOC Application
            builder.RegisterType<ApplicationServiceProduto>().As<IApplicationServiceProduto>();
            builder.RegisterType<ApplicationServiceUsuario>().As<IApplicationServiceUsuario>();
            builder.RegisterType<ApplicationServiceVeiculo>().As<IApplicationServiceVeiculo>();
            builder.RegisterType<ApplicationServiceOperacao>().As<IApplicationServiceOperacao>();
            #endregion

            #region IOC Services
            builder.RegisterType<ServiceProduto>().As<IServiceProduto>();
            builder.RegisterType<ServiceUsuario>().As<IServiceUsuario>();
            builder.RegisterType<ServiceVeiculo>().As<IServiceVeiculo>();
            builder.RegisterType<ServiceOperacao>().As<IServiceOperacao>();
            #endregion

            #region IOC Repositorys SQL
            builder.RegisterType<RepositoryProduto>().As<IRepositoryProduto>();
            builder.RegisterType<RepositoryUsuario>().As<IRepositoryUsuario>();
            builder.RegisterType<RepositoryVeiculo>().As<IRepositoryVeiculo>();
            builder.RegisterType<RepositoryOperacao>().As<IRepositoryOperacao>();
            #endregion

            #region Unity Of Work
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>();
            #endregion

            #endregion

        }
    }
}
