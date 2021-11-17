using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab5.NET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Lab5.NET.Pages.AnswerImages
{
    public class IndexModel : PageModel
    {
        private readonly Lab5.NET.Data.AnswerImageDataContext _context;

        public IndexModel(Lab5.NET.Data.AnswerImageDataContext context)
        {
            _context = context;
        }

        //public IList<AnswerImage> AnswerImages { get; set; }
        public IList<AnswerImage> AnswerImages { get; set; }

        public async Task OnGetAsync()
        {
            AnswerImages = await _context.Answers.ToListAsync();
       
        }
    }
}