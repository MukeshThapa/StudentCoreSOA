using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Lib.SchoolModel;
using Newtonsoft.Json;

namespace Lib.Service.StudentService
{
    public class StudentService :IStudentService
    {
        public StudentService()
        {

        }



        public async Task<List<Student>> GetStudents() 
        {

            var Studentlist = new List<Student>();

            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost");
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
                client.BaseAddress = new Uri("http://localhost");
                client.DefaultRequestHeaders.Clear();
                string jsonData = JsonConvert.SerializeObject(std);
                var content = new StringContent(jsonData,UnicodeEncoding.UTF8,"application/json");
                var response = await client.PostAsync("SchoolWebAPI/api/schoolAPI", content);

                if (response.IsSuccessStatusCode)
                {
                    var readTask =  response.Content.ReadAsStringAsync().Result;
                    var result = JsonConvert.DeserializeObject<int>(readTask);
                    return result;
                }

            }



                return 0;
        
        
        }


    }
}
