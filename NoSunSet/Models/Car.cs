using System;
using System.Text;

namespace NoSunSet.Models
{
    public class Car
    {
        public String Manufacturer { get; set; }

        public String Model { get; set; }

        public String ModelYear { get; set; }

        public String Variant { get; set; }

        public String DisplayPrice { get; set; }

        public double? Price { get; set; }

        public Car(String Manufacturer, String Model, String ModelYear, String Variant, String DisplayPrice, double? Price)
        {
            this.Manufacturer = Manufacturer;
            this.Model = Model;
            this.ModelYear = ModelYear;
            this.Variant = Variant;
            this.DisplayPrice = DisplayPrice;
            this.Price = Price;
        }

        public string LineInformation
        {
            get
            {
                return (new StringBuilder(Model).Append(" ")
                    .Append(Variant).Append(" (").Append(ModelYear).Append(") - ")
                    .Append(DisplayPrice)).ToString();
            }
        }
    }
}