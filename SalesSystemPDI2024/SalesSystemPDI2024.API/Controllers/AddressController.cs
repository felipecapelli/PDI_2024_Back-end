using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SalesSystemPDI2024.Data.DAL;
using SalesSystemPDI2024.Model;
using System.Net;

namespace SalesSystemPDI2024.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly ILogger<AddressController> _logger;

        public AddressController(ILogger<AddressController> logger)
        {
            _logger = logger;
        }

        //[HttpGet(Name = "GetAddress")]
        //public IEnumerable<Address> Get()
        //{
        //    AddressDAL addressDAL = new AddressDAL();
        //    return addressDAL.Listar();
        //}

        [HttpGet]
        public IActionResult GetAdresses()
        {
            AddressDAL addressDAL = new AddressDAL();
            return Ok(addressDAL.Listar());
        }

        [HttpGet("{enderecoParaBuscar}")]
        public IActionResult GetAdressesByAddressNickName(string enderecoParaBuscar)
        {
            AddressDAL addressDAL = new AddressDAL();
            return Ok(addressDAL.Buscar(enderecoParaBuscar));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult AdicionaFilme([FromBody] Address address)
        {
            AddressDAL addressDAL = new AddressDAL();
            addressDAL.Adicionar(address);
            return CreatedAtAction(nameof(GetAdressesByAddressNickName),
                new { enderecoParaBuscar = address.AdressNickname },
                address);
        }

        [HttpPut("{enderecoParaBuscar}")]
        public IActionResult AtualizaFilme(string enderecoParaBuscar, [FromBody] Address address)
        {
            AddressDAL addressDAL = new AddressDAL();
            Address addressFromDB = addressDAL.Buscar(enderecoParaBuscar);
            if (addressFromDB == null) return NotFound();

            addressDAL.Atualizar(address);
            return NoContent();
        }

        [HttpDelete("{enderecoParaBuscar}")]
        public IActionResult DeletaFilme(string enderecoParaBuscar)
        {
            AddressDAL addressDAL = new AddressDAL();
            Address addressFromDB = addressDAL.Buscar(enderecoParaBuscar);
            if (addressFromDB == null) return NotFound();

            addressDAL.Excluir(addressFromDB);
            return NoContent();
        }
    }
}
