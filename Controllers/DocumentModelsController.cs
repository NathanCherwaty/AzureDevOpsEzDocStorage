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
    [Route("api/DocumentModels")]
    [ApiController]
    public class DocumentModelsController : ControllerBase
    {
        private readonly DocumentContext _context;

        public DocumentModelsController(DocumentContext context)
        {
            _context = context;
        }

        // GET: api/DocumentModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentModel>>> GetDocuments()
        {
            return await _context.Documents.ToListAsync();
        }

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
