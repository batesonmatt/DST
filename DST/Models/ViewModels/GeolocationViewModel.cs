using DST.Models.BusinessLogic;
using DST.Models.DomainModels;
using System.Collections.Generic;

namespace DST.Models.ViewModels
{
    public class GeolocationViewModel
    {
        #region Properties

        public GeolocationModel Geolocation { get; set; }
        public IEnumerable<TimeZoneItem> TimeZoneItems { get; set; }
        public IDictionary<string, string> Route { get; set; }
        public string ActionMethod { get; set; }

        #endregion
    }
}
