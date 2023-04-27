namespace TiendaApi.Errors
{
    public class ApiResponse
    {

        public int StatusCode { get; set; }
        public string Message { get; set; }

        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaulMessageForStatusCode(statusCode);
        }

        private string GetDefaulMessageForStatusCode(int statuscode)
        {
            return statuscode switch
            {
                400 => "Solicitud incorrecta",
                401 => "No estas autorizado",
                404 => "No se encontro el recurso",
                500 => "Error interno en el servidor",
                _ => null
            };
        }
    }
}
