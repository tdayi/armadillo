using System.Net;

namespace Armadillo.Core.Exception
{
    public class BusinessException : System.Exception
    {
        public readonly HttpStatusCode httpStatusCode;

        public BusinessException()
        {
        }

        public BusinessException(string rc) : base(rc)
        {

        }

        public BusinessException(string rc, HttpStatusCode httpStatusCode = HttpStatusCode.OK) : base(rc)
        {
            this.httpStatusCode = httpStatusCode;
        }
    }
}
