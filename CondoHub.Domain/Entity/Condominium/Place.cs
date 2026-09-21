using CondoHub.Domain.Dto.Condominium;
using CondoHub.Domain.Entity.Condominium.Places;

namespace CondoHub.Domain.Entity.Condominium;

public class Place: Util.Entity
{
    public long Id { get; set; }
    public long CondominiumId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string FolderRI { get; set; }
    public int Capacity { get; set; }
    public string Status { get; set; }
    public PlaceSchedule Horario { get; set; }
    public List<string> Amenities { get; set; }
    public List<PlaceObject> Objects { get; set; }
    public decimal Price { get; set; }
    public List<string> Regulations { get; set; }
    public PlaceContact Contact { get; set; }
    public List<string> Images { get; set; }
    
    public Place(){}

    public Place(CreatePlaceDto dto)
    {
        Id = dto.Id;
        Name = dto.Name;
        Description = dto.Description;
        FolderRI = dto.FolderRI;
        Capacity = dto.Capacity;
        Status = dto.Status;
        Horario = dto.Horario;
        Amenities = dto.Amenities;
        Objects = dto.Objects;
        Price = dto.Price;
        Regulations = dto.Regulations;
        Contact = dto.Contact;
        Images = dto.Images;
    }
}





