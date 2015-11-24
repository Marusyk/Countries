using System;
using System.Collections.Generic;
using Countries.Models;
using System.Linq.Expressions;

namespace Countries
{
    interface ICountryRepository
    {
        IEnumerable<CountryInfo> Countries { get; }
        void Save(CountryInfo country);
        CountryInfo Delete(Guid giud);
        CountryInfo FindBy(Func<CountryInfo, bool> predicate);
    }
}
