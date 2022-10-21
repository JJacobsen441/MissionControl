using MissionControl.Models.DB;
using MissionControl.Models.DTOs;
using MissionControl.Statics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MissionControl.Models.BIZs
{
    public class FacilityBIZ
    {
        public long Id { get; set; }
        public string Location { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long Timestamp { get; set; }
        public int Distance { get; set; }

        public void CreateFacility(FacilityDTO _f)
        {
            if (_f.IsNull())
                throw new Exception("some error");

            using (DBContext _db = new DBContext())
            {
                Facilitys __f = new Facilitys();
                __f.Location = _f.Location;
                __f.Latitude = _f.Latitude;
                __f.Longitude = _f.Longitude;
                __f.ISSLatitude = _f.ISSLatitude;
                __f.ISSLongitude = _f.ISSLongitude;
                __f.Timestamp = _f.Timestamp;
                __f.Distance = _f.Distance;

                _db.facilitys.Add(__f);

                _db.SaveChanges();
            }
        }

        public FacilityDTO GetClosest5min()
        {
            using (DBContext _db = new DBContext())
            {
                IQueryable<Facilitys> _q = _db.facilitys
                    .Where(x => x.Distance != 0)
                    .OrderByDescending(x => x.Timestamp)
                    .Take(5);

                Facilitys _f = _q.AsEnumerable().FirstOrDefault();

                if (_f.IsNull())
                    throw new Exception();

                return this.ToDTO(_f);
            }
        }

        public List<FacilityDTO> ListToDTO(List<Facilitys> _f)
        {
            if (_f.IsNull())
                throw new Exception();

            List<FacilityDTO> list = new List<FacilityDTO>();
            foreach (Facilitys f in _f)
                list.Add(this.ToDTO(f));

            return list;
        }

        public FacilityDTO ToDTO(Facilitys _f)
        {
            if (_f.IsNull())
                throw new Exception();

            FacilityDTO dto = new FacilityDTO();
            
            dto.Id = _f.Id;
            dto.Location = !_f.Location.IsNull() ? _f.Location : "";
            dto.Latitude = !_f.Latitude.IsNull() ? _f.Latitude : 0.0d;
            dto.Longitude = !_f.Longitude.IsNull() ? _f.Longitude : 0.0d;
            dto.ISSLatitude = !_f.ISSLatitude.IsNull() ? _f.ISSLatitude : 0.0d;
            dto.ISSLongitude = !_f.ISSLongitude.IsNull() ? _f.ISSLongitude : 0.0d;
            dto.Distance = !_f.Distance.IsNull() ? _f.Distance : -1;
            dto.Timestamp = !_f.Timestamp.IsNull() ? _f.Timestamp : -1;
            
            return dto;
        }
    }
}