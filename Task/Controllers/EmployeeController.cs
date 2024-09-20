using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Task.Models;
using Task.Repo;

namespace Task.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly Repository _repository;

        public EmployeeController()
        {
            _repository = new Repository();
        }

        public ActionResult Index()
        {
            List<Employee> employees = new List<Employee>();
            employees = _repository.GetAllEmployees();
            ViewBag.States = _repository.GetStates();

            return View(employees);
        }

        [HttpGet]
        public JsonResult GetTalukas(int stateId)
        {
            var talukas = _repository.GetTalukas(stateId);
            return Json(talukas, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetCities(int talukhaId)
        {
            var cities = _repository.GetCities(talukhaId);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                if (_repository.AddEmployee(employee))
                {
                    return RedirectToAction("Index");
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public JsonResult AddLocation(int employeeId, string stateName, string talukhaName, string cityName)
        {
            bool success = _repository.UpdateEmployeeLocation(employeeId, stateName, talukhaName, cityName);

            return Json(new { success = success });
        }

        [HttpGet]
        public ActionResult ExcelUpload()
        {
            var orders = _repository.GetOrders();

            return View(orders);
        }

        // install package ExcelDataReader
        [HttpPost]
        public ActionResult ExcelUpload(HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                using (var stream = file.InputStream)
                {
                    using (var reader = ExcelReaderFactory.CreateReader(stream))
                    {
                        // Skip the header row
                        reader.Read();

                        while (reader.Read())
                        {
                            var order = new Orders
                            {
                                OrderID = Convert.ToInt32(reader.GetValue(0)),
                                OrderDate = Convert.ToDateTime(reader.GetValue(1)),
                                OrderQuantity = Convert.ToInt32(reader.GetValue(2)),
                                Sales = Convert.ToInt32(reader.GetValue(3)),
                                ShipMode = reader.GetString(4),
                                Profit = Convert.ToInt32(reader.GetValue(5)),
                                UnitPrice = Convert.ToInt32(reader.GetValue(6)),
                                CustomerName = reader.GetString(7),
                                CustomerSegment = reader.GetString(8),
                                ProductCategory = reader.GetString(9)
                            };

                            _repository.InsertOrder(order);
                        }
                    }
                }
            }
            return RedirectToAction("ExcelUpload");
        }
    }
}