using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lib.SchoolModel;
using Lib.SchoolModel.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Lib.Service.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly AppSetting _appSetting = null;
        public StudentService(IOptions<AppSetting> appSetting)
        {
            _appSetting = appSetting.Value;
        }



        public async Task<List<Student>> GetStudents()
        {
            var client = new HttpClient();
            var Studentlist = new List<Student>();
            try
            {
                
                    client.BaseAddress = new Uri(_appSetting.WebAPILink);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var response = await client.GetAsync("SchoolWebAPI/api/schoolAPI");

                    if (response.IsSuccessStatusCode)
                    {
                        var readTask = response.Content.ReadAsStringAsync().Result;

                        Studentlist = JsonConvert.DeserializeObject<List<Student>>(readTask);
                        return Studentlist;
                    }
                
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Studentlist;
        }


        public async Task<int> SaveStudent(Student std)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSetting.WebAPILink);
                client.DefaultRequestHeaders.Clear();
                string jsonData = JsonConvert.SerializeObject(std);
                var content = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");
                var response = await client.PostAsync("SchoolWebAPI/api/schoolAPI", content);

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<int>(readTask);
                    return result;
                }

            }
            return 0;


        }


        public async Task<string> DeleteStudent(int Id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_appSetting.WebAPILink);
                client.DefaultRequestHeaders.Clear();
                var response = await client.DeleteAsync("/api/SchoolAPI/" + Id.ToString());

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync().Result;

                    var result = JsonConvert.DeserializeObject<string>(readTask);
                    //  var result = JsonConvert.DeserializeObject<string>)(readTask);
                    return result;
                }

            }
            return "";

        }



    }
}
