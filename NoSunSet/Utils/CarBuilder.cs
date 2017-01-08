using NoSunSet.Models;
using System;
using System.Text;

namespace NoSunSet.Utils
{
    public class CarBuilder
    {
        const String DASH = "_";

        const String CURRENCY = " DKK";

        private String Manufacturer;

        private String Model;

        private String Variant;

        private String ModelYear;

        private String DisplayPrice;

        private double? Price;

        public CarBuilder CreateCarWithManufacturer(String Manufacturer)
        {
            this.Manufacturer = Manufacturer;
            return this;
        }

        public CarBuilder WithModel(String Model)
        {
            this.Model = Model;
            return this;
        }

        public CarBuilder WithVariant(String Variant)
        {
            this.Variant = Variant;
            return this;
        }

        public CarBuilder WithModelYear(int? ModelYear)
        {
            this.ModelYear = ModelYear.HasValue ? ModelYear.Value.ToString() : DASH;
            return this;
        }

        public CarBuilder WithPrice(double? Price)
        {
            this.Price = Price;
            this.DisplayPrice = Price.HasValue ?  new StringBuilder(Price.Value.ToString()).Append(CURRENCY).ToString() : DASH;
            return this;
        }

        public static implicit operator Car(CarBuilder builder)
        {
            return new Car(
                builder.Manufacturer,
                builder.Model,
                builder.ModelYear,
                builder.Variant,
                builder.DisplayPrice,
                builder.Price);
        }
    }
}