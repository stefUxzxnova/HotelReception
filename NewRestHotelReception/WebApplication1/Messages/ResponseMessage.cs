using Newtonsoft.Json;

namespace WebApplication1.Messages
{
	public class ResponseMessage
	{
		[JsonProperty(PropertyName = "code")]
		public object Code { get; set; }

		[JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
		public object Body { get; set; }


		//NullValueHandling = NullValueHandling.Ignore -> if body/error == null, it won't be included in the serializaed JSON 
		[JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
		public object Error { get; set; }
	}
}
