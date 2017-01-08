using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using NoSunSet.Services;
using NoSunSet.CarRegistrationService;
using System.Configuration;

namespace NoSunSet
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialise()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();
      
      // Dependency Injection
      container.RegisterType<ICarRepository, CarRepository>(
          new InjectionConstructor(ConfigurationManager.ConnectionStrings["AWSConnection"].ConnectionString));
      container.RegisterType<IDmrService, DmrServiceClient>(new InjectionConstructor());
      container.RegisterType<ISearchService, SearchService>();

      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
    
    }
  }
}