using System.Net;

namespace JobCandidateHub.Application.DTOs.Base
{
    public class ResponseDto<T>
    {
        public HttpStatusCode StatusCode { get; set; }

        public string StatusMessage { get; set; }

        public T ResponseData { get; set; }
    }
}
