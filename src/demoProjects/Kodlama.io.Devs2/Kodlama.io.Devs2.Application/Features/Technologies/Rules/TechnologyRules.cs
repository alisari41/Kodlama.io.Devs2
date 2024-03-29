﻿using Core.CrossCuttingConcerns.Exceptions;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Kodlama.io.Devs2.Application.Features.Technologies.Rules;

public class TechnologyRules
{
    private readonly ITechnologyRepository _technologyRepository;

    public TechnologyRules(ITechnologyRepository technologyRepository)
    {
        _technologyRepository = technologyRepository;
    }

    public async Task TechnologyNameCanNotBeDuplicatedWhenIserted(string name)
    {
        var result = await _technologyRepository.Query().Where(x => x.Name == name).AnyAsync(); // Aynı isimde veri var mı
        if (result) throw new BusinessException("Programlama Dili Teknolojisi kullanılmaktadır.");
    }

    public async Task TechnologyShouldExistWhenRequested(int id)
    {
        var result = await _technologyRepository.Query().Where(x => x.Id == id).AnyAsync();
        if (!result) throw new BusinessException("Programlama dili Teknolojisi mevcut değildir.");
    }

    public async Task TechnologyNameConNotBeDuplicatedWhenUpdated(int id, string name)
    {
        var result = await _technologyRepository.Query().Where(x => x.Name == name).AnyAsync(); // Aynı isimde veri var mı
        if (result)
        {
            result = await _technologyRepository.Query().Where(x => (x.Id == id && x.Name == name)).AnyAsync(); // Aynı isimdeki veri aynı id mi evetse devam etsin hayırsa aynı isimden vardır hatası versin

            if (!result)
                throw new BusinessException("Progralama Dili Teknolojisi kullanılmaktadır!");
        }
    }
}
