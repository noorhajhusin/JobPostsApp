using System;
using System.Collections.Generic;
using System.Text;

namespace JobPostsManagement.API.Contracts.V1.Responses
{
    public class ListResponse<T>
    {
        public ListResponse()
        {

        }
        public ListResponse(IEnumerable<T> data)
        {
            Data = data;
        }
        public ListResponse(T data)
        {
            Data = new List<T> { data };
        }
        public IEnumerable<T> Data { get; set; }
    }
}
