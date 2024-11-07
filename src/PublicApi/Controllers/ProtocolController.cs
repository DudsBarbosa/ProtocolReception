using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.ApplicationCore.UseCases;

namespace ProtocolReception.PublicApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ProtocolController : ControllerBase
    {
        private readonly ProtocolService _protocolService;
        private readonly ProtocolLogService _protocolLogService;

        public ProtocolController(ProtocolService protocolService, ProtocolLogService protocolLogService)
        {
            _protocolService = protocolService;
            _protocolLogService = protocolLogService;
        }

        [HttpPost]
        public async Task<ActionResult<Protocol>> PostProtocol(Protocol protocol)
        {
            var existingProtocolByNumber = await _protocolService.GetByNumberAsync(protocol.Number);
            if (existingProtocolByNumber != null)
            {
                return BadRequest(new { Message = "Protocol with this number already exists" });
            }

            var existsByCpfAndCopy = await _protocolService.GetByCpfAndCopyAsync(protocol.Cpf, protocol.Copy);
            if (existsByCpfAndCopy != null)
            {
                return BadRequest(new { Message = "Protocol with this CPF and copy/via already exists" });
            }

            await _protocolService.AddAsync(protocol);
            return CreatedAtAction("GetProtocolById", new { id = protocol.Id }, protocol);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Protocol>> GetProtocolById(int id)
        {
            var protocol = await _protocolService.GetByNumberAsync(id);

            if (protocol == null)
            {
                var message = string.Format("Protocol with number = {0} not found", id);
                return NotFound(new { Message = message });
            }

            return protocol;
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<Protocol>> GetProtocolByCpf(string cpf)
        {
            var protocol = await _protocolService.GetByCpfAsync(cpf);

            if (protocol == null)
            {
                var message = string.Format("Protocol with CPF = {0} not found", cpf);
                return NotFound(new { Message = message });
            }

            return protocol;
        }

        [HttpGet("rg/{rg}")]
        public async Task<ActionResult<Protocol>> GetProtocolByRg(string rg)
        {
            var protocol = await _protocolService.GetByRgAsync(rg);

            if (protocol == null)
            {
                var message = string.Format("Protocol with RG = {0} not found", rg);
                return NotFound(new { Message = message });
            }

            return protocol;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Protocol>>> GetProtocols()
        {
            var protocols = await _protocolService.GetAllAsync();

            if (protocols == null || !protocols.Any())
            {
                var message = "No protocols found";
                return NotFound(new { Message = message });
            }

            return Ok(protocols.Where(p => p != null).Cast<Protocol>());
        }

        [HttpPost]
        public async Task<ActionResult<string>> PostConsumerMessage(string message)
        {
            return await _protocolLogService.ManageConsumerMessage(message);
        }
    }
}
