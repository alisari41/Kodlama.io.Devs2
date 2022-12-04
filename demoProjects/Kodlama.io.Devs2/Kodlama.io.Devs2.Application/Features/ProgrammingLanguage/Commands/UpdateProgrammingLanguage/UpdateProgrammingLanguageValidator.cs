using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs2.Application.Features.ProgrammingLanguage.Commands.UpdateProgrammingLanguage
{
    public class UpdateProgrammingLanguageValidator : AbstractValidator<UpdateProgrammingLanguageCommand>
    {
        // FluentValidation ile Format Doğrulama işlemleri
        // Ekeleme işlemleri için ayrı güncelleme işlemleri vs. ler için ayrı doğrulama işlemleri olabileceği için "UpdateProgrammingLanguageCommand" ile ekleme işlemleri için yapıldı
        public UpdateProgrammingLanguageValidator()
        {
            RuleFor(c => c.Name).NotEmpty(); // Boş geçilemez.
        }
    }
}
