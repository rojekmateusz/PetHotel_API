using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Image.Command.UploadImage;

public class UploadImageCommandHandler(ILogger<UploadImageCommandHandler> logger,IMapper mapper, IConfiguration configuration, IHotelRepository hotelRepository,
    IImageRepository imageRepository) : IRequestHandler<UploadImageCommand, int>
{
    public async Task<int> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Uploading image to Hotel: {@HotelId}", request.HotelId);

        if (request.File == null || request.File.Length == 0)
            throw new ArgumentException("No file uploaded.");

        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        request.Url = await UploadToAzureBlobStorage(request.File);

        var image = mapper.Map<Domain.Entities.Image>(request);
        var id = await imageRepository.CreateImage(image);
        return id;
        
    }

    private async Task<string> UploadToAzureBlobStorage(IFormFile file)
    {
        var connectionString = configuration["AzureStorage:ConnectionString"];
        if (string.IsNullOrEmpty(connectionString))
             throw new ArgumentNullException(nameof(connectionString), "Azure Storage connection string is missing or empty.");
        
        var containerName = configuration["AzureStorage:ContainerName"];

        var blobServiceClient = new BlobServiceClient(connectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(containerName);
        await containerClient.CreateIfNotExistsAsync(PublicAccessType.Blob);

        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
        var blobClient = containerClient.GetBlobClient(fileName);

        using (var stream = file.OpenReadStream())
        {
            await blobClient.UploadAsync(stream, overwrite: true);
        }

        return blobClient.Uri.ToString();
    }
}
