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

        // Crear un nuevo evento
        public bool Crear(Evento evento)
        {
            try
            {
                // ⚠️ Asignar un ID de administrador válido si es necesario
                if (evento.idAdministrador == 0)
                    evento.idAdministrador = 1; // Ajusta según tu lógica de autenticación

                db.Eventos.Add(evento);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        // Listar eventos con filtros opcionales
        public List<Evento> Listar(string tipo, string nombre, DateTime? fecha)
        {
            var query = db.Eventos.AsQueryable();  // Asegúrate de usar db

            if (!string.IsNullOrEmpty(tipo))
                query = query.Where(e => e.TipoEvento.Contains(tipo));

            if (!string.IsNullOrEmpty(nombre))
                query = query.Where(e => e.NombreEvento.Contains(nombre));

            if (fecha.HasValue)
                query = query.Where(e => e.FechaEvento == fecha.Value);

            return query.ToList();  // 🔥 sin Select, ni DTO
        }




        // Actualizar evento
        public bool Actualizar(Evento evento)
        {
            try
            {
                var existente = db.Eventos.FirstOrDefault(e => e.idEventos == evento.idEventos);
                if (existente == null)
                {
                    Console.WriteLine("⚠️ Evento no encontrado.");
                    return false;
                }

                existente.TipoEvento = evento.TipoEvento;
                existente.NombreEvento = evento.NombreEvento;
                existente.TotalIngreso = evento.TotalIngreso;
                existente.FechaEvento = evento.FechaEvento;
                existente.Sede = evento.Sede;
                existente.ActividadesPlaneadas = evento.ActividadesPlaneadas;

                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                // 🔍 Mostramos el error detallado
                Console.WriteLine("❌ ERROR AL ACTUALIZAR: " + ex.ToString());
                return false;
            }
        }




        // Eliminar evento por ID
        public bool Eliminar(int id)
        {
            try
            {
                var evento = db.Eventos.FirstOrDefault(e => e.idEventos == id);
                if (evento == null)
                {
                    Console.WriteLine("Evento no encontrado");
                    return false;
                }

                db.Eventos.Remove(evento);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
            }
        }







    }


}