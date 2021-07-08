using System.Collections.Generic;
using System.Linq;

namespace Zip.Pdv.Cadastro.Model
{
    public class ResultValidModel
    {
        public ResultValidModel()
        {
            ResultValidItens = new List<ResultValidItemModel>();
        }
        public bool IsValid => !ResultValidItens.Any();
        public string ResultValidItemToString {
            get {
                string separator = "\n";
                return ResultValidItens.Aggregate("", (current, subItem) => current + (separator + subItem.Menssage)) + " "; ; }
        }
        public ICollection<ResultValidItemModel> ResultValidItens { get; set; }
    }

    public class ResultValidItemModel
    {
        public string Menssage { get; set; }
    }
}