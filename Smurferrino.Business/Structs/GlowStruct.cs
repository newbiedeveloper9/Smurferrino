namespace Smurferrino.Business.Structs
{
    public struct GlowStruct
    {
        public bool RenderOccluded;
        public bool RenderUnoccluded;
        public bool FullBloom;

        public GlowStruct(bool renderOccluded, bool renderUnoccluded, bool fullBloom)
        {
            this.RenderOccluded = renderOccluded;
            this.RenderUnoccluded = renderUnoccluded;
            FullBloom = fullBloom;
        }
    }
}