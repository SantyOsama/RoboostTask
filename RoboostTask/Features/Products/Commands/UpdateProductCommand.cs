using MediatR;
using RoboostTask.DTOs.Products;
using RoboostTask.GeneralResponse;
using RoboostTask.Models;
using System.ComponentModel.DataAnnotations;

namespace RoboostTask.Features.Products.Commands
{
    public record UpdateProductCommand(UpdateProductRequest ProductRequest) : IRequest<Response<string>>;
}
