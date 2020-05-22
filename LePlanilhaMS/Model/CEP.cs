using System.ComponentModel.DataAnnotations;

namespace LePlanilhaMS.Model
{
    public abstract class CEP
    {
        [Key]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Populacao { get; set; }

    }
}