using System;
using System.IO;
using ExcelDataReader;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace LePlanilhaMS
{
    class Program
    {
        static void Main(string[] args)
        {

            var x = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json");


            IConfigurationRoot configurationRoot = new ConfigurationBuilder()
                                  .AddJsonFile("appsettings.json", true, true)
                                  .Build();

            IConfiguration config = configurationRoot;


            Model.DB.strcn = config.GetConnectionString("relacional");
            Console.WriteLine(Model.DB.strcn);

            System.Data.DataTable dados = LeExcel.LeConteudo(@"D:\Fontes\GitHub\covid\FontesDados\HIST_PAINEL_COVIDBR_19mai2020.xlsx")[0];
            ImportaEstados(dados);
            ImportaMunicipio(dados);
            ImportaDados(dados);

            Console.WriteLine("FIM");
        }

        static void ImportaDados(System.Data.DataTable origem)
        {
            using (var db = new Model.DB())
            {
                using (var cn = db.Database.GetDbConnection())
                {
                    using (var cmd = cn.CreateCommand())
                    {
                        cmd.Connection.Open();
                        cmd.CommandText = "truncate table [dbo].[DadosMS]";
                        cmd.ExecuteNonQuery();
                    }
                }


            }

            using (var db = new Model.DB())
            {
                for (int i = 1; i < origem.Rows.Count; i++)
                {
                    System.Data.DataRow ln = origem.Rows[i];
                    Model.DadosMS novo = new Model.DadosMS(ln);
                    db.Add(novo);
                    if (i % 1000 == 0)
                        db.SaveChanges();
                }
                db.SaveChanges();

            }
        }
        static void ImportaMunicipio(System.Data.DataTable origem)
        {
            using (var db = new Model.DB())
            {
                int idmuni = 0;

                for (int i = 1; i < origem.Rows.Count; i++)
                {
                    System.Data.DataRow ln = origem.Rows[i];
                    if (int.TryParse(ln[4].ToString(), out int vidmuni))
                    {

                        if (idmuni != vidmuni)
                        {
                            Model.Municipio novo = new Model.Municipio(ln);
                            idmuni = vidmuni;
                            var localizado = db.Municipio.Find(novo.Id);
                            if (localizado == null)
                            {
                                db.Add(novo);
                            }

                        }
                    }
                }
                db.SaveChanges();
            }
        }
        static void ImportaEstados(System.Data.DataTable origem)
        {
            using (var db = new Model.DB())
            {
                string vEstado = string.Empty;
                for (int i = 1; i < origem.Rows.Count; i++)
                {
                    System.Data.DataRow ln = origem.Rows[i];
                    if (string.IsNullOrEmpty(ln[4].ToString().Trim()))
                    {
                        if (vEstado != ln[1].ToString())
                        {
                            //Console.WriteLine(ln[1].ToString());
                            Model.Estado novo = new Model.Estado(ln);

                            var localizado = db.Estado.Where(p => p.Id == novo.Id).FirstOrDefault();
                            if (localizado == null)
                            {
                                db.Add(novo);
                            }

                        }
                    }
                }
                db.SaveChanges();

            }
        }
    }
}
