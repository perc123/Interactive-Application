using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Homography
{
    class HomographyRenderPass : ScriptableRenderPass
    {
        private Material material;

        private RenderTargetIdentifier Source { get; set; }
        private RenderTargetHandle TemporaryColorTexture { get; set; }
        
        public Material Mat
        {
            get => material;
        }
        

        public HomographyRenderPass(Material material)
        {
            this.material = material;
            
            renderPassEvent = RenderPassEvent.AfterRenderingTransparents;
        }

        public void Setup(RenderTargetIdentifier source)
        {
            Source = source;
        }

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            CommandBuffer cmd = CommandBufferPool.Get("_HomographyPass");

            // material.SetFloatArray("_Homography", homography);
            // material.SetFloatArray("_InvHomography", invHomography);
            cmd.GetTemporaryRT(TemporaryColorTexture.id, renderingData.cameraData.cameraTargetDescriptor);
            cmd.Blit(Source, TemporaryColorTexture.Identifier(), material);
            cmd.Blit(TemporaryColorTexture.Identifier(), Source);
            cmd.SetRenderTarget(Source);
            
            
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }
        
        public override void FrameCleanup(CommandBuffer cmd)
        {
            if (TemporaryColorTexture == RenderTargetHandle.CameraTarget) return;
            cmd.ReleaseTemporaryRT(TemporaryColorTexture.id);
            TemporaryColorTexture = RenderTargetHandle.CameraTarget;
        }
    }
}
