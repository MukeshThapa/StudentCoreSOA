using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lib.SchoolModel;
using Lib.Service.StudentService;
using Microsoft.AspNetCore.Mvc;

namespace SchoolSOA.Controllers
{
    public class SchoolController : Controller
    {
        private readonly IStudentService _service = null;

        public SchoolController(IStudentService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            
            var stdList = await _service.GetStudents();

            return View(stdList);
        }

        public IActionResult AddStudent() {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(Student std)
        {
            int result = await _service.SaveStudent(std);

            return RedirectToAction("Index");
        }

    }
}
