using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace SalesWebMvc.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]// atualiza o formato de email.
        public string email { get; set; }

        [Display (Name="Birth Date")]//anotação para exibir o nome que desejar na pagina html.
        [DataType(DataType.Date)]// atualiza o formato de data a ser exibido.
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]// inverte para exibei o dia antes do mes.
        public DateTime BirdthDate { get; set; }

        [Display(Name = "Base Salary")]//anotação para exibir o nome que desejar na pagina html.
        [DisplayFormat(DataFormatString ="{0:F2}")]//atualiza o formato de nomero, informa que tera 2 casas deppois do zero.
        public double BaseSalary { get; set; }

        public Department Department { get; set; }
        public int DepartmentId { get; set; }// indica que id é chave estrangeira de Department e não deixa criar com valor nullo.
        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Seller()
        {
        }

        public Seller(int id, string name, string email, DateTime birdthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            this.email = email;
            BirdthDate = birdthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sr)
        {
            Sales.Add(sr);
        }

        public void RemoveSales(SalesRecord sr)
        {
            Sales.Remove(sr);
        }

        public double TotalSales (DateTime initial, DateTime final)
        {
            return Sales.Where(sr => sr.Date >= initial && sr.Date <= final).Sum(sr => sr.Amount);
        }
    }
}
