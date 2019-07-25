using jganchozo.salaespera.data;
using jganchozo.salaespera.entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jganchozo.salaespera.business
{
    public class PersonaCola
    {
        private BaseDatos objBase = new BaseDatos();
        public SqlParameter[] Mapeo(PersonaColaEntity Ent)
        {
            SqlParameter[] parametros = {
                new SqlParameter("@name", Ent.PersonaNombre),
                new SqlParameter("@id", Ent.IdPersona),
    };
            return parametros;
        }

        public List<PersonaColaEntity> Guardar(PersonaColaEntity Ent)
        {
            List<PersonaColaEntity> lista = new List<PersonaColaEntity>();
            try
            {
                SqlParameter[] parametros = Mapeo(Ent);
                lista = objBase.InsertarDatos(parametros, "SP_GUARDARNUEVOTURNO");
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return lista;
        }

        public List<PersonaColaEntity> GetData(PersonaColaEntity Ent)
        {
            List<PersonaColaEntity> lista = new List<PersonaColaEntity>();
            try
            {
                SqlParameter[] parametros = Mapeo(Ent);
                DataSet ds = objBase.dsEjecutarProcedimiento("SP_GETDATA", parametros);
                foreach (DataRow item in ds.Tables[0].Rows)
                {
                    int numeroAtencion = 0;
                    PersonaColaEntity persona = new PersonaColaEntity();
                    numeroAtencion = int.Parse(item[0].ToString());
                    persona.NumeroCola = int.Parse(item[1].ToString());
                    persona.NumeroAtencion = numeroAtencion.ToString().PadLeft(4, '0');
                    lista.Add(persona);
                }
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return lista;
        }
    }
}
