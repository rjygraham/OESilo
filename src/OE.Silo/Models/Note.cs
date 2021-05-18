using System.Text.Json.Serialization;

namespace OE.Silo.Models
{
	public class Note
	{
		[JsonPropertyName("message")]
		public string Message { get; set; }
	}
}
