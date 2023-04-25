using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TiendaContextSeed
    {

        public static async Task SeedAsync(TiendaContext context)
        {
            //var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (!context.Marcas.Any())
            {
                var marcasData = File.ReadAllText("../Infrastructure/Data/SeedData/Marcas.json");
                var marcas = JsonSerializer.Deserialize<List<Marca>>(marcasData);

                foreach (var marca in marcas)
                {
                    context.Marcas.Add(marca);
                }
                await context.SaveChangesAsync();   
            }

            if (!context.Tipos.Any())
            {
                var titposData = File.ReadAllText("../Infrastructure/Data/SeedData/Tipos.json");
                var tipos = JsonSerializer.Deserialize<List<Tipo>>(titposData);
                foreach (var tipo in tipos)
                {
                    context.Tipos.Add(tipo);
                }
                await context.SaveChangesAsync();
            }

           if (!context.Productos.Any())
            {
                var productosData = File.ReadAllText("../Infrastructure/Data/SeedData/Productos.json");
                var productos = JsonSerializer.Deserialize<List<Producto>>(productosData);
                foreach (var producto in productos)
                {
                    context.Productos.Add(producto);
                }
                await context.SaveChangesAsync();
            }
            
            
        }
    }
}
