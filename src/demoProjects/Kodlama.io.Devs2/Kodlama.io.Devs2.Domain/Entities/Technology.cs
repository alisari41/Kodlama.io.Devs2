using Core.Persistence.Repositories;

namespace Kodlama.io.Devs2.Domain.Entities;

public class Technology : Entity
{
    // Programlama Dillerinin Teknolojileri
    public int ProgrammingLanguageId { get; set; }
    public string Name { get; set; }
    public virtual ProgrammingLanguage? ProgrammingLanguage { get; set; } // Bir teknolojinin Bir programlama dili olur 
                                                                          // Bir çok ORM için kullanılabilinmesi için "virtual" olarak süsledik


    // Programlama dili bir tane olduğu için ProgrammingLanguage şeklinde kullanıldı ilerde mesela progralama dilleri olsa List<...> şeklinde yazılır.

    public Technology()
    {
    }

    public Technology(int id, int programmingLanguageId, string name) :this()
    {
        Id = id; // Biz elle ekliyoruz ctor ile otomatik gelmiyor
        ProgrammingLanguageId = programmingLanguageId;
        Name = name;
    }
}
