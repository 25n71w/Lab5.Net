using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Storage.Blobs;
using Lab5.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lab5.NET.Pages.AnswerImages
{
    public class DeleteModel : PageModel
    {
        private readonly Lab5.NET.Data.AnswerImageDataContext _context;
       // private readonly BlobServiceClient _blobServiceClient;
       // private readonly string containerName = "smiliesrazorpages";
        private readonly BlobServiceClient _blobServiceClient;
        private readonly string earthContainerName = "earthimages";
        private readonly string computerContainerName = "computerimages";


        public DeleteModel(Lab5.NET.Data.AnswerImageDataContext context, BlobServiceClient blobServiceClient)
        {
            _context = context;
            _blobServiceClient = blobServiceClient;
        }

        [BindProperty]
        public AnswerImage AnswerImage { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnswerImage = await _context.Answers.FirstOrDefaultAsync(m => m.AnswerImageId == id);

            if (AnswerImage == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AnswerImage = await _context.Answers.FindAsync(id);

            if (AnswerImage != null)
            {
                BlobContainerClient containerClient;
                try
                {
                    containerClient = _blobServiceClient.GetBlobContainerClient(earthContainerName);
                    containerClient = _blobServiceClient.GetBlobContainerClient(computerContainerName);
                }
                catch (RequestFailedException)
                {
                    return RedirectToPage("Error");
                }

                try
                {
                    // Get the blob that holds the data
                    var blockBlob = containerClient.GetBlobClient(AnswerImage.FileName);
                    if (await blockBlob.ExistsAsync())
                    {
                        await blockBlob.DeleteAsync();
                    }

                    _context.Answers.Remove(AnswerImage);
                    await _context.SaveChangesAsync();

                }
                catch (RequestFailedException)
                {
                    return RedirectToPage("Error");
                }
            }

            return RedirectToPage("./Index");
        }
    }
}
