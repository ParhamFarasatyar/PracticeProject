using Autofac;
using PracticeProject.Persistence.Contexts;
using PracticeProject.Application;
using PracticeProject.Persistence.DataAccess;
using PracticeProject.Application.BaseInfo;
using PracticeProject.Application.BaseInfo.UserManagement.CreateUser;
using PracticeProject.Application.BaseInfo.UserManagement.LoginUser;
using PracticeProject.Application.BaseInfo.UserManagement.SearchUsers;
using PracticeProject.Application.BaseInfo.UserManagement.DeleteUser;
using PracticeProject.Application.BaseInfo.UserManagement.UpdateUser;

namespace EndPoint.WebForm.App_Start
{
    public class AutofacConfig
    {
        public static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<DataBaseContext>().As<IDb>().InstancePerRequest();

            builder.RegisterType<UserDataAccess>().As<IUserDataAccess>().InstancePerRequest();

            builder.RegisterType<CreateUser>().As<ICreateUser>().InstancePerRequest();

            builder.RegisterType<LoginUser>().As<ILoginUser>().InstancePerRequest();

            builder.RegisterType<SearchUsers>().As<ISearchUsers>().InstancePerRequest();

            builder.RegisterType<DeleteUser>().As<IDeleteUser>().InstancePerRequest();

            builder.RegisterType<UpdateUser>().As<IUpdateUser>().InstancePerRequest();

            return builder.Build();
        }
    }
}