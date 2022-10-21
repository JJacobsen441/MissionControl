using MissionControl.Models.DB;
using MissionControl.Models.DTOs;
using MissionControl.Statics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace MissionControl.Models.BIZs
{
    public class UserBIZ
    {
        public long Id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string code_name { get; set; }
        public string user_name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string avatar { get; set; }


        public IEnumerable<MissionReport> missionreports { get; set; }

        public List<UserDTO> GetUsers()
        {
            using (DBContext _db = new DBContext())
            {
                IQueryable<User> _q = _db.users.OrderBy(x=>x.Id);
                
                IEnumerable<User> _u = _q.AsEnumerable().ToList();

                if (_u.IsNull())
                    throw new Exception();

                return this.ListToDTO(_u.ToList());
            }
        }

        public UserDTO GetUser(long id)
        {
            using (DBContext _db = new DBContext())
            {
                IQueryable<User> _q = _db.users
                    .Include(x => x.missionreports)
                    .Where(x => x.Id == id);

                User _u = _q.AsEnumerable().FirstOrDefault();

                if (_u.IsNull())
                    throw new Exception("user not found");
                
                return this.ToDTO(_u);
            }
        }

        public void CreateUser(ViewModelUserPost _u)
        {
            if (_u.IsNull())
                throw new Exception("some error");

            using (DBContext _db = new DBContext())
            {
                User user = new User();
                user.FirstName = _u.first_name;
                user.LastName = _u.last_name;
                user.CodeName = _u.code_name;
                user.UserName = _u.user_name;
                user.Email = _u.email;
                user.Password = _u.password;
                user.Avatar = _u.avatar;

                _db.users.Add(user);

                _db.SaveChanges();
            }
        }

        public void UpdateUser(ViewModelUserPut _u)
        {
            if (_u.IsNull())
                throw new Exception("some error");

            using (DBContext _db = new DBContext())
            {
                User user = _db.users.Where(x => x.Id == _u.id).FirstOrDefault();

                if (user.IsNull())
                    throw new Exception("user does not exist");

                user.FirstName = _u.first_name;
                user.LastName = _u.last_name;
                user.CodeName = _u.code_name;
                user.UserName = _u.user_name;
                user.Email = _u.email;
                user.Password = _u.password;
                user.Avatar = _u.avatar;

                _db.SaveChanges();
            }
        }

        public void DeleteUser(long id)
        {
            using (DBContext _db = new DBContext())
            {
                User user = _db.users.Where(x => x.Id == id).FirstOrDefault();

                if (user.IsNull())
                    throw new Exception("user does not exist");

                //foreach (MissionReport _m in user.missionreports)
                //    _db.reports.Remove(_m);

                _db.users.Remove(user);

                _db.SaveChanges();
            }
        }

        public List<UserDTO> ListToDTO(List<User> _u)
        {
            if (_u.IsNull())
                throw new Exception();

            List<UserDTO> list = new List<UserDTO>();
            foreach (User u in _u)
                list.Add(this.ToDTO(u));

            return list;
        }

        public UserDTO ToDTO(User _u)
        {
            if (_u.IsNull())
                throw new Exception();

            UserDTO dto = new UserDTO();
            MissionReportBIZ biz = new MissionReportBIZ();

            dto.Id = _u.Id;
            dto.first_name = !_u.FirstName.IsNull() ? _u.FirstName : "";
            dto.last_name = !_u.LastName.IsNull() ? _u.LastName : "";
            dto.code_name = !_u.CodeName.IsNull() ? _u.CodeName : "";
            dto.user_name = !_u.UserName.IsNull() ? _u.UserName : "";
            dto.email = !_u.Email.IsNull() ? _u.Email : "";
            dto.password = !_u.Password.IsNull() ? _u.Password : "";
            dto.avatar = !_u.Avatar.IsNull() ? _u.Avatar : "";
            dto.missionreports = !_u.missionreports.IsNull() ? biz.ListToDTO(_u.missionreports.ToList()) : new List<MissionReportDTO>();
            

            return dto;
        }
    }
}
/**/