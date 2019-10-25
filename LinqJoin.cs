using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TesteLinqLambda
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<MyTest> CRM = new List<MyTest> {
                new MyTest  {Name = "Pedro",    Cnpj = "1", Id ="1", Sobrenome="Vidal"},
                new MyTest  {Name = "Mauricio", Cnpj = "2", Id ="2", Sobrenome="Linhares"},
                new MyTest  {Name = "Fernando", Cnpj = "3", Id ="3", Sobrenome="Pinheiro"},
                new MyTest  {Name = "Júlio",    Cnpj = "4", Id ="4", Sobrenome="Veloso"},
                new MyTest  {Name = "Victor",   Cnpj = "5", Id ="5", Sobrenome="Scatolin"},
            };

            List<MyTest> DATALAKE = new List<MyTest> {
                new MyTest  {Name = "Waldyr",   Cnpj = "1", Id ="null", Sobrenome = "Vidal"},
                new MyTest  {Name = "Jorge",    Cnpj = "2", Id ="null", Sobrenome = "Gomes"},
                new MyTest  {Name = "Fernando", Cnpj = "3", Id ="null", Sobrenome = "Vidal"},
                new MyTest  {Name = "Júlio",    Cnpj = "4", Id ="null", Sobrenome = "Veloso"},
                new MyTest  {Name = "Lineu",    Cnpj = "5", Id ="null", Sobrenome = "Almeida"},
            };

            var x = (from crm in CRM
                     join datalake in DATALAKE
                     on new { cnpj = crm.Cnpj }
                     equals new { cnpj = datalake.Cnpj }
                     into aux
                     from datalekeNull in aux.DefaultIfEmpty()
                     select new { datalekeNull, crm }).ToList();

            var item = x.Where(y => y.datalekeNull == null ||
                        ((y.crm.Name != y.datalekeNull.Name) || (y.crm.Sobrenome != y.datalekeNull.Sobrenome))).Select(xy => new MyTest
                        {
                            Name = xy.datalekeNull.Name,
                            Cnpj = xy.crm.Cnpj,
                            Sobrenome = xy.datalekeNull.Sobrenome,
                            Id = xy.crm.Id
                        }).ToList();
        }
    }

    public class MyTest
    {
        public string Name { get; set; }
        public string Cnpj { get; set; }
        public string Id { get; set; }
        public string Sobrenome { get; set; }
    }

}
