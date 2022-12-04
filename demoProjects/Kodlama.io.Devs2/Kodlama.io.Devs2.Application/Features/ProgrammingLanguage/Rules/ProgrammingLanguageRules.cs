using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Kodlama.io.Devs2.Application.Services.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Rules
{
    public class ProgrammingLanguageRules
    {
        private readonly IProgrammingLanguageRepository _programmingLanguageRepository;

        public ProgrammingLanguageRules(IProgrammingLanguageRepository programmingLanguageRepository)
        {
            _programmingLanguageRepository = programmingLanguageRepository;
        }

        public async Task ProgrammingLanguageConNotBeDuplicatedWhenInserted(string name)
        {
            // Metod geriye hiç bir şey dönmüyor. Hiçbir şey dönmüyorsa geçiyor demektir. Kuralldan geçmediği her durumda hata fırlatıyor demektir.
            // Tablo Adı ( Programlama Dilleri ) isimleri tekrar edemez
            IPaginate<Domain.Entities.ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(x => x.Name == name);// IPaginate sayfalama yapmak için yazıldı
            if (result.Items.Any()) throw new BusinessException("Progralama Dili kullanılmaktadır!"); // BusinessException için "Core.CrossCuttingConcerns" dan Referans almak gerekir
        }
        public void ProgrammingLanguageShouldExitsWhenRequested(Domain.Entities.ProgrammingLanguage programmingLanguage)
        {
            // Eğer bir Programlama Dili  talep ediliyorsa o dilin olması gerekir.
            if (programmingLanguage == null) throw new BusinessException("Programalama dili mevcut değildir."); // BusinessException için "Core.CrossCuttingConcerns" dan Referans almak gerekir
        }
    }
}
