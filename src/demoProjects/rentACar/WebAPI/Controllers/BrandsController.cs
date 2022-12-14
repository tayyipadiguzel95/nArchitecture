using Application.Features.Brands.Commands.CreateBrand;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandsController : BaseController
{
     [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBrandCommand command)
    {
        var result = await Mediator.Send(command);
        return Created(string.Empty, result);
    }
}