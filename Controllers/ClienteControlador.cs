using Microsoft.AspNetCore.Mvc;
using pruebaApiC_.Modelos;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using pruebaApiC_.Context;
using Microsoft.EntityFrameworkCore;

namespace pruebaApiC_.Controllers
{

    [ApiController]
    [Route("cliente")]
    public class ClienteControlador:ControllerBase
    {
        private readonly ClienteContext _clienteContext;

        public ClienteControlador(ClienteContext clienteContext)
        {
            _clienteContext = clienteContext;
        }

        [HttpPost]
        [Route("crear")]
        public async Task <IActionResult> crearCliente(Cliente cliente)
        {
            var cl = await _clienteContext.Clientes.AddAsync(cliente);

            if (cl == null)
            {
                return this.BadRequest(new
                {
                    mensaje = "Error No se pudo crear el cliente",
                    estado = 404
                });
            }

            var resultado = await _clienteContext.SaveChangesAsync();

            return this.Ok(new
            {
                estado = 200,
                mensaje = "Cliente creado correctamente",
               
            });

        }


        [HttpGet]
        [Route("listar")]
        public async Task<ActionResult<IEnumerable<Cliente>>> listarCliente()
        {
            var cl = await _clienteContext.Clientes.ToListAsync();

            if (cl == null)
            {
                return this.BadRequest(new
                {
                    mensaje = "Error Mostrando la lista",
                    estado = 404
                });
            }


            return StatusCode(statusCode: 200, new {
                estado = 200,
                mensaje = "Lista de Clientes",
                ListaCliente = cl
            });
       
           
        }

        [HttpGet]
        [Route("buscar")]
        public async Task<IActionResult> Buscar(int id)
        {
            Cliente cliente = await _clienteContext.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return this.BadRequest(new
                {
                    mensaje = "Error No se encontro cliente",
                    estado = 404
                });
            }

            return this.Ok(new
            {
                estado = 200,
                mensaje= "Cliente encontrado",
                ListaCliente = cliente
            });
              
   
        }


        [HttpPut]
        [Route("editar")]
        public async Task<IActionResult> actualizarCliente(int id, Cliente cliente)
        {
            var clienteId = await _clienteContext.Clientes.FindAsync(id);

            if (clienteId == null)
            {
                return this.BadRequest(new
                {
                    mensaje= "Error No se encontro cliente",
                    estado = 404
                });
            }

            clienteId!.Nombre = cliente.Nombre;
            clienteId.Apellido = cliente.Apellido;
            clienteId.êdad = cliente.êdad;

            await _clienteContext.SaveChangesAsync();

            return this.Ok(new
            {
                estado = 200,
                mensaje = "Cliente Actualizado Correctamente",
                ClienteActualizado = clienteId
            });
        }

        [HttpDelete]
        [Route("eliminar")]
        public async Task<IActionResult> deleteCliente(int id)
        {
            var clienteId = await _clienteContext.Clientes.FindAsync(id);

            if (clienteId == null)
            {
                return this.BadRequest(new
                {
                    mensaje = "Error No se encontro cliente",
                    estado = 404
                });
            }

            _clienteContext.Clientes.Remove(clienteId!);


            await _clienteContext.SaveChangesAsync();

            return this.Ok(new
            {
                estado = 200,
                mensaje = "cliente eliminado",
                ClienteEliminado = clienteId
            });
        }

        [HttpGet]
        [Route("buscarNombre")]
        public async Task<IActionResult> BuscarNombre(string nombre)
        {
            var cliente =  from cl in _clienteContext.Clientes
                         select cl;

            if (!System.String.IsNullOrEmpty(nombre))
            {
                cliente =   cliente.Where(s => s.Nombre!.Contains(nombre));
            }

            return this.Ok(new
            {
                estado = 200,
                mensaje = "cliente encontrado",
                ClienteEncontrado = cliente
            });
           

        }

    }
}
