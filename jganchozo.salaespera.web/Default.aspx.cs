using jganchozo.salaespera.entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using jganchozo.salaespera.business;
using System.Web.Services;

namespace jganchozo.salaespera.web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [WebMethod]
        public static List<PersonaColaEntity> GuardarNuevoTurno(string Id, string Nombre)
        {
            List<PersonaColaEntity> lista = new List<PersonaColaEntity>();
            PersonaCola personaCola = new PersonaCola();
            PersonaColaEntity pc = new PersonaColaEntity() {
                PersonaNombre = Nombre,
                IdPersona = Id
            };
            lista = personaCola.Guardar(pc);
            return lista;
        }

        [WebMethod]
        public static List<PersonaColaEntity> GetData()
        {
            List<PersonaColaEntity> lista = new List<PersonaColaEntity>();
            PersonaCola personaCola = new PersonaCola();
            lista = personaCola.GetData(new PersonaColaEntity() { });
            return lista;
        }
    }
}