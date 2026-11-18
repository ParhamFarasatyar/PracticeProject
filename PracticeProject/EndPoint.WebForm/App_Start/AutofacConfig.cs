using Autofac;
using PracticeProject.Persistence.Contexts;
using PracticeProject.Application;
using PracticeProject.Persistence.DataAccess;
using PracticeProject.Application.BaseInfos;
using PracticeProject.Application.BaseInfos.UserManagement.CreateUser;
using PracticeProject.Application.BaseInfos.UserManagement.LoginUser;
using PracticeProject.Application.BaseInfos.UserManagement.SearchUsers;
using PracticeProject.Application.BaseInfos.UserManagement.DeleteUser;
using PracticeProject.Application.BaseInfos.UserManagement.UpdateUser;
using PracticeProject.Persistance.DataAccess;
using PracticeProject.Application.BaseInfos.Security;
using PracticeProject.Application.BaseInfos.UserManagement.GetUserById;
using PracticeProject.Persistance.DataAccess.BaseInfos;
using PracticeProject.Application.BaseInfos.PersonManagement.SearchPeople;

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

            builder.RegisterType<Pbkdf2PasswordHasher>().As<IPasswordHasher>().InstancePerRequest();

            builder.RegisterType<GetUserById>().As<IGetUserById>().InstancePerRequest();

            builder.RegisterType<SearchPeople>().As<ISearchPeople>().InstancePerRequest();

            return builder.Build();
        }
    }
}