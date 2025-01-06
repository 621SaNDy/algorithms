using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Firma
{
    internal interface IZapisywalna
    {
        void ZapiszBin(string nazwa);
        Object OdczytajBin(string nazwa);
    }
}
