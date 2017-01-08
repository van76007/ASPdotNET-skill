using NoSunSet.CarRegistrationService;
using NoSunSet.Exceptions;
using NoSunSet.Models;
using System.Collections.Generic;

namespace NoSunSet.Services
{
    public class SearchService : ISearchService
    {
        ICarRepository carRepository;
        IDmrService carRegistrationService;

        public SearchService(ICarRepository carRepository, IDmrService carRegistrationService)
        {
            this.carRepository = carRepository;
            this.carRegistrationService = carRegistrationService;
        }

        public IEnumerable<Car> GetCarDisplayInformation(string registrationNumber)
        {
            List<Car> displayCars = new List<Car>();

            try
            {
                var response = carRegistrationService.GetVehicleInformationByReg(registrationNumber);
                IEnumerable<Car> cars = carRepository.GetCars(response);

                foreach (var car in cars)
                {
                    displayCars.Add(car);
                }
            }
            catch
            {
                throw new CustomException("WebService Exception");
            }
            
            return displayCars;
        }
    }
}