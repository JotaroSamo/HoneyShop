using HoneyShop.Application.Files.DeleteFile;
using HoneyShop.Application.Files.DownloadFile;
using HoneyShop.Application.Files.UploadFile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HoneyShop.Controllers;

[ApiController]

[Route("api/[controller]")]
[Authorize(Policy = "AMU")]
public class FileController: ControllerBase
{
    private readonly IMediator _mediator;

    public FileController(IMediator mediator)
    {
        _mediator = mediator;
    }
    [HttpPost]
    [Route("upload")]
    [Authorize(Policy = "AMU")]
    public async Task<ActionResult<List<int>>> Post(IEnumerable<IFormFile> files)
    {
        var ids = await _mediator.Send(new UploadFileCommand(files.ToList()));
        return Ok(ids);
    }
    
    [HttpGet]
    [Route("get/{id}")]
    [ResponseCache(NoStore = true)]
    [Authorize(Policy = "AMU")]
    public async Task<IActionResult> Get(int id)
    {
        var file = await _mediator.Send(new DownloadFileQuery(id));

        if (file == null)
        {
            return NotFound();
        }

        return File(file.Content, file.ContentType, file.Name);
    }

    [HttpDelete]
    [Route("delete/{id}")]
    [Authorize(Policy = "AMU")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await _mediator.Send(new DeleteFileCommand(id));
        return Ok(result);
    }
}