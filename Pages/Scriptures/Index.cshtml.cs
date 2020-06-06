using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;
using ScriptureJournal.Data;

namespace ScriptureJournal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly ScriptureJournal.Data.ScriptureJournalContext _context;

        public IndexModel(ScriptureJournal.Data.ScriptureJournalContext context)
        {
            _context = context;
        }

        public string BookSort { get; set; }
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public IList<Scripture> Scripture { get;set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string sortOrder { get; set; }

        public async Task OnGetAsync(string sortOrder) {
            var scriptures = from s in _context.Scripture select s;

            if (!string.IsNullOrEmpty(SearchString)) {
                scriptures = scriptures.Where(s => s.Book.Contains(SearchString) || s.Note.Contains(SearchString));
            }


            //BookSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            //DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            if (!string.IsNullOrEmpty(sortOrder)) {
                switch (sortOrder) {

                    case "name":
                    scriptures = scriptures.OrderByDescending(s => s.Book);
                    break;

                    case "date":
                    scriptures = scriptures.OrderByDescending(s => s.DateAdded);
                    break;

                    //case "date_desc":
                    //scriptures = scriptures.OrderByDescending(s => s.DateAdded);
                    //break;

                    default:
                    scriptures = scriptures.OrderBy(s => s.DateAdded);
                    break;
                }
            }


            Scripture = await scriptures.ToListAsync();
        }

        /*public async Task OnGetAsync(string sortOrder) {
            BookSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";

            IQueryable<Scripture> scriptures = from s in _context.Scripture
                                             select s;

            switch (sortOrder) {
                case "name_desc":
                scriptures = scriptures.OrderByDescending(s => s.Book);
                break;
                case "Date":
                scriptures = scriptures.OrderBy(s => s.DateAdded);
                break;
                case "date_desc":
                scriptures = scriptures.OrderByDescending(s => s.DateAdded);
                break;
                default:
                scriptures = scriptures.OrderBy(s => s.DateAdded);
                break;
            }

            Scripture = await scriptures.AsNoTracking().ToListAsync();
        }*/
    }
}
