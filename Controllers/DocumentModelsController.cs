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
        /// <param name="id">Id of the Document you are trying to get</param>
        /// <response code="404">Document not found</response> 
        // GET: api/DocumentModels/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        /// /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Documents
        ///     {
        ///        "id": 1,
        ///        "docName": "Item1",
        ///        "btyes": 64,
        ///        "creationDate": "2000-01-30T14:00:00.000Z",
        ///        "extention": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Id of the Document you are trying to update must match in request and exist in database</param>
        /// <param name="documentModel"></param>
        /// <returns>updates a Document</returns>
        /// <response code="204">Returns no content but Document was updated</response>
        /// <response code="400">If the Document is null</response> 
        // PUT: api/DocumentModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Documents
        ///     {
        ///        "id": 1,
        ///        "docName": "Item1",
        ///        "btyes": 64,
        ///        "creationDate": "2000-01-30T14:00:00.000Z",
        ///        "extention": "string"
        ///     }
        ///
        /// </remarks>
        /// <param name="documentModel"></param>
        /// <returns>A newly created Document</returns>
        /// <response code="201">Returns the newly created Document</response>
        /// <response code="400">If the item is null</response>            
        // POST: api/DocumentModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <param name="id">Id of the document you wish to delete</param>
        /// <response code="404">Document not found</response>       
        // DELETE: api/DocumentModels/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
