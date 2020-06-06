using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Models;

namespace ScriptureJournal.Data
{
    public class ScriptureJournalContext : DbContext
    {
        public ScriptureJournalContext (DbContextOptions<ScriptureJournalContext> options)
            : base(options)
        {
        }

        public DbSet<RazorPagesMovie.Models.Scripture> Scripture { get; set; }
    }
}
