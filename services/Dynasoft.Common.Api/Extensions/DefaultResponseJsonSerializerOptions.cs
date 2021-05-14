
using Microsoft.Extensions.DependencyInjection;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Dynasoft.Common.Api.Extensions
{
    public static class DefaultResponseJsonSerializerOptions
    {
        public static JsonSerializerOptions Options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        static DefaultResponseJsonSerializerOptions() => Options.Converters.Add(new JsonStringEnumConverter());


        public static IMvcBuilder AddDefaultJsonSerializerOptions(this IMvcBuilder builder)
        {
            return builder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = Options.PropertyNamingPolicy;
                foreach (JsonConverter converter in Options.Converters)
                    options.JsonSerializerOptions.Converters.Add(converter);
            });
        }
    }
}
