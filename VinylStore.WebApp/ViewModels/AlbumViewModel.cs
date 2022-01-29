namespace VinylStore.Web.ViewModels;

public class AlbumViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime ReleaseDate { get; set; }
    public string Artist { get; set; } = null!;  
}
