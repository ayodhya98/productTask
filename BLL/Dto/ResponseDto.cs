using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Dto
{
    public class ResponseDto<T>
    {
        public bool IsSuccess { get; set; }
        public int? httpCode { get; set; }
        public string? error { get; set; }
        public T data { get; set; }
    }
}
