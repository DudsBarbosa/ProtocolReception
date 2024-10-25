using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProtocolReception.ApplicationCore.Entities;
using ProtocolReception.ApplicationCore.Interfaces;
using ProtocolReception.ApplicationCore.Services;
using ProtocolReception.Infrastructure.Repositories;

namespace ProtocolReception.PublicApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProtocolController : ControllerBase
    {
        private readonly ProtocolService _protocolService;

        public ProtocolController(ProtocolService protocolService)
        {
            _protocolService = protocolService;
        }

        [HttpPost]
        public async Task<ActionResult<Protocol>> PostProtocol(Protocol protocol)
        {
            await _protocolService.AddAsync(protocol);
            return CreatedAtAction("GetProtocolById", new { id = protocol.Id }, protocol);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Protocol>> GetProtocolById(int id)
        {
            var protocol = await _protocolService.GetByNumberAsync(id);

            if (protocol == null)
            {
                return NotFound();
            }

            return protocol;
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<Protocol>> GetProtocolByCpf(string cpf)
        {
            var protocol = await _protocolService.GetByCpfAsync(cpf);

            if (protocol == null)
            {
                return NotFound();
            }

            return protocol;
        }

        [HttpGet("rg/{rg}")]
        public async Task<ActionResult<Protocol>> GetProtocolByRg(string rg)
        {
            var protocol = await _protocolService.GetByRgAsync(rg);

            if (protocol == null)
            {
                return NotFound();
            }

            return protocol;
        }
    }
}
