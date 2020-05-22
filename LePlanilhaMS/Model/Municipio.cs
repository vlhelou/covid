

namespace LePlanilhaMS.Model
{
    public class Municipio : CEP
    {
        public Municipio() { }

        public Municipio(System.Data.DataRow ln)
        {
            if (int.TryParse(ln[4].ToString(), out int id))
                Id = id;
            Nome = ln[2].ToString();
            if (int.TryParse(ln[9].ToString(), out int pop))
                Populacao = pop;
            if (int.TryParse(ln[3].ToString(), out int idestado))
                EstadoId = idestado;
        }


        public int EstadoId { get; set; }
        public Estado Estado { get; set; }
    }
}