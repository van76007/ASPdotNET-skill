using NoSunSet.Models;
using System.Collections.Generic;

namespace NoSunSet.Services
{
    public interface ISearchService
    {
        IEnumerable<Car> GetCarDisplayInformation(string registrationNumber);
    }
}
