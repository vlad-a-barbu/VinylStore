namespace VinylStore.DataObjects.Entities;

public class Album
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }

    public Guid ArtistId { get; set; }
}
