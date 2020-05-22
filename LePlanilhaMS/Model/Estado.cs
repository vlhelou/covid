

namespace LePlanilhaMS.Model
{
    public class Estado : CEP
    {

        public Estado() { }
        public Estado(System.Data.DataRow ln)
        {
            Nome = ln[1].ToString();
            if (int.TryParse(ln[3].ToString(), out int id))
            {
                Id = id;
            }
            if (int.TryParse(ln[9].ToString(), out int pop))
            {
                Populacao = pop;
            }
            PaisId = 76;

        }
        public int PaisId { get; set; }
        public Pais Pais { get; set; }
    }
}