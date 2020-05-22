using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LePlanilhaMS.Model
{
    public class DadosMS
    {
        public DadosMS() { }
        public DadosMS(System.Data.DataRow linha)
        {
            Data = Convert.ToDateTime(linha[7].ToString());
            SemanaEpi = Convert.ToInt32(linha[8].ToString());

            if (int.TryParse(linha[10].ToString(), out int casoacumlado))
            {
                CasosAcumulados = casoacumlado;
            }
            else
            {
                CasosAcumulados = 0;
            }


            if (int.TryParse(linha[11].ToString(), out int obitoaculado))
            {
                ObitosAcumulados = obitoaculado;
            }
            else
            {
                ObitosAcumulados = 0;
            }


            if (int.TryParse(linha[12].ToString(), out int recuperado))
            {
                RecuperadosAcumulados = recuperado;
            }
            else
            {
                RecuperadosAcumulados = 0;
            }

            if (int.TryParse(linha[13].ToString(), out int acompanhamento))
            {
                EmAcompanhamentoAcumulados = acompanhamento;
            }
            else
            {
                EmAcompanhamentoAcumulados = 0;
            }

            if (int.TryParse(linha[4].ToString(), out int municipioid))
            {
                MunicipioId = municipioid;
            }
            else
            {
                if (int.TryParse(linha[3].ToString(), out int estadoid))
                {
                    if (estadoid == 76)
                        PaisId = estadoid;
                    else
                        EstadoId = estadoid;
                }
            }


        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public DateTime Data { get; set; }
        public int? PaisId { get; set; }
        public int? EstadoId { get; set; }
        public int? MunicipioId { get; set; }

        public int SemanaEpi { get; set; }
        public int CasosAcumulados { get; set; }
        public int CasosDia { get; set; }
        public int ObitosAcumulados { get; set; }
        public int ObitosDia { get; set; }
        public int RecuperadosAcumulados { get; set; }
        public int RecuperadosDia { get; set; }
        public int EmAcompanhamentoAcumulados { get; set; }
        public int EmAcompanhamentoDia { get; set; }
    }
    
}