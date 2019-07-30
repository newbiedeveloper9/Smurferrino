namespace Smurferrino.Business.Structs
{
    public struct GlowStruct
    {
        public byte RenderOccluded;
        public byte RenderUnoccluded;
        public byte FullBloom;

        public GlowStruct(bool renderOccluded, bool renderUnoccluded, bool fullBloom)
        {
            RenderOccluded = renderOccluded ? (byte)1 : (byte)0;
            RenderUnoccluded = renderUnoccluded ? (byte)1 : (byte)0;
            FullBloom = fullBloom ? (byte)1 : (byte)0;
        }
    }
}