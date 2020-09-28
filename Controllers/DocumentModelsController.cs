using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EZDocStorage.Models;

namespace EZDocStorage.Controllers
{
    [Route("api/Documents")]
    [ApiController]
    public class DocumentModelsController : ControllerBase
    {
        private readonly DocumentContext _context;

        public DocumentModelsController(DocumentContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets all Documents.
        /// </summary>
        // GET: api/DocumentModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentModel>>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

        /// <summary>
        /// Gets a specific Document.
        /// </summary>
        // GET: api/DocumentModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentModel>> GetDocumentModel(int id)
        {
            var documentModel = await _context.Documents.FindAsync(id);

            if (documentModel == null)
            {
                return NotFound();
            }

            return documentModel;
        }

        /// <summary>
        /// Updates a specific Document.
        /// </summary>
        // PUT: api/DocumentModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocumentModel(int id, DocumentModel documentModel)
        {
            if (id != documentModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(documentModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Creates a specific Document.
        /// </summary>
        // POST: api/DocumentModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<DocumentModel>> PostDocumentModel(DocumentModel documentModel)
        {
            _context.Documents.Add(documentModel);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetDocumentModel", new { id = documentModel.Id }, documentModel);
            return CreatedAtAction(nameof(GetDocumentModel), new { id = documentModel.Id }, documentModel);
        }

        /// <summary>
        /// Deletes a specific Document.
        /// </summary>
        // DELETE: api/DocumentModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DocumentModel>> DeleteDocumentModel(int id)
        {
            var documentModel = await _context.Documents.FindAsync(id);
            if (documentModel == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(documentModel);
            await _context.SaveChangesAsync();

            return documentModel;
        }

        private bool DocumentModelExists(int id)
        {
            return _context.Documents.Any(e => e.Id == id);
        }
    }
}
