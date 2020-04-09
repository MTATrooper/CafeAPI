using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using CafeClientWeb.Models;

namespace CafeClientWeb.DAO
{
    public class UserDAO
    {
        private ConnectApi connect = new ConnectApi();
        public User FindEmail(string email)
        {
            var user = db.Users.SingleOrDefault(x => x.EMAIL == email);
            return user;
        }
        public User FindUserName(string name)
        {
            var user = db.Users.SingleOrDefault(x => x.TENDANGNHAP == name);
            return user;
        }
        public int CreateUSer(User user)
        {
            var checkName = FindUserName(user.TENDANGNHAP);
            var checkEmail = FindEmail(user.EMAIL);
            if (checkName == null)
            {
                if (checkEmail == null)
                {
                    if (user.QUYEN == null) user.QUYEN = 0;
                    connect.SendPostRequest(user, "/api/NGUOIDUNG/");
                    return 1;
                }
                else
                {
                    // ton tai email
                    return 0;
                }
            }
            // ten dang nhap ton tai
            return -1;
        }

        // login -- client

        public int LoginClient(string UserName, string Password)
        {
            var user = db.Users.SingleOrDefault(x => x.TENDANGNHAP == UserName && x.MATKHAU == Password);
            if (user != null)
            {
                return 1;
            }
            return -1;
        }
    }
}