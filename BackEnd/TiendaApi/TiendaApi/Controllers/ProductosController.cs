using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using TiendaApi.Dtos;
using TiendaApi.Errors;

namespace TiendaApi.Controllers
{
    public class ProductosController : BaseApiController
    {
        private readonly IGenericRepository<Producto> _productoRepo;
        private readonly IGenericRepository<Marca> _marcaRepo;
        private readonly IGenericRepository<Tipo> _tipoRepo;
        private readonly IMapper _mapper;

        public ProductosController(IGenericRepository<Producto> productoRepo, IGenericRepository<Marca> marcaRepo,
            IGenericRepository<Tipo> tipoRepo,IMapper mapper)
        {
            _productoRepo = productoRepo;
            _marcaRepo = marcaRepo;
            _tipoRepo = tipoRepo;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductoDto>>> GetProductos()
        {
            var spec = new ProductosConMarcaYTipoSpecification();
            var productos = await _productoRepo.ListAsync(spec);
            return Ok(_mapper.Map
                         <IReadOnlyList<Producto>, IReadOnlyList<ProductoDto>>(productos));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var spec = new ProductosConMarcaYTipoSpecification(id);
            var producto = await _productoRepo.getEntityWithSpec(spec);
            if (producto == null) return NotFound(new ApiResponse(404));
            return _mapper.Map<ProductoDto>(producto);
        }


        [HttpGet("marcas")]
        public async Task<ActionResult<List<Marca>>> GetMarcas()
        {
            return Ok(await _marcaRepo.ListAllAsync());
        }

        [HttpGet("tipos")]
        public async Task<ActionResult<List<Tipo>>> GetTipos()
        {
            return Ok(await _tipoRepo.ListAllAsync());
        }

    }
}
