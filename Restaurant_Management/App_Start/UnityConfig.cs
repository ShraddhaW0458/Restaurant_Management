using Restaurant_Management.IServices;
using Restaurant_Management.Services;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Restaurant_Management
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();

            container.RegisterType<ILogin, LoginService>();
            container.RegisterType<IBranch, BranchService>();
            container.RegisterType<IUserRole, UserRoleService>();
            container.RegisterType<ICountry, CountryService>();
            container.RegisterType<IState, StateService>();
            container.RegisterType<ICity, CityService>();
            container.RegisterType<ICustomer, CustomerService>();
            container.RegisterType<IMenu, MenuService>();
            container.RegisterType<ITables, TablesService>();
            container.RegisterType<IMaster, MasterService>();
            container.RegisterType<IOrderItems, OrderItemsService>();
            container.RegisterType<IOrder, OrderService>();
            container.RegisterType<IReservation, ReservationService>();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}