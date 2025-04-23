using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Natillera_Eventos_Parcial.Models;

namespace Natillera_Eventos_Parcial.Clases
{
    public class clsEventos
    {
        private EVENTOS_NATILLERAEntities db = new EVENTOS_NATILLERAEntities();

        public IQueryable<Evento> Listar(string tipo = null, string nombre = null, DateTime? fecha = null)
        {
            var query = db.Eventos.AsQueryable();

            if (!string.IsNullOrEmpty(tipo))
                query = query.Where(e => e.TipoEvento.Contains(tipo));
            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(e => e.NombreEvento.Contains(nombre));
            if (fecha.HasValue)
                query = query.Where(e => e.FechaEvento == fecha.Value);

            return query;
        }

        public bool Crear(Evento nuevo)
        {
            try
            {
                db.Eventos.Add(nuevo);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Actualizar(Evento actualizado)
        {
            try
            {
                var original = db.Eventos.Find(actualizado.idEventos);
                if (original == null) return false;

                original.TipoEvento = actualizado.TipoEvento;
                original.NombreEvento = actualizado.NombreEvento;
                original.TotalIngreso = actualizado.TotalIngreso;
                original.FechaEvento = actualizado.FechaEvento;
                original.Sede = actualizado.Sede;
                original.ActiviadesPlaneadas = actualizado.ActiviadesPlaneadas;

                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }

        public bool Eliminar(int id)
        {
            try
            {
                var ev = db.Eventos.Find(id);
                if (ev == null) return false;

                db.Eventos.Remove(ev);
                db.SaveChanges();
                return true;
            }
            catch { return false; }
        }
    }
}