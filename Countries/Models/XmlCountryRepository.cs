using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Countries.Models
{
    class XmlCountryRepository : ICountryRepository
    {
        private readonly CountriesCollection _countries;
        private readonly string _xmlFilePath;

        public XmlCountryRepository()
        {
            _xmlFilePath = GetXmlFilePath();
            _countries = LoadDataFromXml();
        }

        #region private methods
        private CountriesCollection LoadDataFromXml()
        {
            CountriesCollection countries = null;

            using (var reader = new StreamReader(_xmlFilePath))
            {
                var serializer = new XmlSerializer(typeof(CountriesCollection));
                countries = (CountriesCollection)serializer.Deserialize(reader);
            }

            return countries;
        }

        private void SaveToXml()
        {

            using (var writer = new StreamWriter(_xmlFilePath))
            {
                var serializer = new XmlSerializer(typeof(CountriesCollection));
                serializer.Serialize(writer, _countries);
            }

        }

        private static string GetXmlFilePath()
        {
            var pathToAppData = AppDomain.CurrentDomain.GetData("DataDirectory").ToString();
            var pathToXml = Path.Combine(pathToAppData, "countries.xml");

            var settingValue = ConfigurationManager.AppSettings["PathToXmlFile"];

            if (settingValue != null && !settingValue.Equals("default"))
            {
                pathToXml = settingValue;
            }

            return pathToXml;
        }

        private void Add(CountryInfo country)
        {
            country.Guid = Guid.NewGuid();
            _countries.Country.Add(country);
        }

        private void Edit(CountryInfo country, int index)
        {
            _countries.Country[index] = country;
        }
        #endregion

        public IEnumerable<CountryInfo> Countries
        {
            get
            {
                return _countries.Country.AsEnumerable();
            }
        }

        public CountryInfo Delete(Guid giud)
        {
            var country = _countries.Country.FirstOrDefault(x => x.Guid == giud);
            if (country != null)
            {
                _countries.Country.Remove(country);
            }
            SaveToXml();
            return country;
        }

        public void Save(CountryInfo country)
        {
            var index = _countries.Country.FindIndex(x => x.Guid == country.Guid);
            if (index > -1)
            {
                Edit(country, index);
            }
            else
            {
                Add(country);
            }

            SaveToXml();
        }

        public CountryInfo FindBy(Func<CountryInfo, bool> predicate)
        {
            return Countries.FirstOrDefault(predicate);
        }
    }
}
