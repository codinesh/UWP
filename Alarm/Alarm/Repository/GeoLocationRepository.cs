using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Alarm.Repository
{
    class GeoLocationRepository
    {
        public async Task<Geopoint> GetCurrentPosition()
        {
            return (await new Geolocator().GetGeopositionAsync()).Coordinate.Point;
        }
    }
}
