using Core.Persistence.Paging;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Dtos;

namespace Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Models
{
    public class ProgrammingLanguageListModel : BasePageableModel
    {
        // Sayfalama için kullanılan sınıf olarak diyebiliriz. Hem Sayfalama verilerini hemde Dto verilerini kullacağız
        public IList<ProgrammingLanguageDto> Items { get; set; } // İsimlendirmeler aynı olması gerekirki mapperda bir daha configuraiton yapmamak için 
    }
}

