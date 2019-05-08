using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using WebAPI_Giris.Models;

namespace WebAPI_Giris.Controllers.MVC
{
    public class StudentController : Controller
    {
        // GET: Student
        public ActionResult Index()
            
        {

            IEnumerable<StudentViewModel> students = null;
            using (var client = new HttpClient())
            {
                //client.BaseAddress = new Uri("http://localhost:49853/api/");
                client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority) + "/api/Student" );
                //client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/xml"));

                //HTTP GET
                var responseTask = client.GetAsync(client.BaseAddress);
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<IList<StudentViewModel>>();
                    readTask.Wait();

                    students = readTask.Result;
                }
                else
                {

                    students = Enumerable.Empty<StudentViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator");
                }
            }
            return View(students);
               


        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(StudentViewModel student)
        {
            using(var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority) + "/api/Student");

                var postTask = client.PostAsJsonAsync<StudentViewModel>("student", student);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty, "Server Error. Please contact administrator.");

                return View(student);
            }

        }
        public ActionResult Edit(int id)
        {
            StudentViewModel student = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority) + "/api/");
                //HTTP GET
                var responseTask = client.GetAsync("student?id=" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<StudentViewModel>();
                    readTask.Wait();
                    student = readTask.Result;
                }
            }
            return View(student);
        }

        [System.Web.Mvc.HttpPost]
        public ActionResult Edit(StudentViewModel student)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority) + "/api/student");
                //Http post
                var putTask = client.PutAsJsonAsync<StudentViewModel>("student", student);
                putTask.Wait();

                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(student);
        }

        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(Request.Url.GetLeftPart(UriPartial.Authority) + "/api/");
                //Http Delete
                var deleteTask = client.DeleteAsync("student/" + id.ToString());
                deleteTask.Wait();

                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }
    }
}