using NoSunSet.CarRegistrationService;
using NoSunSet.Exceptions;
using NoSunSet.Models;
using NoSunSet.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NoSunSet.Services
{
    public class CarRepository : ICarRepository
    {
        private CarBuilder carBuilder;
        private readonly string _connectionString;

        public CarRepository(String connectionString)
        {
            _connectionString = connectionString;
            carBuilder = new CarBuilder();
        }

        public IEnumerable<Car> GetCars(VehicleInformation information)
        {
            List<Car> cars = new List<Car>();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = buildSQLCommand(information);
                command.Connection = connection;

                try
                {
                    connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            cars.Add(BuildCar(reader));
                        }
                    }
                }
                catch
                {
                    throw new CustomException("Database access exception");
                }
            }

            return cars;
        }

        private SqlCommand buildSQLCommand(VehicleInformation information)
        {
            SqlCommand command = new SqlCommand();

            StringBuilder queryStringBuilder = new StringBuilder("SELECT TOP(@topParam) Manufacturerttxt, Modeltxt, Variant, ModelYear, Price FROM dbo.Cars ");
            queryStringBuilder.Append("WHERE UPPER(Manufacturerttxt) = UPPER(@manufacturer) ");
            queryStringBuilder.Append("AND UPPER(Modeltxt) = UPPER(@model) ");

            command.Parameters.Add("@topParam", SqlDbType.Int);
            command.Parameters["@topParam"].Value = 10;
            command.Parameters.Add("@manufacturer", SqlDbType.VarChar);
            command.Parameters["@manufacturer"].Value = information.Maerke;
            command.Parameters.Add("@model", SqlDbType.VarChar);
            command.Parameters["@model"].Value = information.Model;

            try
            {
                int year = Convert.ToInt32(information.KoeretoejIdent.Substring(7, 4));
                queryStringBuilder.Append("AND ModelYear = @modelYear ");
                command.Parameters.Add("@modelYear", SqlDbType.Int);
                command.Parameters["@modelYear"].Value = year;
            }
            catch
            {
                // In case KoeretoejIdent is malformatted, the year value is not created 
                // and hence the query does not need to add @modelYear parameter
            }

            if (information.MotorEffektHK.HasValue)
            {
                queryStringBuilder.Append("ORDER BY ABS(HorsePower - @horsePower) ");
                command.Parameters.Add("@horsePower", SqlDbType.Int);
                command.Parameters["@horsePower"].Value = Convert.ToInt32(information.MotorEffektHK);
            }
            
            command.CommandText = queryStringBuilder.ToString();

            return command;
        }

        private Car BuildCar(IDataReader reader)
        {
            var dr = new NullableDataReader(reader);
            return carBuilder.CreateCarWithManufacturer(dr.GetNullableString("Manufacturerttxt"))
                .WithModel(dr.GetNullableString("Modeltxt"))
                .WithVariant(dr.GetNullableString("Variant"))
                .WithModelYear(dr.GetNullableInt32("ModelYear"))
                .WithPrice(dr.GetNullableDouble("Price"));
        }
    }
}