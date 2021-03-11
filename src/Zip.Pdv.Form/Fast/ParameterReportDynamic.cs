using System.Collections.Generic;

namespace Zip.Pdv.Fast
{
    public class ParameterReportDynamic
    {
        public IDictionary<string, object> ListParameters;

        public ParameterReportDynamic()
        {
            ListParameters = new Dictionary<string, object>();
        }
        public void Add(string parameter, object value)
        {
            ListParameters.Add(parameter, value);
        }
    }
}