using MissionControl.Models.BIZs;
using MissionControl.Models.DTOs;
using System.Collections.Generic;

namespace MissionControl.Models.DataAccessLayer
{
    public class DAL
    {
        public void CreateClosestFacilityByTimeStamp(FacilityDTO _f)
        {
            FacilityBIZ biz = new FacilityBIZ();

            biz.CreateFacility(_f);
        }

        public FacilityDTO GetClosest5min()
        {
            FacilityBIZ biz = new FacilityBIZ();

            return biz.GetClosest5min();
        }

        public List<UserDTO> GetUsers()
        {
            UserBIZ biz = new UserBIZ();

            return biz.GetUsers();
        }

        public UserDTO GetUser(long id)
        {
            UserBIZ biz = new UserBIZ();

            return biz.GetUser(id);
        }

        public void CreateUser(ViewModelUserPost _u)
        {
            UserBIZ biz = new UserBIZ();

            biz.CreateUser(_u);
        }

        public void UpdateUser(ViewModelUserPut _u)
        {
            UserBIZ biz = new UserBIZ();

            biz.UpdateUser(_u);
        }

        public void DeleteUser(long id)
        {
            UserBIZ biz = new UserBIZ();

            biz.DeleteUser(id);
        }





        public List<MissionReportDTO> GetMissionReports()
        {
            MissionReportBIZ biz = new MissionReportBIZ();

            return biz.GetMissinReports();
        }

        public MissionReportDTO GetMissionReport(long id)
        {
            MissionReportBIZ biz = new MissionReportBIZ();

            return biz.GetMissionReport(id);
        }

        public void CreateMissionReport(ViewModelMissionreportPost _m)
        {
            MissionReportBIZ biz = new MissionReportBIZ();

            biz.CreateMissioReport(_m);
        }

        public void UpdateMissionReport(ViewModelMissionreportPut _m)
        {
            MissionReportBIZ biz = new MissionReportBIZ();

            biz.UpdateMissionreport(_m);
        }

        public void DeleteMissionReport(long id)
        {
            MissionReportBIZ biz = new MissionReportBIZ();

            biz.DeleteMissionreport(id);
        }
    }
}
