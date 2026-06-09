using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class OutlineRenderFeature : ScriptableRendererFeature
{
    public override void Create() { }
    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        // This ensures Outline pass is batched properly
        var pass = new OutlinePass();
        renderer.EnqueuePass(pass);
    }
    class OutlinePass : ScriptableRenderPass
    {
        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var sortingCriteria = renderingData.cameraData.defaultOpaqueSortFlags;
            var drawingSettings = CreateDrawingSettings(new ShaderTagId("Outline"), ref renderingData, sortingCriteria);
            drawingSettings.perObjectData = PerObjectData.None;
            context.DrawRenderers(renderingData.cullResults, ref drawingSettings, ref filteringSettings);
        }
        FilteringSettings filteringSettings = new FilteringSettings(RenderQueueRange.all);
    }
}