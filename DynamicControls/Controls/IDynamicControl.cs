using Newtonsoft.Json.Linq;

namespace DynamicControls.Controls
{
    public interface IDynamicControl
    {
        void Build(JObject control);
    }
}