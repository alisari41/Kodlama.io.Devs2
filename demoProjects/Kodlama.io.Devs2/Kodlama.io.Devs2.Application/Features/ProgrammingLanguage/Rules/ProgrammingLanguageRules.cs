﻿using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Dtos;
using Kodlama.io.Devs2.Application.Services.Repositories;
using _ProgrammingLanguage = Kodlama.io.Devs2.Domain.Entities.ProgrammingLanguage;
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
            IPaginate<_ProgrammingLanguage> result = await _programmingLanguageRepository.GetListAsync(x => x.Name == name);// IPaginate sayfalama yapmak için yazıldı
            if (result.Items.Any()) throw new BusinessException("Progralama Dili kullanılmaktadır!"); // BusinessException için "Core.CrossCuttingConcerns" dan Referans almak gerekir
        }
        public void ProgrammingLanguageShouldExitsWhenRequested(_ProgrammingLanguage programmingLanguage)
        {
            // Eğer bir Programlama Dili  talep ediliyorsa o dilin olması gerekir.
            if (programmingLanguage == null) throw new BusinessException("Programalama dili mevcut değildir."); // BusinessException için "Core.CrossCuttingConcerns" dan Referans almak gerekir
        }
        public async Task<_ProgrammingLanguage> ProgrammingLanguageShouldExistWhenRequested(int id)
        {
            // Eğer bir Programlama Dili  talep ediliyorsa o dilin olması gerekir.
            var programmingLanguage = await _programmingLanguageRepository.GetAsync(p => p.Id == id);

            if (programmingLanguage == null)
                throw new BusinessException("Programlama dili mevcut değildir.");
            else
            {
                return programmingLanguage;
            }
        }

        public async Task<_ProgrammingLanguage> ProgrammingLanguageConNotBeDuplicatedWhenUpdated(int id, string name)
        {

            var programmingLanguage = await _programmingLanguageRepository.GetAsync(x => x.Name == name);
            int say = 0;
            if (programmingLanguage != null)
            {
                programmingLanguage = await _programmingLanguageRepository.GetAsync(x => (x.Id == id && x.Name == name));

                if (programmingLanguage == null)
                {
                    say = 1;
                    throw new BusinessException("Progralama Dili kullanılmaktadır!");
                }
            }
            if (say == 1)
            {
                throw new BusinessException("Progralama Dili kullanılmaktadır!");
            }
            else
            {
                return programmingLanguage;
            }
        }
    }
}

