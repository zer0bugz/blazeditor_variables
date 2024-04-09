using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Blazeditor.TinyMCE;

public static class Interop
{
    public static ValueTask<object> InitializeTextEditor
    (
        IJSRuntime jSRuntime,
        DotNetObjectReference<TextEditor> dotNetObjectReference,
        string idSelector,
        BlazeditorOption blazeditorOption,
        string onchangeCallback
    )
    {
        return jSRuntime.InvokeAsync<object>
        (
            "blazeditorCallbackProxy",
            dotNetObjectReference,
            "blazeditorInit",
            idSelector,
            new
            {
                content_css = "css/site.css",
                inlineMode = blazeditorOption.InlineMode,
                toolbar = blazeditorOption.Toolbar,
                menubar = blazeditorOption.ShowMenuBar,
                plugins = blazeditorOption.Plugins,
                external_plugins = blazeditorOption.ExternalPlugins,
                paste_data_images = blazeditorOption.PasteDataImage,
                paste_as_text = blazeditorOption.PasteAsText,
                variable_mapper = blazeditorOption.VariableMapper,
                variable_valid = blazeditorOption.VariableValid,
                variable_prefix = blazeditorOption.VariablePrefix,
                variable_suffix = blazeditorOption.VariableSuffix,
            },
            onchangeCallback
        );
    }

    public static ValueTask<string> GetContent(IJSRuntime jSRuntime, string id, string format)
        => jSRuntime.InvokeAsync<string>("blazeditorGetContent", id, format);

    public static ValueTask<object> SetContent(IJSRuntime jSRuntime, string id, string data)
        => jSRuntime.InvokeAsync<object>("blazeditorSetContent", id, data);
}
