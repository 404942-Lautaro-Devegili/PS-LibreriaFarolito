using LibreriaBack.Datos.Entidades.Autorización;
using LibreriaBack.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibreriaFrontendWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService us;

        public UsuarioController(IUsuarioService usuarioService)
        {
            this.us = usuarioService;
        }

        // POST: api/Usuario/Login
        [HttpPost("Login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] LoginRequest loginRequest)
        {
            try
            {
                var usuario = await Task.Run(() => us.Login(loginRequest.Usuario, loginRequest.Password));

                HttpContext.Session.SetString("UserId", usuario.Id.ToString());
                HttpContext.Session.SetString("UserName", usuario.User);
                HttpContext.Session.SetString("UserRole", usuario.IdRol.ToString());
                var userIdCheck = HttpContext.Session.GetString("UserId"); //Estas cosas eran de debug pero no las uso más, las dejo por si acaso
                var sessionIdLogin = HttpContext.Session.Id;

                return Ok(usuario);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // GET: api/Usuario/Logout
        [HttpGet("Logout")]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok(new { message = "Sesión cerrada exitosamente" });
        }

        // GET: api/Usuario/CheckSession
        [HttpGet("CheckSession")]
        public ActionResult<object> CheckSession()
        {
            var userId = HttpContext.Session.GetString("UserId");
            var sessionId = HttpContext.Session.Id;
            var isAvailable = HttpContext.Session.IsAvailable;

            return Ok(new
            {
                hasValidSession = !string.IsNullOrEmpty(userId),
                sessionDebug = new
                {
                    sessionId = sessionId,
                    userId = userId ?? "NULL",
                    sessionAvailable = isAvailable,
                    sessionKeysCount = HttpContext.Session.Keys.Count()
                }
            });
        }

        // GET: api/Usuario/TraerUsuarios
        [HttpGet("TraerUsuarios")]
        public async Task<ActionResult<List<Usuario>>> TraerUsuarios()
        {
            try
            {
                var userRole = HttpContext.Session.GetString("UserRole");
                if (userRole != "1")
                {
                    return Forbid("No tiene permisos para acceder a esta funcionalidad");
                }

                var usuarios = await Task.Run(() => us.TraerUsuarios());
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener usuarios", error = ex.Message });
            }
        }

        // GET: api/Usuario/TraerRoles
        [HttpGet("TraerRoles")]
        public async Task<ActionResult<List<Rol>>> TraerRoles()
        {
            try
            {
                var userRole = HttpContext.Session.GetString("UserRole");
                if (userRole != "1")
                {
                    return Forbid("No tiene permisos para acceder a esta funcionalidad");
                }

                var roles = await Task.Run(() => us.TraerRoles());
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener roles", error = ex.Message });
            }
        }

        // POST: api/Usuario/GuardarUsuario
        [HttpPost("GuardarUsuario")]
        public async Task<ActionResult<List<int>>> GuardarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var userRole = HttpContext.Session.GetString("UserRole");
                if (userRole != "1")
                {
                    return Forbid("No tiene permisos para acceder a esta funcionalidad");
                }

                var resultado = await Task.Run(() => us.GuardarUsuario(usuario));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al guardar usuario", error = ex.Message });
            }
        }

        // PUT: api/Usuario/ModificarUsuario
        [HttpPut("ModificarUsuario")]
        public async Task<ActionResult<int>> ModificarUsuario([FromBody] Usuario usuario)
        {
            try
            {
                var userRole = HttpContext.Session.GetString("UserRole");
                if (userRole != "1")
                {
                    return Forbid("No tiene permisos para acceder a esta funcionalidad");
                }

                var resultado = await Task.Run(() => us.ModificarUsuario(usuario));
                return Ok(resultado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al modificar usuario", error = ex.Message });
            }
        }

        // GET: api/Usuario/CheckAdminRole
        [HttpGet("CheckAdminRole")]
        public ActionResult<bool> CheckAdminRole()
        {
            var userRole = HttpContext.Session.GetString("UserRole");
            return Ok(userRole == "1");
        }
    }
    public class LoginRequest
    {
        public string Usuario { get; set; }
        public string Password { get; set; }
    }
}
