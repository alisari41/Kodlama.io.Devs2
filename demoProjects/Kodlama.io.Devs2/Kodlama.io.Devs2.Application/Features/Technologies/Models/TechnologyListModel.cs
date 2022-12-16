using Core.Persistence.Paging;
using Kodlama.io.Devs2.Application.Features.Technologies.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Application.Features.Technologies.Models
{
    public class TechnologyListModel : BasePageableModel
    {
        // Sayfalama için kullanılan sınıf olarak diyebiliriz. Hem Sayfalama verilerini hemde Dto verilerini kullacağız
        public IList<TechnologyListDto> Items { get; set; } // İsimlendirmeler aynı olması gerekirki mapperda bir daha configuraiton yapmamak için
    }
}
