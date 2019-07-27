using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;

using DouApp.Models;
using System.Threading.Tasks;

namespace DouApp.Databases
{
    public class UsersDatabase
    {
        private string userLoginURL = "https://dohconverter.azurewebsites.net/api/UserLogin";
        private string registerUserURL = "https://dohconverter.azurewebsites.net/api/UserRegister";
        private string usernameExistsURL = "https://dohconverter.azurewebsites.net/api/UsernameExists";
        private string emailExistsUrl = "https://dohconverter.azurewebsites.net/api/EmailExists";
        private string getUserByIDURL = "https://dohconverter.azurewebsites.net/api/GetUserByID";

        public int GetUserID(string username, string password)
        {
            User user = new User
            {
                ID = 0,
                Username = username,
                Email = "",
                Password = password
            };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(userLoginURL);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string jsonOutput = JsonConvert.SerializeObject(user);

                streamWriter.Write(jsonOutput);
                streamWriter.Flush();
                streamWriter.Close();

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    user = JsonConvert.DeserializeObject<User>(result.ToString());
                }
            }

            return user.ID;
        }

        public User GetUserByID(int id)
        {
            User user = new User
            {
                ID = id,
                Username = "",
                Email = "",
                Password = ""
            };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(getUserByIDURL);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string jsonOutput = JsonConvert.SerializeObject(user);

                streamWriter.Write(jsonOutput);
                streamWriter.Flush();
                streamWriter.Close();

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    user = JsonConvert.DeserializeObject<User>(result.ToString());
                }
            }

            if (user.ID == 0)
                return null;

            return user;
        }

        public int RegisterUser(User user)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(registerUserURL);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string jsonOutput = JsonConvert.SerializeObject(user);

                streamWriter.Write(jsonOutput);
                streamWriter.Flush();
                streamWriter.Close();

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    user = JsonConvert.DeserializeObject<User>(result.ToString());
                }
            }

            return user.ID;
        }

        public bool IsUsernameExists(string username)
        {
            User user = new User
            {
                ID = 0,
                Username = username,
                Email = "",
                Password = ""
            };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(usernameExistsURL);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string jsonOutput = JsonConvert.SerializeObject(user);

                streamWriter.Write(jsonOutput);
                streamWriter.Flush();
                streamWriter.Close();

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    user = JsonConvert.DeserializeObject<User>(result.ToString());
                }
            }

            return (user.ID != 0);
        }

        public bool IsEmailExists(string email)
        {
            User user = new User
            {
                ID = 0,
                Username = "",
                Email = email,
                Password = ""
            };

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(emailExistsUrl);
            httpWebRequest.ContentType = "application/json; charset=utf-8";
            httpWebRequest.Method = "POST";
            httpWebRequest.Accept = "application/json; charset=utf-8";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string jsonOutput = JsonConvert.SerializeObject(user);

                streamWriter.Write(jsonOutput);
                streamWriter.Flush();
                streamWriter.Close();

                HttpWebResponse httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    user = JsonConvert.DeserializeObject<User>(result.ToString());
                }
            }

            return (user.ID != 0);
        }
    }
}
