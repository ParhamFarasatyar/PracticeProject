using Autofac;
using PracticeProject.Persistence.Contexts;
using PracticeProject.Application;
using PracticeProject.Persistence.DataAccess;
using PracticeProject.Application.BaseInfo;
using PracticeProject.Application.BaseInfo.UserManagement.CreateUser;

namespace EndPoint.WebForm.App_Start
{
    public class AutofacConfig
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DataBaseContext>().As<IDb>().As<IUnitOfWork>().InstancePerRequest();

            builder.RegisterType<UserDataAccess>().As<IUserDataAccess>().InstancePerRequest();

            builder.RegisterType<CreateUser>().As<ICreateUser>().InstancePerRequest();

            return builder.Build();
        }
    }
}