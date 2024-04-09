using System.Collections.Generic;
using System.Linq;

namespace Blazeditor.TinyMCE;

public class BlazeditorOption
{
    public bool InlineMode { get; set; }

    public string Plugins { get; set; } = "paste autolink link wordcount code";

    public Dictionary<string, string> ExternalPlugins { get; set; }

    public string Toolbar { get; set; }

    public bool ShowMenuBar { get; set; }

    public bool PasteAsText { get; set; }

    public bool PasteDataImage { get; set; }

    public Dictionary<string, string> VariableMapper { get; set; }

    public List<string> VariableValid
    {
        get
        {
            return VariableMapper?.Keys.ToList();
        }
    }

    public string VariablePrefix { get; set; } = "%%";

    public string VariableSuffix { get; set; } = "%%";
}
