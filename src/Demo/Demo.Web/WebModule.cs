using Autofac;
using Demo.Application;
using Demo.Application.Features.Books.Commands;
using Demo.Application.Services;
using Demo.Domain.Repository;
using Demo.Domain.Services;
using Demo.Domain.Utilities;
using Demo.Infrastructure;
using Demo.Infrastructure.Repositories;
using Demo.Infrastructure.Utilities;
using Demo.Models.Demo;

namespace Demo
{
    public class WebModule: Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;
        public WebModule(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Item>().As<IItem>().InstancePerLifetimeScope();
            builder.RegisterType<ApplicationDbContext>().AsSelf()
               .WithParameter("connectionString", _connectionString)
               .WithParameter("migrationAssembly", _migrationAssembly)
               .InstancePerLifetimeScope();
            builder.RegisterType<ApplicationUnitOfWork>().As<IApplicationUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<BookRepository>().As<IBookRepository>().InstancePerLifetimeScope();
            builder.RegisterType<BookService>().As<IBookService>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorRepository>().As<IAuthorRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AuthorService>().As<IAuthorService>().InstancePerLifetimeScope();
            builder.RegisterType<EmailUtility>().As<IEmailUtility>().InstancePerLifetimeScope();
            builder.RegisterType<BookAddCommandHandler>().AsSelf();
            base.Load(builder);
        }
    }
}
