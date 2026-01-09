using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface ILookupCodeGenerator
    {
        string Generate(int length = 10);
    }
}