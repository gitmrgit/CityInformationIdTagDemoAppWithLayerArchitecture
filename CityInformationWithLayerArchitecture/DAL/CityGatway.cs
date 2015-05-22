using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
namespace CityInformationWithLayerArchitecture.DAL
{
   class CityGatway
    {
        string conStr = ConfigurationManager.ConnectionStrings[1].ConnectionString;
        public List<City> LoadCityInformation() 
        {
            List<City> cityList = new List<City>();
            using (SqlConnection con = new SqlConnection(conStr)) 
            {
                SqlCommand cmd = new SqlCommand("Select * from tblCity",con);
                con.Open();
                SqlDataReader rdr =  cmd.ExecuteReader();
                while (rdr.Read()) 
                {
                    City city = new City();
                    city.cityID = int.Parse(rdr[0].ToString());
                    city.Name = rdr[1].ToString();
                    city.About = rdr[2].ToString();
                    city.Country = rdr[3].ToString();

                    cityList.Add(city);
                }
            }

            return cityList;


        }

        public int UpdateCity(City city) 
        {
            int rowsUpdated;
            using (SqlConnection con = new SqlConnection(conStr)) 
            {
                SqlCommand cmd = new SqlCommand("update tblCity set About='"+city.About+"' , Country = '"+city.Country+"' where ID='"+city.cityID+"'",con);
                con.Open();
                rowsUpdated = cmd.ExecuteNonQuery();
            }
            return rowsUpdated;
        }

        public bool IsCityNameExist(string name) 
        {
            bool isNameExist = false;
            using (SqlConnection con = new SqlConnection(conStr)) 
            {
                SqlCommand cmd = new SqlCommand("Select * from tblCity where Name='"+name+"'",con);
                con.Open();
                SqlDataReader rdr =  cmd.ExecuteReader();
                while (rdr.Read()) 
                {
                    isNameExist = true;
                }
            }
            return isNameExist;
        }


        public int SaveCityInformation(City city) 
        {
            int rowsInserted;
            using (SqlConnection con = new SqlConnection(conStr)) 
            {
                SqlCommand cmd = new SqlCommand("Insert into tblCity values('"+city.Name+"','"+city.About+"','"+city.Country+"')",con);
                con.Open();
                rowsInserted = cmd.ExecuteNonQuery();
            }
            return rowsInserted;
        
        }


        public List<City> SearchCityByCityName(string cityName) 
        {
            List<City> cityList = new List<City>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblCity where Name like '%" + cityName + "%'", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    City city = new City();
                    city.cityID = int.Parse(rdr[0].ToString());
                    city.Name = rdr[1].ToString();
                    city.About = rdr[2].ToString();
                    city.Country = rdr[3].ToString();

                    cityList.Add(city);
                }
            }
            return cityList;
        }

        public List<City> SearchCityByCountryName(string countryName)
        {
            List<City> cityList = new List<City>();
            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand("Select * from tblCity where Country like '%" + countryName + "%'", con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    City city = new City();
                    city.cityID = int.Parse(rdr[0].ToString());
                    city.Name = rdr[1].ToString();
                    city.About = rdr[2].ToString();
                    city.Country = rdr[3].ToString();

                    cityList.Add(city);
                }
            }
            return cityList;
        }

        //public List<City> SearchCityByCountryName(string countryName) 
        //{
          
        //}

    }
}
