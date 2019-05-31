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
        private string userLoginURL = "https://dohconverter.azurewebsites.net/api/UserLogin?code=UCjmvcAyL9yv6wLPLf5QChd21Dg8tOUMxaoTbrkoKhy4rSO8NIZgrA==";
        private string registerUserURL = "https://dohconverter.azurewebsites.net/api/UserRegister?code=1pvCozgmaGP7Lrega6GGXHmqup5piMPtPVLKlF/Ug4e/52OvlrByCQ==";

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
    }
}
