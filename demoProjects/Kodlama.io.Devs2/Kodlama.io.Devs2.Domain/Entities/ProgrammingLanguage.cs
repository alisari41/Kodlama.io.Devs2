using Core.Persistence.Repositories;

namespace Kodlama.io.Devs2.Domain.Entities;

public class ProgrammingLanguage : Entity
{
    public string Name { get; set; }
    public virtual ICollection<Technology> Technologies { get; set; } // Bir programlama dilinin birden fazla teknolojisi olabileceği için bu şekilde yazıldı

    public ProgrammingLanguage()
    {

    }

    public ProgrammingLanguage(int id, string name) : this()
    {
        Id = id;
        Name = name;
    }

}

