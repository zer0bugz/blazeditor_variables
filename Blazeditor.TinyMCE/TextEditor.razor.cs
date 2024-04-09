using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Blazeditor.TinyMCE;

public partial class TextEditor
{
    [Inject]
    public IJSRuntime JSRuntime { get; set; }

    [Parameter]
    public RenderFragment EditorContent { get; set; }

    [Parameter]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Parameter]
    public BlazeditorOption BlazeditorOption { get; set; } = new BlazeditorOption();

    [Parameter]
    public EventCallback<string> OnChangeCallBack { get; set; }

    protected override async Task OnAfterRenderAsync(bool firstRender)
    {
        await Interop.InitializeTextEditor(JSRuntime, DotNetObjectReference.Create(this), Id, BlazeditorOption, nameof(OnChangeCallback));
    }

    public async Task<string> GetRawContent()
    {
        return await Interop.GetContent(JSRuntime, Id, "raw");
    }

    public async Task<string> GetHtmlContent()
    {
        return await Interop.GetContent(JSRuntime, Id, "html");
    }

    public async Task SetContent(string content)
    {
        await Interop.SetContent(JSRuntime, Id, content);
    }

    [JSInvokable] // This is required in order to JS be able to execute it
    public async Task OnChangeCallback(string response)
    {
        if (OnChangeCallBack.HasDelegate)
        {
            await OnChangeCallBack.InvokeAsync(response);
        }
    }
}
