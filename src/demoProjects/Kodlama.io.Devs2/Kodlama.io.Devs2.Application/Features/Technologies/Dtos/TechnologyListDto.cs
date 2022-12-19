using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Application.Features.Technologies.Dtos;

public class TechnologyListDto
{
    // Join İşlemi için kullanacağım sınıf. Hangi Dataları koymak istiyorsak onları yazıyoruz
    public int Id { get; set; }
    public string Name { get; set; }
    public string ProgrammingLanguageName { get; set; } // Programlama Dili adı ( Diğer Tablodan Alacağız)   İstediklerimi verebiliriz.
}
