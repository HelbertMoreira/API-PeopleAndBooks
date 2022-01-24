using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PeopleAndBooks.DataConverter.Converter.VO
{
    public class PersonVO
    {
        /*
            SEREALIZATION

            Essa implementação consiste em simplismente modificar como as popriedades do objeto irão ser repasssada ao nosso client.

            Para que isso funcione, deve-se apenas colocar a anotation [JsonPropertyName("Nome_Desejado")] em cada propriedade da classe,
                sendo que para ignorar um atributo coloca-se [JsonIgnore].
         */

        [JsonPropertyName("Código")]
        public int Id { get; set; }

        [JsonPropertyName("Primeiro nome")]
        public string First_Name { get; set; }

        [JsonPropertyName("Sobrenome")]
        public string Last_Name { get; set; }

        [JsonPropertyName("Endereço")]
        public string Address { get; set; }

        [JsonIgnore]
        public string Gender { get; set; }

        [JsonPropertyName("Ativo")]
        public bool Ativo { get; set; }
    }
}
