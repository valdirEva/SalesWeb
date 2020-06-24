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

        [Required(ErrorMessage ="{0} required")]//anotação de validação para informar que o campo é obrigatório.{0} pega o nome do atributo.
        [StringLength(60, MinimumLength =3, ErrorMessage ="{0} sizeShould be between {2} and {1}")]//anotação para informar o tamanho maximo e minimo de caracteres, e mensagem personalizada de erro.
        public string Name { get; set; }

        [EmailAddress(ErrorMessage = "Enter a valid email")]//anotação para validar formato de email.
        [DataType(DataType.EmailAddress)]// atualiza o formato de email.
        [Required(ErrorMessage = "{0} required")]//anotação de validação para informar que o campo é obrigatório.{0} pega o nome do atributo.
        public string email { get; set; }

        [Required(ErrorMessage = "{0} required")]//anotação de validação para informar que o campo é obrigatório.{0} pega o nome do atributo.
        [Display (Name="Birth Date")]//anotação para exibir o nome que desejar na pagina html.
        [DataType(DataType.Date)]// atualiza o formato de data a ser exibido.
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]// inverte para exibei o dia antes do mes.
        public DateTime BirdthDate { get; set; }


        [Required(ErrorMessage = "{0} required")]//anotação de validação para informar que o campo é obrigatório.{0} pega o nome do atributo.
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]//Informa o valor minimo e maximo do salario.
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
