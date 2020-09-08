using System.Collections.Generic;

namespace Razensoft.Psd.Sections.LayerMaskInformation
{
    internal class LayerInfo
    {
        public short LayerCount { get; }

        public IReadOnlyList<LayerRecords> LayerRecords { get; }

        public IReadOnlyList<ChannelImageData> ChannelImageData { get; }
    }
}
