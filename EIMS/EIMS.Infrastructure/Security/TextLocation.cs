using iText.Kernel.Geom;
using iText.Svg.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Security
{
    public class TextLocation
    {
        public int PageNumber { get; set; }
        public Rectangle Rect { get; set; }
    }
}
