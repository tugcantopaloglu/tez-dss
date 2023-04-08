using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonerSermaye.Models.Data
{
    public class FakulteGenelDurumIsteklerViewModel
    {
        public IEnumerable<Istekler> Istekler { get; set; }
        public IEnumerable<FakulteGenelDurumViewModel> FakulteGenelDurumViewModel { get; set; }
    }
}
