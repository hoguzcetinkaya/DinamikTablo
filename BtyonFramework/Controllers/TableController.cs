using BtyonFramework.Db;
using BtyonFramework.Models;
using BtyonFramework.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BtyonFramework.Controllers
{
    public class TableController : Controller
    {
        DataContext db = new DataContext();

        public ActionResult TableDesign()
        {
            var colums = db.Colums.ToList();
            
            return View(colums);
        }
        public ActionResult ColumAdd(string colum)
        {
            //Sütun ekleme
            ColumsModel col = new ColumsModel();
            col.ColumsName = colum;
            db.Colums.Add(col);

            //log kaydı ekleme
            LogModel logData = new LogModel();
            logData.Log = $"Sütun eklendi :{colum}";
            db.Log.Add(logData);
            db.SaveChanges();
            return RedirectToAction("TableDesign");
        }
        public ActionResult ColumDelete(int id)
        {
            var sutun = db.Colums.FirstOrDefault(x => x.Id == id);


            db.Colums.Remove(sutun);
            //log kaydı ekleme
            LogModel logData = new LogModel();
            logData.Log = $"Sütun silindi :{sutun.ColumsName}";
            db.Log.Add(logData);
            db.SaveChanges();



            return RedirectToAction("TableDesign");
        }

        public ActionResult TableView()
        {
            var colums = db.Colums.ToList();
            ViewBag.Datas = db.Data.ToList();
            return View(colums);
        }
        public ActionResult FormView()
        {
            var colums = db.Colums.ToList();
            if (colums.Count() == 0)
            {
                var datas = db.Data.ToList();
                foreach (var item in datas)
                {
                    db.Data.Remove(item);
                }
                //log kaydı ekleme
                LogModel logData = new LogModel();
                logData.Log = $"Tüm sütunlar ve veriler silindi.";
                db.Log.Add(logData);
                db.SaveChanges();
            }
            return View(colums);
        }
        public ActionResult DataAdd(List<string> data)
        {
            //string veri1, veri2;
            //veri1 = data[0];
            //veri2 = data[0];
            //for (int i = 1; i < data.Count(); i++)
            //{
            //    veri1 = veri1 + "\n" + data[i];
            //}
            //DataModel datas = new DataModel();
            //datas.Data = veri1;
            //db.Data.Add(datas);
            //db.SaveChanges();

            DataModel datas = new DataModel();
            string veri1 = data[0];
            for (int i = 1; i < data.Count(); i++)
            {
                if (data[i] == "")
                {
                    data[i] = " ";
                    veri1 = veri1 + data[i];
                }
                else
                {
                    veri1 = veri1 + " " + data[i];
                }
            }
            datas.Data = veri1;
            db.Data.Add(datas);

            //log kaydı ekleme
            LogModel logData = new LogModel();
            logData.Log = $"Kayıt eklendi :{veri1}";
            db.Log.Add(logData);
            db.SaveChanges();
            return RedirectToAction("FormView");
        }
        public ActionResult RowDelete(int id)
        {
            var row = db.Data.FirstOrDefault(x => x.Id == id);
            db.Data.Remove(row);
            //log kaydı ekleme
            LogModel logData = new LogModel();
            logData.Log = $"Satır silindi Id:{row.Id}";
            db.Log.Add(logData);
            db.SaveChanges();
            return RedirectToAction("TableView");
        }
        [HttpGet]
        public ActionResult RowUpdate(int id)
        {
            var colums = db.Colums.ToList();
            ViewBag.Row = db.Data.Where(x => x.Id == id).ToList();
            TempData["RowId"] = id;
            
            return View("FormView", colums);
        }
        [HttpPost]
        public ActionResult RowUpdate(List<string> data)
        {
            int rowId = Int32.Parse(TempData["RowId"].ToString());
            var row = db.Data.FirstOrDefault(x => x.Id == rowId);
            string veri1 = data[0];
            for (int i = 1; i < data.Count(); i++)
            {
                if (data[i] == "")
                {
                    data[i] = " ";
                    veri1 = veri1 + data[i];
                }
                else
                {
                    veri1 = veri1 + " " + data[i];
                }
            }
            row.Data = veri1;
            //log kaydı ekleme
            LogModel logData = new LogModel();
            logData.Log = $"{row.Id} Id'li Satır güncellendi :{veri1}";
            db.Log.Add(logData);
            db.SaveChanges();
            return RedirectToAction("TableView");
        }
        public ActionResult SystemLogs()
        {
            return View(db.Log.ToList());
        }
    }
}