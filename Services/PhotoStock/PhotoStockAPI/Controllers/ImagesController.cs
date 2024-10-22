using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace PhotoStock.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ImagesController : ControllerBase
{
    private readonly string _uploadFolderPath;

    public ImagesController()
    {
        _uploadFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "upload");
    }

    /// <summary>
    /// Uploads an image.
    /// </summary>
    /// <param name="image"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>The URLs of the uploaded image</returns>
    /// <response code="200">Returns the URLs of the uploaded image</response>
    /// <response code="400">If the image is null or the file extension is invalid</response>
    [HttpPost("upload")]
    public async Task<IActionResult> Upload(IFormFile image, CancellationToken cancellationToken)
    {
        Guard.Against.Null(image, nameof(image));

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(image.FileName);

        if (string.IsNullOrEmpty(extension) || !allowedExtensions.Contains(extension.ToLower()))
        {
            return BadRequest("Invalid file extension. Allowed extensions are: .jpg, .jpeg, .png, .gif.");
        }

        var fileName = $"{Guid.NewGuid()}_{Path.GetFileName(image.FileName)}";
        var path = Path.Combine(_uploadFolderPath, fileName);

        try
        {
            Directory.CreateDirectory(_uploadFolderPath);  // Ensure the directory exists
            await using (var stream = new FileStream(path, FileMode.Create))
            {
                await image.CopyToAsync(stream, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"An error occurred while uploading the image: {ex.Message}");
        }

        string imageUrl = Url.Content($"~/upload/{fileName}");
        return Ok(new { Url = imageUrl });
    }

    /// <summary>
    /// Deletes an uploaded image.
    /// </summary>
    /// <param name="imageUrl"></param>
    /// <returns>Status of the deletion</returns>
    /// <response code="200">If the image is successfully deleted</response>
    /// <response code="404">If the image is not found</response>
    [HttpDelete("delete")]
    public IActionResult Delete(string imageUrl)
    {
        Guard.Against.NullOrEmpty(imageUrl, nameof(imageUrl));

        var fileName = Path.GetFileName(imageUrl);
        var path = Path.Combine(_uploadFolderPath, fileName);

        if (System.IO.File.Exists(path))
        {
            try
            {
                System.IO.File.Delete(path);
                return Ok(new { message = "Image deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while deleting the image: {ex.Message}");
            }
        }

        return NotFound(new { message = "Image not found" });
    }
}