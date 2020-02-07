using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer;

namespace Domicilio.Controllers
{
    public class PedidosController : Controller
    {
        private pedidosEntities db = new pedidosEntities();

        // GET: Pedidos
        public async Task<ActionResult> Index()
        {
            var pedidos = db.Pedidos.Include(p => p.Clientes).Include(p => p.Productos);
            return View(await pedidos.ToListAsync());
        }

        // GET: Pedidos/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = await db.Pedidos.FindAsync(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            return View(pedidos);
        }

        // GET: Pedidos/Create
        public ActionResult Create()
        {
            ViewBag.id_cliente = new SelectList(db.Clientes, "id", "cedula");
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "plu");
            return View();
        }

        // POST: Pedidos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id_pedido,nombre_recibe,observacion_pedido,observacion_producto,total_productos,valor_total,id_producto,id_cliente")] Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                db.Pedidos.Add(pedidos);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.id_cliente = new SelectList(db.Clientes, "id", "cedula", pedidos.id_cliente);
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "plu", pedidos.id_producto);
            return View(pedidos);
        }

        // GET: Pedidos/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = await db.Pedidos.FindAsync(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_cliente = new SelectList(db.Clientes, "id", "cedula", pedidos.id_cliente);
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "plu", pedidos.id_producto);
            return View(pedidos);
        }

        // POST: Pedidos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id_pedido,nombre_recibe,observacion_pedido,observacion_producto,total_productos,valor_total,id_producto,id_cliente")] Pedidos pedidos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pedidos).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.id_cliente = new SelectList(db.Clientes, "id", "cedula", pedidos.id_cliente);
            ViewBag.id_producto = new SelectList(db.Productos, "id_producto", "plu", pedidos.id_producto);
            return View(pedidos);
        }

        // GET: Pedidos/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pedidos pedidos = await db.Pedidos.FindAsync(id);
            if (pedidos == null)
            {
                return HttpNotFound();
            }
            return View(pedidos);
        }

        // POST: Pedidos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Pedidos pedidos = await db.Pedidos.FindAsync(id);
            db.Pedidos.Remove(pedidos);
            await db.SaveChangesAsync();
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
