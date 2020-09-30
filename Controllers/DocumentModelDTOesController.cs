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
    [Route("api/DocumentsDTO")]
    [ApiController]
    public class DocumentModelDTOesController : ControllerBase
    {
        private readonly DocumentContext _context;

        public DocumentModelDTOesController(DocumentContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets all DocumentsDTO.
        /// </summary>
        // GET: api/DocumentModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DocumentModelDTO>>> GetDocumentsDTO()
        {
            return await _context.Documents.Select(x => DocumentToDTO(x)).ToListAsync();
            //return await _context.Documents.ToListAsync();
        }

        /// <summary>
        /// Gets a specific DocumentDTO.
        /// </summary>
        /// <param name="id">Id of the Document you are trying to get</param>
        /// <response code="404">Document not found</response> 
        // GET: api/DocumentModels/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DocumentModelDTO>> GetDocumentModelDTO(int id)
        {
            var documentModel = await _context.Documents.FindAsync(id);

            if (documentModel == null)
            {
                return NotFound();
            }

            return DocumentToDTO(documentModel);
        }

        /// <summary>
        /// Updates a specific DocumentDTO.
        /// </summary>
        /// /// <remarks>
        /// Sample request:
        ///
        ///     PUT /Documents
        ///     {
        ///        "id": 1,
        ///        "docName": "Item1",
        ///     }
        ///
        /// </remarks>
        /// <param name="id">Id of the Document you are trying to update must match in request and exist in database</param>
        /// <param name="documentModelDTO"></param>
        /// <returns>updates a Document</returns>
        /// <response code="204">Returns no content but Document was updated</response>
        /// <response code="400">If the Document is null</response> 
        // PUT: api/DocumentModels/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PutDocumentModelDTO(int id, DocumentModelDTO documentModelDTO)
        {
            if (id != documentModelDTO.Id)
            {
                return BadRequest();
            }

            var document = await _context.Documents.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            document.DocName = documentModelDTO.DocName;

            //_context.Entry(documentModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentModelDTOExists(id))
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
        /// Creates a specific DocumentDTO.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Documents
        ///     {
        ///        "id": 1,
        ///        "docName": "Item1",
        ///     }
        ///
        /// </remarks>
        /// <param name="documentModelDTO"></param>
        /// <returns>A newly created Document</returns>
        /// <response code="201">Returns the newly created Document</response>
        /// <response code="400">If the item is null</response>            
        // POST: api/DocumentModels
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<DocumentModelDTO>> PostDocumentModelDTO(DocumentModelDTO documentModelDTO)
        {
            var document = new DocumentModel
            {
                DocName = documentModelDTO.DocName
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetDocumentModel", new { id = documentModel.Id }, documentModel);
            return CreatedAtAction(nameof(GetDocumentModelDTO), new { id = document.Id }, DocumentToDTO(document));
        }

        /// <summary>
        /// Deletes a specific DocumentDTO.
        /// </summary>
        /// <param name="id">Id of the document you wish to delete</param>
        /// <response code="404">Document not found</response>       
        // DELETE: api/DocumentModels/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<DocumentModelDTO>> DeleteDocumentModelDTO(int id)
        {
            var documentModelDTO = await _context.DocumentModelDTO.FindAsync(id);
            if (documentModelDTO == null)
            {
                return NotFound();
            }

            _context.DocumentModelDTO.Remove(documentModelDTO);
            await _context.SaveChangesAsync();

            return documentModelDTO;
        }

        private bool DocumentModelDTOExists(int id)
        {
            return _context.DocumentModelDTO.Any(e => e.Id == id);
        }

        private static DocumentModelDTO DocumentToDTO(DocumentModel todoItem) =>
        new DocumentModelDTO
        {
            Id = todoItem.Id,
            DocName = todoItem.DocName
        };
    }
}
