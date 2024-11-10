

using GYM_MILESTONETHREE.Models;
using GYM_MILESTONETHREE.DataBase;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.Xml;
using static System.Net.Mime.MediaTypeNames;


namespace GYM_APPLICATION_BACK_END.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Image : ControllerBase
    {
        private readonly AppDb _context;

        public Image(AppDb context)
        {
            _context = context;
        }
        [HttpPost("add-image")]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("Image is not Proper");
            }
            using var ms =new MemoryStream();
            await file.CopyToAsync(ms);

            var image = new ImageModel
            {
                Name = file.FileName,
                Data = ms.ToArray(),
                ContentType = file.ContentType,
            };
            _context.Images.Add(image);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Image added successfully", imageId = image.Id });

        }

        [HttpGet("Display/{id}")]
        public async Task<IActionResult>DisplayImage(int id)
        {
            var image = await _context.Images.FindAsync(id);
            if (image == null)
            {
                return NotFound();
            }
            return File(image.Data, image.ContentType);
        }

        [HttpDelete("Delete images")]
        public async Task<IActionResult>DeleteImages(int id)
        {
            var image = await _context.Images.FirstOrDefaultAsync(o=>o.Id==id);
            if (image == null)
            {
                return NotFound();
            }
            _context.Images.Remove(image);
            await _context.SaveChangesAsync();
            return Ok(image.Name+ "Deleted Succesfully");
        }
        //[HttpGet("Ger all images")]
        //public async Task<IActionResult> GetAllImages()
        //{
        //    try
        //    {
        //        // Get all images from the database
        //        var images = await _context.Images.ToListAsync();

        //        // If no images found, return a 404
        //        if (images == null || images.Count == 0)
        //        {
        //            return NotFound(new { message = "No images found." });
        //        }

        //        // Create a list to hold image metadata with base64 encoding
        //        var imageList = images.Select(image => new
        //        {
        //            image.Id,
        //            image.Name,
        //            image.ContentType,
        //            ImageData = Convert.ToBase64String(image.Data) // Convert image binary data to base64 string
        //        }).ToList();

        //        // Return the list of images with base64 encoded data
        //        return Ok(imageList);
        //    }
        //    catch (Exception ex)
        //    {
        //        // Handle errors and return a 500 status
        //        return StatusCode(500, new { message = "An error occurred while fetching images", error = ex.Message });
        //    }

        //}
    }
}
