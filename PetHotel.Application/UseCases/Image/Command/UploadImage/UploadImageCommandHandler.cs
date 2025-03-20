using AutoMapper;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using PetHotel.Domain.Constants;
using PetHotel.Domain.Exceptions;
using PetHotel.Domain.Interfaces;
using PetHotel.Domain.Interfaces.AuthorizationServices;
using PetHotel.Domain.Repositories;

namespace PetHotel.Application.UseCases.Image.Command.UploadImage;

public class UploadImageCommandHandler(ILogger<UploadImageCommandHandler> logger,IMapper mapper, IHotelRepository hotelRepository,
    IHotelAuthorizationService hotelAuthorizationService, IBlobStorageService blobStorageService,
    IImageRepository imageRepository) : IRequestHandler<UploadImageCommand, int>
{
    public async Task<int> Handle(UploadImageCommand request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Uploading image to Hotel: {@HotelId}", request.HotelId);

        if (request.File == null || request.File.Length == 0)
            throw new ArgumentException("No file uploaded.");

        var hotel = await hotelRepository.GetHotelByIdAsync(request.HotelId)
            ?? throw new NotFoundException(nameof(Hotel), request.HotelId.ToString());

        if (!hotelAuthorizationService.Authorize(hotel, ResourceOperation.Create))
            throw new ForbidException();

        var ImageUrl = await blobStorageService.UploadToBlobAsync(request.File, request.FileName);
        request.Url = ImageUrl;

        var image = mapper.Map<Domain.Entities.Image>(request);
        int imageId = await imageRepository.CreateImage(image);
        return imageId;
    }

   
}
