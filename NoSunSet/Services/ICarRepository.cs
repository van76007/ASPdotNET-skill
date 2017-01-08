using NoSunSet.CarRegistrationService;
using NoSunSet.Models;
using System.Collections.Generic;

namespace NoSunSet.Services
{
    public interface ICarRepository
    {
        IEnumerable<Car> GetCars(VehicleInformation information);
    }
}
