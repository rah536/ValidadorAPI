using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ValidadorAPI.Security
{
    [AttributeUsage(validOn: AttributeTargets.Class | AttributeTargets.Method)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string APIKEYNAME = "x-api-key";

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // 1. Verificamos si el cliente mandó el header "x-api-key"
            if (!context.HttpContext.Request.Headers.TryGetValue(APIKEYNAME, out var extractedApiKey))
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Acceso denegado: API Key no proporcionada."
                };
                return;
            }

            // 2. Buscamos la API Key real en el appsettings.json
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>("ApiKey");

            // 3. Comparamos las dos llaves
            if (apiKey == null || apiKey != extractedApiKey)
            {
                context.Result = new ContentResult()
                {
                    StatusCode = 401,
                    Content = "Acceso denegado: API Key incorrecta."
                };
                return;
            }

            // Si todo está bien, dejamos pasar la petición al Controlador
            await next();
        }
    }
}