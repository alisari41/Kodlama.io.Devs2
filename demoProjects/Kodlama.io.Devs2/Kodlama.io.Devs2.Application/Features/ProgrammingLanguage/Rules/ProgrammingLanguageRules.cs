using Core.CrossCuttingConcerns.Exceptions;
using Kodlama.io.Devs2.Application.Services.Repositories;
using _ProgrammingLanguage = Kodlama.io.Devs2.Domain.Entities.ProgrammingLanguage;
using Microsoft.EntityFrameworkCore;

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
            var result = await _programmingLanguageRepository.Query().Where(x => x.Name == name).AnyAsync(); // Aynı isimde veri var mı
            if (result) throw new BusinessException("Progralama Dili kullanılmaktadır!"); // BusinessException için "Core.CrossCuttingConcerns" dan Referans almak gerekir
        }

        public void ProgrammingLanguageShouldExistWhenRequested(_ProgrammingLanguage? programmingLanguage)
        {
            // Eğer bir Programlama Dili  talep ediliyorsa o dilin olması gerekir.

            if (programmingLanguage == null) throw new BusinessException("Programlama dili mevcut değildir.");
        }

        public async Task ProgrammingLanguageShouldExistWhenRequested(int id)
        {
            // Eğer biz aradığımız Id ile tüm listenin verilerini istemiyorsa sadece kontrol sağlamak için bu metot kullanılır. Yok biz id vererek GetById (id numarasını vererek tüm listeyi almak gibi) gibi aramalar yapmak isteyip birde sorgulama yapmak istersek yukardaki metot kullanılır
            var result = await _programmingLanguageRepository.Query().Where(x => x.Id == id).AnyAsync();
            if (!result) throw new BusinessException("Programlama dili mevcut değildir.");
        }

        public async Task ProgrammingLanguageConNotBeDuplicatedWhenUpdated(int id, string name)
        {
            var result = await _programmingLanguageRepository.Query().Where(x => x.Name == name).AnyAsync(); // Aynı isimde veri var mı
            if (result)
            {
                result = await _programmingLanguageRepository.Query().Where(x => (x.Id == id && x.Name == name)).AnyAsync(); // Aynı isimdeki veri aynı id mi evetse devam etsin hayırsa aynı isimden vardır hatası versin

                if (!result)
                    throw new BusinessException("Progralama Dili kullanılmaktadır!");
            }
        }
    }
}

