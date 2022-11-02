using MissionControl.Models.DB;
using MissionControl.Models.DTOs;
using MissionControl.Statics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MissionControl.Models.BIZs
{
    public class ApiKeyBIZ
    {
        DBContext _db;
        public long Id { get; set; }
        public string Key { get; set; }
        public string Email { get; set; }

        public string KeyExists(string key)
        {
            using (_db = new DBContext())
            {
                IQueryable<ApiKeys> _q = _db.apikeys
                    .Where(x => x.Key == key);

                ApiKeys _f = _q.AsEnumerable().FirstOrDefault();

                if (_f.IsNull())
                    return null;

                return _f.Key;
            }
        }/**/

        private bool KeyExists(string email, DBContext _db, out ApiKeys _k)
        {
            IQueryable<ApiKeys> _q = _db.apikeys
                .Where(x => x.Email == email);

            _k = _q.AsEnumerable().FirstOrDefault();

            if (_k.IsNull())
                return false;

            return true;
        }/**/        

        public string CreateApiKey(string email)
        {
            if (email.IsNull())
                throw new Exception("some error");

            string key = ApiKeyGenerator.CreateApiKey();

            using (DBContext _db = new DBContext())
            {
                ApiKeys _k;
                if (KeyExists(email, _db, out _k))
                    UpdateApiKey(_k, email, key);
                else
                {
                    ApiKeys __a = new ApiKeys();
                    __a.Key = key;
                    __a.Email = email;
                
                    _db.apikeys.Add(__a);
                }
                
                _db.SaveChanges();
            }

            return key;
        }

        private void UpdateApiKey(ApiKeys _k, string email, string key)
        {
            if (email.IsNull())
                throw new Exception("some error");

            if (key.IsNull())
                throw new Exception("some error");
            
            _k.Key = key;
            _k.Email = email;
        }


        public List<ApiKeyDTO> ListToDTO(List<ApiKeys> _a)
        {
            if (_a.IsNull())
                throw new Exception();

            List<ApiKeyDTO> list = new List<ApiKeyDTO>();
            foreach (ApiKeys a in _a)
                list.Add(this.ToDTO(a));

            return list;
        }

        public ApiKeyDTO ToDTO(ApiKeys _a)
        {
            if (_a.IsNull())
                throw new Exception();

            ApiKeyDTO dto = new ApiKeyDTO();

            dto.Id = _a.Id;
            dto.Key = !_a.Key.IsNull() ? _a.Key : "";
            dto.Email = !_a.Email.IsNull() ? _a.Email : "";

            return dto;
        }

        public ApiKeyDTO ToDTO(string key, string email)
        {
            if (key.IsNull())
                throw new Exception();

            if (email.IsNull())
                throw new Exception();

            ApiKeyDTO dto = new ApiKeyDTO();

            dto.Id = -1;
            dto.Key = !key.IsNull() ? key : "";
            dto.Email = !email.IsNull() ? email : "";

            return dto;
        }
    }
}