
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Messenger.Repository;
using Messenger.Repository.Interface;

namespace Messenger
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> add config
            builder.Services.AddAutoMapper(typeof(MapperProfiles));
            builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
            builder.Host.ConfigureContainer<ContainerBuilder>(contaierBuilder => {
                contaierBuilder.RegisterType<UserManeger>().As<IUserManeger>();
            });
            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
