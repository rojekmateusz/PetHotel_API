namespace PetHotel.Infrastructure.Configuration;

public class BlobStorageSettings
{
    public string ConnectionString { get; set; } = default!;
    public string ImagesContainerName { get; set; } = default!;
}
