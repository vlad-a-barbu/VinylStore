namespace VinylStore.DataObjects.BusinessModels;

public class CompleteAlbum
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public string Artist { get; set; } = null!;  
}
