using Lab3_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Lab3_mvc.Controllers
{
    public class StudentController : Controller
    {
        //StudentMoc db = new StudentMoc();

        public IStudent db;
        public StudentController(IStudent _db)
        {
            db = _db;
        }

        public IActionResult Index()
        {
            List<Student> model = db.GetAllStudent();
            return View(model);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Student std,IFormFile imgsrc)
        {
            std.Id = db.GetNextId();
            if (imgsrc != null)
            {
                string imgname = std.Id.ToString() +"."+ imgsrc.FileName.Split(".")[1];
                using (var obj = new FileStream(@".\wwwroot\images\" + imgname, FileMode.Create))
                {
                    imgsrc.CopyTo(obj);
                }
                std.stdImg = imgname; 
            }
            db.AddStudents(std);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Student std = db.GetStudentById(id.Value);
            if(std == null)
            {
                return NotFound();
            }
            return View(std);
        }
           

        public IActionResult Delete(int? id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Student std = db.GetStudentById(id.Value);
            if(std == null)
            {
                return NotFound(); 
            }
            db.DeleteById(id.Value);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Student std = db.GetStudentById(id.Value);
            if (std == null)
            {
                return NotFound();
            }
            //db.EditStudents(std);
            return View(std);
        }

        [HttpPost]
        public IActionResult Edit(Student std)
        {
            db.EditStudents(std);
            return RedirectToAction("Index");
        }

        public IActionResult AddImage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EditImg(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Student std = db.GetStudentById(id.Value);
            if (std == null)
            {
                return NotFound();
            }
            return View(std);
        }

        [HttpPost]
        public IActionResult EditImg(int id,IFormFile stdImg)
        {
            Student std = db.GetStudentById(id);
            string imgName = id.ToString() + "." + std.stdImg.Split(".")[1];
            if (stdImg != null)
            {
                using (var obj = new FileStream(@".\wwwroot\images\" + imgName, FileMode.OpenOrCreate))
                {
                    stdImg.CopyTo(obj);
                }
                db.Editimg(id,imgName);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Download(int? id)
        {
            if (id == null)
                return BadRequest();
            Student std = db.GetStudentById(id.Value);
            if (std == null)
                return NotFound();
            return File("~/images/" + std.stdImg, "image/jpg", std.Name + ".png");
        }

    }
}
