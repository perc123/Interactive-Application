using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Homography
{
    public class HomographyRenderFeature : ScriptableRendererFeature
    {
        [Serializable]
        public class HomographySettings
        {
            public Material material;
        }

        HomographyRenderPass _homographyRenderPass;
        
        [SerializeField] HomographySettings settings = new();

        public Material Material
        {
            get => settings.material;
            set => settings.material = value;
        }

        public override void Create()
        {
            _homographyRenderPass = new HomographyRenderPass(settings.material)
            {
                renderPassEvent = RenderPassEvent.AfterRenderingPostProcessing
            };
        }

        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            var source = renderer.cameraColorTarget;

            _homographyRenderPass.Setup(source);
            // if (settings.homography != null && settings.homography != null & settings.homography.Length != 0 && settings.invHomography.Length != 0)
            // {
            renderer.EnqueuePass(_homographyRenderPass);
            // }
        }
    }
}