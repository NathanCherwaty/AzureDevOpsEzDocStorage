using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using EZDocStorage.Models;

namespace EZDocStorage.Models
{
    public class DocumentContext : DbContext
    {
        public DocumentContext(DbContextOptions<DocumentContext> options) : base(options)
        { 
        }

        public DbSet<DocumentModel> Documents { get; set; }

        public DbSet<EZDocStorage.Models.DocumentModelDTO> DocumentModelDTO { get; set; }
    }
}
