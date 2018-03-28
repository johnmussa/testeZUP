using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TesteZUPJAMP.Models;

namespace TesteZUPJAMP.Controllers
{
    public class POIController : Controller
    {
        private DBPOIContext db = new DBPOIContext();
        

        // GET: POIs
        public ActionResult Index()
        {
            return View(db.PointsOfInterest.Where(i => !i.name.Contains("DISTANCIA")).ToList());
        }

        public ActionResult ListaProximidades()
        {
            try
            {
                POI anchor = db.PointsOfInterest.Where(i => i.name.Contains("DISTANCIA")).FirstOrDefault();
                var distanciaMax = int.Parse(anchor.name.Substring(9));

                if(anchor == null)
                    return HttpNotFound();
                else
                {
                    var pontos = db.PointsOfInterest.Where(i => !i.name.Contains("DISTANCIA")).ToList();
                    List<POI> retorno = new List<POI>();

                    foreach(POI ponto in pontos)
                    {
                        var dist = Math.Sqrt(Math.Pow((ponto.pos_x - anchor.pos_x),2) + Math.Pow((ponto.pos_y - anchor.pos_y),2));

                        if(dist < distanciaMax)
                            retorno.Add(ponto);
                    }
                    
                    return View(retorno);
                }
            }
            catch
            {
                return HttpNotFound();
            }
        }

        // GET: POIs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = db.PointsOfInterest.Find(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            return View(pOI);
        }
        
        // GET: POIs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: POIs/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,pos_x,pos_y")] POI pOI)
        {
            if (ModelState.IsValid)
            {
                db.PointsOfInterest.Add(pOI);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pOI);
        }

        // GET: POIs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = db.PointsOfInterest.Find(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            return View(pOI);
        }

        // GET: POIs/BuscaProx
        public ActionResult BuscaProx()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BuscaProx([Bind(Include = "id,name,pos_x,pos_y")] POI pOI)
        {
            POI anchor = db.PointsOfInterest.Where(i => i.name.Contains("DISTANCIA")).FirstOrDefault();
            if (anchor == null)
            {
                POI P = new POI();
                P.name = "DISTANCIA" + pOI.name; //como Valor da distância máxima
                P.pos_x = pOI.pos_x;
                P.pos_y = pOI.pos_y;
                db.PointsOfInterest.Add(P);
                db.SaveChanges();
            }
            else
            {
                anchor.name = "DISTANCIA" + pOI.name; //como Valor da distância máxima
                anchor.pos_x = pOI.pos_x;
                anchor.pos_y = pOI.pos_y;
                db.Entry(anchor).State = EntityState.Modified;
                db.SaveChanges();
            }

            return RedirectToAction("ListaProximidades");
        }

        // POST: POIs/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,pos_x,pos_y")] POI pOI)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pOI).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pOI);
        }

        // GET: POIs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            POI pOI = db.PointsOfInterest.Find(id);
            if (pOI == null)
            {
                return HttpNotFound();
            }
            return View(pOI);
        }

        // POST: POIs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            POI pOI = db.PointsOfInterest.Find(id);
            db.PointsOfInterest.Remove(pOI);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
