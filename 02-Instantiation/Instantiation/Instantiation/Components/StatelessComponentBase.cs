using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace Instantiation.Components;

public class StatelessComponentBase 
{
    private RenderHandle _renderHandle;
    public RenderFragment RenderFragment { get; private set; }

    public StatelessComponentBase()
    {
        RenderFragment = BuildRenderTree;
    }

    public void Attach(RenderHandle renderHandle)
    {
        _renderHandle = renderHandle;
    }

    public virtual Task SetParametersAsync(ParameterView parameters)
    {
        // 绑定props参数到具体的组件（为[Parameter]设置值）
        parameters.SetParameterProperties(this);

        // 渲染组件
        _renderHandle.Render(RenderFragment);
        AfterRender();
        return Task.CompletedTask;
    }

    protected virtual void BuildRenderTree(RenderTreeBuilder builder)
    {
    }

    protected virtual void AfterRender()
    {
    }
}
