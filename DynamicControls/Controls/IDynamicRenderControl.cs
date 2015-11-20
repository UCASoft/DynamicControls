namespace DynamicControls.Controls
{
    public interface IDynamicRenderControl : IDynamicControl
    {
        string Render();

        string RenderChilds(string parentValue);
    }
}