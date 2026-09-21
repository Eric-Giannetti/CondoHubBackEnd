using CondoHub.Domain.Entity.Condominium.Places;

namespace CondoHub.Domain.Dto.Condominium;

public class CreatePlaceDto
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
}