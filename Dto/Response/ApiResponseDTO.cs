using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Response
{
    public class ApiResponseDTO
    {
        public object Data { get; set; }
        public bool FromCache { get; set; }
    }
}
