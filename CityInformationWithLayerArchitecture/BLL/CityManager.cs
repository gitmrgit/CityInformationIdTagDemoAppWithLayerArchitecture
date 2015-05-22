using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityInformationWithLayerArchitecture.DAL;
namespace CityInformationWithLayerArchitecture.BLL
{
    
    class CityManager
    {
        CityGatway cityGatway = new CityGatway();

        public List<City> LoadCityInformation() 
        {
            return cityGatway.LoadCityInformation();
        }

        public int UpdateCity(City city) 
        {
            return cityGatway.UpdateCity(city);
        }

        public string SaveCityInformation(City city) 
        {
            if (city.Name.Length < 4)
            {
                return "Name must be atleast 4 character long.";
            }
            else 
            {
                if (cityGatway.IsCityNameExist(city.Name))
                {
                    return "City name already exists. ";
                }
                else 
                {
                    if (cityGatway.SaveCityInformation(city) > 0)
                    {
                        return "Inserted";
                    }
                    else 
                    {
                        return "Not inserted.";
                    }
                
                }
            }
        }

        public List<City> SearchCityByCityName(string cityName) 
        {
            return cityGatway.SearchCityByCityName(cityName);
        }

        public List<City> SearchCityByCountryName(string countryName)
        {
            return cityGatway.SearchCityByCountryName(countryName);
        }
             
     

    }
}
