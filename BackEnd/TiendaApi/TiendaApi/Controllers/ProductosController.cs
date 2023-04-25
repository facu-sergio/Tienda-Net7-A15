using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using TiendaApi.Dtos;

namespace TiendaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
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
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var spec = new ProductosConMarcaYTipoSpecification(id);
            var producto = await _productoRepo.getEntityWithSpec(spec);

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
