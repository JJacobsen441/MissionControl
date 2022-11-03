using MissionControl.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MissionControl.Statics
{
    public class CheckHelper
    {
        public static bool IsValidEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return false;

                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private static string Check(string val, bool decode, bool allow_upper, int len, bool allow_newline, bool allow_numeric, List<string> allow_tag, char[] allowed, out bool _ok)
        {
            if (!val.IsNullOrEmpty())
            {
                if (val.Length >= len)
                {
                    val = val.Trim();
                    
                    if(decode )
                        val = HttpUtility.UrlDecode(val);
                    
                    if (!allow_upper)
                        val = val.ToLower();

                    val = ValidateHelper.Sanitize(val, allow_newline, allow_upper, allow_numeric, allow_tag, allowed, out _ok);
                    if (_ok)
                        return val;
                }
            }
            _ok = false;
            return val;
        }

        private static long CheckID(long val, out bool _ok)
        {
            _ok = true;
            if (val == 0)
                _ok = false;

            if (val < 0)
                _ok = false;

            return val;
        }

        private static double CheckTude(double val, out bool _ok)
        {
            /*
             * some checks for latitude an longitude
             * */
            _ok = true;

            return val;
        }

        private static long CheckDate(long val, out bool _ok)
        {
            /*
             * some checks for dates
             * */
            _ok = true;

            return val;
        }

        public static bool CheckUser(ViewModelUserPost model)
        {
            if (model.IsNull())
                return false;

            /*
             * use fx 'no_tag' for no tags
             * */
            bool _ok_a, _ok_b, _ok_c, _ok_d, _ok_e, _ok_f, _ok_g;
            model.first_name = Check(model.first_name, false, true, 0, false, true, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_a);
            model.last_name = Check(model.last_name, false, true, 0, false, true, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_b);
            model.code_name = Check(model.code_name, false, true, 4, false, true, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_c);
            model.user_name = Check(model.user_name, false, true, 4, false, true, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_d);
            model.email = Check(model.email, false, false, 5, false, true, new List<string>() { "no_tag" }, CharacterHelper.All(false), out _ok_e);
            model.password = Check(model.password, false, true, 6, false, true, new List<string>() { "no_tag" }, CharacterHelper.All(false), out _ok_f);
            model.avatar = Check(model.avatar, false, true, 0, false, true, new List<string>() { "no_tag" }, CharacterHelper.All(false), out _ok_g);

            return _ok_a && _ok_b && _ok_c && _ok_d && _ok_e && _ok_f && _ok_g;
        }

        public static bool CheckUser(ViewModelUserPut model)
        {
            if (model.IsNull())
                return false;

            /*
             * use fx 'no_tag' for no tags
             * */
            bool _ok_a, _ok_b, _ok_c, _ok_d, _ok_e, _ok_f, _ok_g;
            model.first_name = Check(model.first_name, false, true, 0, false, true, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_a);
            model.last_name = Check(model.last_name, false, true, 0, false, true, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_b);
            model.code_name = Check(model.code_name, false, true, 4, false, true, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_c);
            model.user_name = Check(model.user_name, false, true, 4, false, true, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_d);
            model.email = Check(model.email, false, false, 5, false, true, new List<string>() { "no_tag" }, CharacterHelper.All(false), out _ok_e);
            model.password = Check(model.password, false, true, 6, false, true, new List<string>() { "no_tag" }, CharacterHelper.All(false), out _ok_f);
            model.avatar = Check(model.avatar, false, true, 0, false, true, new List<string>() { "no_tag" }, CharacterHelper.All(false), out _ok_g);

            return _ok_a && _ok_b && _ok_c && _ok_d && _ok_e && _ok_f && _ok_g;
        }





        public static bool CheckMissionReport(ViewModelMissionreportPost model)
        {
            if (model.IsNull())
                return false;

            /*
             * use fx 'no_tag' for no tags
             * */
            bool _ok_a, _ok_b, _ok_c, _ok_d, _ok_e, _ok_f, _ok_g, _ok_h, _ok_i, _ok_j;
            model.name = Check(model.name, false, true, 0, false, true, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_a);
            model.description = Check(model.description, false, true, 0, true, true, new List<string>() { "b", "strong" }, CharacterHelper.All(true), out _ok_b);
            model.lat = CheckTude(model.lat, out _ok_c);
            model.lng = CheckTude(model.lng, out _ok_d);
            model.mission_date = CheckDate(model.mission_date, out _ok_e);
            model.finalization_date = CheckDate(model.finalization_date, out _ok_f);
            model.created_at = CheckDate(model.created_at, out _ok_g);
            model.updated_at = CheckDate(model.updated_at, out _ok_h);
            model.deleted_at = CheckDate(model.deleted_at, out _ok_i);
            model.user_id = CheckID(model.user_id, out _ok_j);

            return _ok_a && _ok_b && _ok_c && _ok_d && _ok_e && _ok_f && _ok_g && _ok_h && _ok_i && _ok_j;
        }

        public static bool CheckMissionReport(ViewModelMissionreportPut model)
        {
            if (model.IsNull())
                return false;

            /*
             * use fx 'no_tag' for no tags
             * */
            bool _ok_a, _ok_b, _ok_c, _ok_d, _ok_e, _ok_f, _ok_g, _ok_h, _ok_i, _ok_j;
            model.name = Check(model.name, false, true, 0, false, true, new List<string>() { "no_tag" }, CharacterHelper.Name(), out _ok_a);
            model.description = Check(model.description, false, true, 0, true, true, new List<string>() { "b", "strong" }, CharacterHelper.All(false), out _ok_b);
            model.lat = CheckTude(model.lat, out _ok_c);
            model.lng = CheckTude(model.lng, out _ok_d);
            model.mission_date = CheckDate(model.mission_date, out _ok_e);
            model.finalization_date = CheckDate(model.finalization_date, out _ok_f);
            model.created_at = CheckDate(model.created_at, out _ok_g);
            model.updated_at = CheckDate(model.updated_at, out _ok_h);
            model.deleted_at = CheckDate(model.deleted_at, out _ok_i);
            model.user_id = CheckID(model.user_id, out _ok_j);

            return _ok_a && _ok_b && _ok_c && _ok_d && _ok_e && _ok_f && _ok_g && _ok_h && _ok_i && _ok_j;
        }

        public static bool CheckApiKey(ViewModelApiKeyPost model)
        {
            if (model.IsNull())
                return false;

            /*
             * use fx 'no_tag' for no tags
             * */
            bool _ok_a;
            model.email = Check(model.email, false, true, 0, false, true, new List<string>() { "no_tag" }, CharacterHelper.All(false), out _ok_a);
            
            return _ok_a;
        }
    }
}