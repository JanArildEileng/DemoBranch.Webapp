using DemoBranch.Webapp.Appliction.Model;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace DemoBranch.Webapp
{
    
    public class CreateEventExamples : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(CreatePerson))
            {
                schema.Example = new OpenApiObject
                {
                    ["name"] = new OpenApiString($"TestName- { new System.Random().Next()}")
                };
            }
        }
    }


    public class ChangeEventExamples : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type == typeof(ChangePerson))
            {
                schema.Example = new OpenApiObject
                {
                    ["name"] = new OpenApiString($"ChangedName- { new System.Random().Next()}")
                };
            }
        }
    }


}
