using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CityInformationWithLayerArchitecture.BLL;
namespace CityInformationWithLayerArchitecture
{
   
    public partial class CityInformationUI : Form
    {
        CityManager cityManager = new CityManager();
        bool isUpdateMode = false;
        int selectedCityID = 0;
        public CityInformationUI()
        {
            InitializeComponent();
        }

        private void CityInformationUI_Load(object sender, EventArgs e)
        {
            LoadCityInformationInListView();  
        }

        private void LoadCityInformationInListView() 
        {
            allCityListView.Items.Clear();
            List<City> cityList = cityManager.LoadCityInformation();
            int count = 1;
            foreach(City city in cityList)
            {
                ListViewItem listViewItem = new ListViewItem(count.ToString());
                listViewItem.SubItems.Add(city.Name.ToString());
                listViewItem.SubItems.Add(city.About.ToString());
                listViewItem.SubItems.Add(city.Country.ToString());

                listViewItem.Tag = city;
                allCityListView.Items.Add(listViewItem);
                count++;
            }

        }
        private void allCityListView_DoubleClick(object sender, EventArgs e)
        {
            City city = new City();
            ListViewItem listViewItem = allCityListView.SelectedItems[0];
            city = (City)listViewItem.Tag;
            cityNameTextBox.Text = city.Name;
            aboutTextBox.Text = city.About;
            countryNameTextBox.Text = city.Country;


            cityNameTextBox.Enabled = false;
            saveButton.Text = "Update";
            selectedCityID = city.cityID;
            isUpdateMode = true;
    
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (isUpdateMode)
            {
                if (aboutTextBox.Text == "" || countryNameTextBox.Text == "")
                {
                    MessageBox.Show("Please enter the value .");
                }
                else
                {
                    City city = new City();
                    city.cityID = selectedCityID;
                    city.About = aboutTextBox.Text;
                    city.Country = countryNameTextBox.Text;

                    if (cityManager.UpdateCity(city) > 0)
                    {
                        MessageBox.Show("Updated");

                        isUpdateMode = false;
                        saveButton.Text = "Save";
                        cityNameTextBox.Enabled = true;
                        selectedCityID = 0;
                        ClearAllField();
                        LoadCityInformationInListView();
                    }
                    else
                    {
                        MessageBox.Show("Not updated.");
                    }
                }
            }
            else 
            {
                if (cityNameTextBox.Text == "" || aboutTextBox.Text == "" || countryNameTextBox.Text == "")
                {
                    MessageBox.Show("Please enter the information. ");
                }
                else 
                {
                    City city = new City();
                    city.Name = cityNameTextBox.Text;
                    city.About = aboutTextBox.Text;
                    city.Country = countryNameTextBox.Text;

                    MessageBox.Show(cityManager.SaveCityInformation(city));

                    LoadCityInformationInListView();

                  //  ClearAllField();
                }
            }
        }

        private void ClearAllField() 
        {
            cityNameTextBox.Text = aboutTextBox.Text = countryNameTextBox.Text = "";
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            if (citySearchRadioButton.Checked) 
            {
                if (searchTextBox.Text == "")
                {
                    MessageBox.Show("Please enter the city name to search.");
                }
                else 
                {
                    allCityListView.Items.Clear();
                   List<City> cityList = cityManager.SearchCityByCityName(searchTextBox.Text);

                   int count = 1;
                   foreach (City city in cityList)
                   {
                       ListViewItem listViewItem = new ListViewItem(count.ToString());
                       listViewItem.SubItems.Add(city.Name.ToString());
                       listViewItem.SubItems.Add(city.About.ToString());
                       listViewItem.SubItems.Add(city.Country.ToString());

                       listViewItem.Tag = city;
                       allCityListView.Items.Add(listViewItem);
                       count++;
                   }


                }
            }
            else if (countrySearchRadioButton.Checked)
            {
                if (searchTextBox.Text == "")
                {
                    MessageBox.Show("Please enter the country name to search.");
                }
                else
                {
                    allCityListView.Items.Clear();
                    List<City> cityList = cityManager.SearchCityByCountryName(searchTextBox.Text);

                    int count = 1;
                    foreach (City city in cityList)
                    {
                        ListViewItem listViewItem = new ListViewItem(count.ToString());
                        listViewItem.SubItems.Add(city.Name.ToString());
                        listViewItem.SubItems.Add(city.About.ToString());
                        listViewItem.SubItems.Add(city.Country.ToString());

                        listViewItem.Tag = city;
                        allCityListView.Items.Add(listViewItem);
                        count++;
                    }
                }
            }
            else 
            {
                MessageBox.Show("Please select country/city to search.");
            }
        }

        private void countrySearchRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            LoadCityInformationInListView();
        }

    }
}
