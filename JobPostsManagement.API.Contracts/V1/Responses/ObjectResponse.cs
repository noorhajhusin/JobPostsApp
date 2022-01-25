using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostsManagement.API.Contracts.V1.Responses
{
    public class ObjectResponse<T>
    {
        public ObjectResponse()
        {

        }
        public ObjectResponse(T data)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
}
