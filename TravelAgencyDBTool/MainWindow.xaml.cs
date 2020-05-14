using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TravelAgencyDBTool
{
    public enum PlaceLevel
    {
        Metro = 1,
        Urban = 2,
        City = 3,
        Town = 4,
        Village = 5
    }
    public enum RegionLevel
    {
        Continent = 1,
        Geo = 2,
        Sovereign = 3,
        Area = 4,
        District = 5,
        SubRegion = 6
    }
    public class Coordinate
    {
        public float Latitude;
        public float Longitude;

        public Coordinate()
        {
            Latitude = 0.0f;
            Longitude = 0.0f;
        }
        public Coordinate(float Latitude, float Longitude)
        {
            this.Latitude = Latitude;
            this.Longitude = Longitude;
        }
    }
    public class Position
    {
        public float x;
        public float y;

        public Position()
        {
            x = 0.0f;
            y = 0.0f;
        }
        public Position(float x, float y)
        {
            this.x = x;
            this.y = y;
        }
    }
    public class Place
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public Position Pos { get; set; }
        public string Country { get; set; }
        public int ScaleRank { get; set; }
        public int Direction { get; set; }
        public Coordinate Location { get; set; }
        public int Population { get; set; }
        public int PopulationMetro { get; set; }
        public int Elevation { get; set; }
        public string TimeZone { get; set; }
        public string Province { get; set; }
        public List<string> Regions { get; set; }
        public List<string> Languages { get; set; }
        public int GDP { get; set; }
        public List<int> PeakSeasons { get; set; }
        public string Picture { get; set; }

        public Place()
        {
            Location = new Coordinate(0.0f, 0.0f);
            Regions = new List<string>();
            Languages = new List<string>();
            PeakSeasons = new List<int>();
        }
    }
    public class Country
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Capital { get; set; }
        public int Population { get; set; }
        public string Currency { get; set; }
        public int GDP { get; set; }

        public Country()
        {
        }
    }
    public class Province
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public Province()
        {
        }
    }
    public class Region
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }

        public Region()
        {
        }
    }
    public class Currency
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public float RatePerDollar { get; set; }

        public Currency()
        {
        }
    }
    public class Language
    {
        public string Tag { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public int Speakers { get; set; }

        public Language()
        {
        }
    }

    public partial class MainWindow : Window
    {
        private List<Place> placeList = new List<Place>();
        private List<Country> countryList = new List<Country>();
        private List<Province> provinceList = new List<Province>();
        private List<Region> regionList = new List<Region>();
        private List<Currency> currencyList = new List<Currency>();
        private List<Language> languageList = new List<Language>();

        public MainWindow()
        {
            InitializeComponent();

            Place placeData = new Place
            {
                Tag = "LDN",
                Name = "London",
                Level = 1,
                Country = "UK",
                Location = new Coordinate(51.507222f, -0.1275f),
                Population = 8908801,
                PopulationMetro = 14257962,
                Elevation = 0,
                TimeZone = "+09:00",
                Province = "GRLD",
                Regions = new List<string>() { "WEEU" },
                Languages = new List<string>() { "ENG" },
                GDP = 75000,
                PeakSeasons = new List<int>() { 45, 55, 70, 85, 90, 100, 100, 95, 80, 75, 65, 50 },
                Picture = ""
            };

            placeList.Add(placeData);

            Country countryData = new Country
            {
                Tag = "UK",
                Name = "United Kingdom",
                Capital = "LDN",
                Population = 63182178,
                Currency = "GBP",
                GDP = 41030
            };
            countryList.Add(countryData);

            Province provinceData = new Province
            {
                Tag = "GRLD",
                Name = "Greater London",
                Country = "UK"
            };
            provinceList.Add(provinceData);

            Region regionData = new Region
            {
                Tag = "WEEU",
                Name = "Western Europe",
                Level = 1
            };
            regionList.Add(regionData);

            Currency currencyData = new Currency
            {
                Tag = "GBP",
                Name = "Pound sterling",
                Symbol = "£",
                RatePerDollar = 1.22f
            };
            currencyList.Add(currencyData);

            Language languageData = new Language
            {
                Tag = "ENG",
                Name = "English",
                Family = "Indo-European",
                Speakers = 7000000
            };
            languageList.Add(languageData);



            // Place List

            PlaceDataListView.Items.Clear();
            var placeGridView = new GridView();
            PlaceDataListView.View = placeGridView;

            var placeColumns = new List<string> { "Tag", "Name", "Level", "Country", "Latitude", "Longitude", "Population", "PopMetro", "Elevation", "TimeZone", "Province", "Regions", "Languages", "GDP" };
            var placeColumnWidth = new List<int> { 50, 150, 70, 150, 70, 70, 90, 90, 70, 70, 120, 150, 150, 70 };
            for (int index = 0; index < placeColumns.Count; index++)
            {
                placeGridView.Columns.Add(new GridViewColumn
                {
                    Header = placeColumns[index],
                    Width = placeColumnWidth[index],
                    DisplayMemberBinding = new Binding("[" + index.ToString() + "]"),
                });
            }

            foreach ( Place place in placeList )
            {
                List<string> item = PlaceToListItem(place);
                PlaceDataListView.Items.Add(item);
            }


            // Country List

            CountryDataListView.Items.Clear();
            var countryGridView = new GridView();
            CountryDataListView.View = countryGridView;

            var countryColumns = new List<string> { "Tag", "Name", "Capital", "Population", "Currency", "GDP" };
            var countryColumnWidth = new List<int> { 50, 150, 150, 90, 70, 70 };
            for (int index = 0; index < countryColumns.Count; index++)
            {
                countryGridView.Columns.Add(new GridViewColumn
                {
                    Header = countryColumns[index],
                    Width = countryColumnWidth[index],
                    DisplayMemberBinding = new Binding("[" + index.ToString() + "]"),
                });
            }

            foreach (Country country in countryList)
            {
                List<string> item = CountryToListItem(country);
                CountryDataListView.Items.Add(item);
            }


            // Province List

            ProvinceDataListView.Items.Clear();
            var provinceGridView = new GridView();
            ProvinceDataListView.View = provinceGridView;

            var provinceColumns = new List<string> { "Tag", "Name", "Country" };
            var provinceColumnWidth = new List<int> { 50, 150, 150 };
            for (int index = 0; index < provinceColumns.Count; index++)
            {
                provinceGridView.Columns.Add(new GridViewColumn
                {
                    Header = provinceColumns[index],
                    Width = provinceColumnWidth[index],
                    DisplayMemberBinding = new Binding("[" + index.ToString() + "]"),
                });
            }

            foreach (Province province in provinceList)
            {
                List<string> item = ProvinceToListItem(province);
                ProvinceDataListView.Items.Add(item);
            }


            // Region List

            RegionDataListView.Items.Clear();
            var regionGridView = new GridView();
            RegionDataListView.View = regionGridView;

            var regionColumns = new List<string> { "Tag", "Name", "Level" };
            var regionColumnWidth = new List<int> { 50, 150, 120 };
            for (int index = 0; index < regionColumns.Count; index++)
            {
                regionGridView.Columns.Add(new GridViewColumn
                {
                    Header = regionColumns[index],
                    Width = regionColumnWidth[index],
                    DisplayMemberBinding = new Binding("[" + index.ToString() + "]"),
                });
            }

            foreach (Region region in regionList)
            {
                List<string> item = RegionToListItem(region);
                RegionDataListView.Items.Add(item);
            }


            // Currency List

            CurrencyDataListView.Items.Clear();
            var currencyGridView = new GridView();
            CurrencyDataListView.View = currencyGridView;

            var currencyColumns = new List<string> { "Tag", "Name", "Symbol", "RatePerDollar" };
            var currencyColumnWidth = new List<int> { 50, 150, 50, 100 };
            for (int index = 0; index < currencyColumns.Count; index++)
            {
                currencyGridView.Columns.Add(new GridViewColumn
                {
                    Header = currencyColumns[index],
                    Width = currencyColumnWidth[index],
                    DisplayMemberBinding = new Binding("[" + index.ToString() + "]"),
                });
            }

            foreach (Currency currency in currencyList)
            {
                List<string> item = CurrencyToListItem(currency);
                CurrencyDataListView.Items.Add(item);
            }


            // Language List

            LanguageDataListView.Items.Clear();
            var languageGridView = new GridView();
            LanguageDataListView.View = languageGridView;

            var languageColumns = new List<string> { "Tag", "Name", "Family", "Speakers" };
            var languageColumnWidth = new List<int> { 50, 150, 120, 80 };
            for (int index = 0; index < languageColumns.Count; index++)
            {
                languageGridView.Columns.Add(new GridViewColumn
                {
                    Header = languageColumns[index],
                    Width = languageColumnWidth[index],
                    DisplayMemberBinding = new Binding("[" + index.ToString() + "]"),
                });
            }

            foreach (Language language in languageList)
            {
                List<string> item = LanguageToListItem(language);
                LanguageDataListView.Items.Add(item);
            }
        }

        private List<string> PlaceToListItem(Place place)
        {
            List<string> item = new List<string>();

            item.Add(place.Tag);
            item.Add(place.Name);
            item.Add(((PlaceLevel)place.Level).ToString());
            item.Add(GetCountry(place.Country).Name);
            item.Add(place.Location.Latitude.ToString());
            item.Add(place.Location.Longitude.ToString());
            item.Add(string.Format("{0:#,0}", place.Population));
            item.Add(string.Format("{0:#,0}", place.PopulationMetro));
            item.Add(string.Format("{0:#,0}", place.Elevation));
            item.Add(place.TimeZone);
            item.Add(GetProvince(place.Province).Name);
            item.Add(string.Join(", ", place.Regions.ToArray()));
            item.Add(string.Join(", ", place.Languages.ToArray()));
            item.Add(string.Format("{0:#,0}", place.GDP));

            return item;
        }

        private List<string> CountryToListItem(Country country)
        {
            List<string> item = new List<string>();

            item.Add(country.Tag);
            item.Add(country.Name);
            item.Add(GetPlace(country.Capital).Name);
            item.Add(string.Format("{0:#,0}", country.Population));
            Currency currency = GetCurrnecy(country.Currency);
            item.Add(string.Format("{0} ({1})", currency.Tag, currency.Symbol));
            item.Add(string.Format("{0:#,0}", country.GDP));

            return item;
        }

        private List<string> ProvinceToListItem(Province province)
        {
            List<string> item = new List<string>();

            item.Add(province.Tag);
            item.Add(province.Name);
            item.Add(GetCountry(province.Country).Name);

            return item;
        }

        private List<string> RegionToListItem(Region region)
        {
            List<string> item = new List<string>();

            item.Add(region.Tag);
            item.Add(region.Name);
            item.Add(((RegionLevel)region.Level).ToString());

            return item;
        }

        private List<string> CurrencyToListItem(Currency currency)
        {
            List<string> item = new List<string>();

            item.Add(currency.Tag);
            item.Add(currency.Name);
            item.Add(currency.Symbol);
            item.Add(currency.RatePerDollar.ToString());

            return item;
        }

        private List<string> LanguageToListItem(Language language)
        {
            List<string> item = new List<string>();

            item.Add(language.Tag);
            item.Add(language.Name);
            item.Add(language.Family);
            item.Add(string.Format("{0:#,0}", language.Speakers));

            return item;
        }

        private Place GetPlace(string tag)
        {
            foreach (Place place in placeList)
            {
                if (place.Tag.Equals(tag))
                {
                    return place;
                }
            }
            return new Place();
        }

        private Country GetCountry(string tag)
        {
            foreach (Country country in countryList)
            {
                if (country.Tag.Equals(tag))
                {
                    return country;
                }
            }
            return new Country();
        }

        private Province GetProvince(string tag)
        {
            foreach (Province province in provinceList)
            {
                if (province.Tag.Equals(tag))
                {
                    return province;
                }
            }
            return new Province();
        }

        private Region GetRegion(string tag)
        {
            foreach (Region region in regionList)
            {
                if (region.Tag.Equals(tag))
                {
                    return region;
                }
            }
            return new Region();
        }

        private Currency GetCurrnecy(string tag)
        {
            foreach (Currency currency in currencyList)
            {
                if (currency.Tag.Equals(tag))
                {
                    return currency;
                }
            }
            return new Currency();
        }

        private Language GetLanguage(string tag)
        {
            foreach (Language language in languageList)
            {
                if (language.Tag.Equals(tag))
                {
                    return language;
                }
            }
            return new Language();
        }
    }
}
