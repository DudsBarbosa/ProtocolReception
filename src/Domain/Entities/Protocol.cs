using Newtonsoft.Json;

namespace ProtocolReception.ApplicationCore.Entities
{
    public class Protocol
    {
        [JsonIgnore]
        public int Id { get; set; }
        [JsonProperty("number")]
        public int Number { get; set; }
        [JsonProperty("copy")]
        public int Copy { get; set; }
        [JsonProperty("cpf")]
        public required string Cpf { get; set; }
        [JsonProperty("rg")]
        public required string Rg { get; set; }
        [JsonProperty("name")]
        public required string Name { get; set; }
        [JsonProperty("motherName")]
        public required string MotherName { get; set; }
        [JsonProperty("fatherName")]
        public string? FatherName { get; set; }
        [JsonProperty("photoUrl")]
        public required string PhotoUrl { get; set; }

    }
}
