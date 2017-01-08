using System;

namespace NoSunSet.Models
{
    public class SearchCriteria
    {
        public String Maerke { get; set; }
        public String Model { get; set; }
        public DateTime? FoersteRegistreringDato { get; set; }
        public Decimal? MotorEffektHK { get; set; }

        public SearchCriteria(string Maerke, string Model, DateTime? FoersteRegistreringDato, decimal? MotorEffektHK)
        {
            this.Maerke = Maerke;
            this.Model = Model;
            this.FoersteRegistreringDato = FoersteRegistreringDato;
            this.MotorEffektHK = MotorEffektHK;
        }
    }
}