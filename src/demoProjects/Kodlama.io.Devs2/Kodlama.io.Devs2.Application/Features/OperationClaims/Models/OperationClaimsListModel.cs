using Core.Persistence.Paging;
using Kodlama.io.Devs2.Application.Features.OperationClaims.Dtos;

namespace Kodlama.io.Devs2.Application.Features.OperationClaims.Models;

public class OperationClaimsListModel : BasePageableModel
{
    // Sayfalama için kullanılan sınıf olarak diyebiliriz. Hem Sayfalama verilerini hemde Dto verilerini kullacağız
    public IList<OperationClaimDto> Items { get; set; }  // İsimlendirmeler aynı olması gerekirki mapperda bir daha configuraiton yapmamak için 
}
