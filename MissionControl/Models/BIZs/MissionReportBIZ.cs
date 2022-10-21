using MissionControl.Models.DataAccessLayer;
using MissionControl.Models.DB;
using MissionControl.Models.DTOs;
using MissionControl.Statics;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MissionControl.Models.BIZs
{
    public class MissionReportBIZ
    {
        public long Id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public double lat { get; set; }
        public double lng { get; set; }
        public DateTime mission_date { get; set; }
        public DateTime finalization_date { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime deleted_at { get; set; }
        public long user_id { get; set; }



        public UserDTO user { get; set; }
        public IEnumerable<MissionImageDTO> images { get; set; }

        private bool UserExists(long id)
        {
            /*
             * unsure if we need a try/catch here
             * but I think its a good idea to let individual classes handle exceptions on their own
             * have tried returning user, but that just pushed the problem further
             * this way we gather the problem in one "weird" piece of code
             * */
            try
            {
                DAL dal = new DAL();
                UserDTO user = dal.GetUser(id);
                
                //???, hmm
                if(user.IsNull())
                    throw new Exception("unknown error");
                
                return true;
            }
            catch (Exception _e)
            {
                //this way we handle any type of exception
                throw;

                //throw new Exception("user dosnt exist");
                //return false;
            }
        }

        public List<MissionReportDTO> GetMissinReports()
        {
            using (DBContext _db = new DBContext())
            {
                IQueryable<MissionReport> _q = _db.reports.OrderBy(x=>x.Id);

                IEnumerable<MissionReport> _m = _q.AsEnumerable().ToList();

                if (_m.IsNull())
                    throw new Exception();

                return this.ListToDTO(_m.ToList());
            }
        }

        public MissionReportDTO GetMissionReport(long id)
        {
            using (DBContext _db = new DBContext())
            {
                IQueryable<MissionReport> _q = _db.reports
                    .Include(x => x.mission_images)
                    .Where(x => x.Id == id);

                MissionReport _m = _q.AsEnumerable().FirstOrDefault();

                if (_m.IsNull())
                    throw new Exception("report not found");

                return this.ToDTO(_m);
            }
        }


        public void CreateMissioReport(ViewModelMissionreportPost _m)
        {
            
            if (_m.IsNull())
                throw new Exception("some error");

            if(UserExists(_m.user_id))
            {
                using (DBContext _db = new DBContext())
                {
                    MissionReport report = new MissionReport();
                    report.Name = _m.name;
                    report.Description = _m.description;
                    report.Lat = _m.lat;
                    report.Lng = _m.lng;
                    report.MissionDate = GeneralHelper.UnixTimeStampToDateTime(_m.mission_date);
                    report.FinalisationDate = GeneralHelper.UnixTimeStampToDateTime(_m.finalization_date);
                    report.CreatedAt = GeneralHelper.UnixTimeStampToDateTime(_m.created_at);
                    report.UpdatedAt = GeneralHelper.UnixTimeStampToDateTime(_m.updated_at);
                    report.DeletedAt = GeneralHelper.UnixTimeStampToDateTime(_m.deleted_at);
                    report.UserId = _m.user_id;

                    _db.reports.Add(report);

                    _db.SaveChanges();
                }
                //return;
            }

            //throw new Exception("unknown error");
            
            //else
            //{
            //    throw new Exception("user dosnt exist");
            //}
        }

        public void UpdateMissionreport(ViewModelMissionreportPut _m)
        {
            if (_m.IsNull())
                throw new Exception("some error");

            if (UserExists(_m.user_id))
            {
                using (DBContext _db = new DBContext())
                {
                    MissionReport report = _db.reports.Where(x => x.Id == _m.id).FirstOrDefault();

                    if (report.IsNull())
                        throw new Exception("report does not exist");

                    report.Name = _m.name;
                    report.Description = _m.description;
                    report.Lat = _m.lat;
                    report.Lng = _m.lng;
                    report.MissionDate = GeneralHelper.UnixTimeStampToDateTime(_m.mission_date);
                    report.FinalisationDate = GeneralHelper.UnixTimeStampToDateTime(_m.finalization_date);
                    report.CreatedAt = GeneralHelper.UnixTimeStampToDateTime(_m.created_at);
                    report.UpdatedAt = GeneralHelper.UnixTimeStampToDateTime(_m.updated_at);
                    report.DeletedAt = GeneralHelper.UnixTimeStampToDateTime(_m.deleted_at);
                    report.UserId = _m.user_id;

                    _db.SaveChanges();
                }
                //return;
            }

            //throw new Exception("unknown error");

            //else
            //{
            //    throw new Exception("user dosnt exist");
            //}
        }

        public void DeleteMissionreport(long id)
        {
            using (DBContext _db = new DBContext())
            {
                MissionReport report = _db.reports.Where(x => x.Id == id).FirstOrDefault();

                if (report.IsNull())
                    throw new Exception("report does not exist");

                _db.reports.Remove(report);

                _db.SaveChanges();
            }
        }

        public List<MissionReportDTO> ListToDTO(List<MissionReport> _m)
        {
            if (_m.IsNull())
                throw new Exception();

            List<MissionReportDTO> list = new List<MissionReportDTO>();
            foreach (MissionReport m in _m)
                list.Add(this.ToDTO(m));

            return list;
        }

        public MissionReportDTO ToDTO(MissionReport _m)
        {
            if (_m.IsNull())
                throw new Exception();

            MissionReportDTO dto = new MissionReportDTO();
            
            dto.Id = _m.Id;
            dto.name = !_m.Name.IsNull() ? _m.Name : "";
            dto.description = !_m.Description.IsNull() ? _m.Description : "";
            dto.lat = !_m.Lat.IsNull() ? _m.Lat : 0.0d;
            dto.lng = !_m.Lng.IsNull() ? _m.Lng : 0.0d;
            dto.mission_date = !_m.MissionDate.IsNull() ? _m.MissionDate : new DateTime();
            dto.finalization_date = !_m.FinalisationDate.IsNull() ? _m.FinalisationDate : new DateTime();
            dto.created_at = !_m.CreatedAt.IsNull() ? _m.CreatedAt : new DateTime();
            dto.updated_at = !_m.UpdatedAt.IsNull() ? _m.UpdatedAt : new DateTime();
            dto.deleted_at = !_m.DeletedAt.IsNull() ? _m.DeletedAt : new DateTime();
            dto.user_id = _m.UserId;
        
            return dto;
        }
    }
}
/**/