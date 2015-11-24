using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Xml.Serialization;

namespace Countries.Models
{
    [XmlRoot("Countries")]
    public class CountriesCollection
    {
        [XmlElement("Country")]
        public List<CountryInfo> Country { get; set; }
    }

    public class CountryInfo
    {
        [XmlElement("Guid")]
        [HiddenInput(DisplayValue = false)]
        public Guid Guid { get; set; }

        [XmlElement("Name")]
        [Required(ErrorMessage = "Please enter a name")]
        public string Name { get; set; }

        [XmlElement("Capital")]
        [Required(ErrorMessage = "Please enter a capital")]
        public string Capital { get; set; }

        [XmlElement("IsoCode")]
        [MaxLength(3)]
        public string IsoCode { get; set; }

        [XmlElement("Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }
    }
}