using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Kodlama.io.Devs2.Application.Features.Technolojies.Models;
using Kodlama.io.Devs2.Application.Services.Repositories;
using Kodlama.io.Devs2.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Application.Features.Technolojies.Queries.GetListTechnolojy
{
    public class GetListTechnologyQuery : IRequest<TechnologyListModel>
    {
        // Mediator da IRequest
        public PageRequest PageRequest { get; set; } // Bir listeleme yapılacağı için bir Request üzerinden geçekleştirilecek

        public class GetListTechnologyQueryHandler : IRequestHandler<GetListTechnologyQuery, TechnologyListModel>
        {
            // IRequestHandler<GetListTechnologyQuery, TechnologyListModel> bu satır amacı GetListTechnologyQuery bunu gönderildiğinde hangi Handler çalışıcak 

            public readonly ITechnologyRepository _technolojyRepository;
            public readonly IMapper _mapper;

            public GetListTechnologyQueryHandler(ITechnologyRepository technolojyRepository, IMapper mapper)
            {
                _technolojyRepository = technolojyRepository;
                _mapper = mapper;
            }

            public async Task<TechnologyListModel> Handle(GetListTechnologyQuery request, CancellationToken cancellationToken)
            {
                IPaginate<Technology> technologies = await _technolojyRepository.GetListAsync(include:
                                                            x => x.Include(c => c.ProgrammingLanguage),  // Include işlemi ilişkilendirme için
                                                            index: request.PageRequest.Page,
                                                            size: request.PageRequest.PageSize); // Birden fazla ilişkide yapılabilir. Github Projesinden bakılabilir. Linkedinde paylaşıldı.

                var mappedTechnologyListModel = _mapper.Map<TechnologyListModel>(technologies); // Gelen datayı ModelListModel 'a çeviriyorum

                return mappedTechnologyListModel;
            }
        }
    }
}
