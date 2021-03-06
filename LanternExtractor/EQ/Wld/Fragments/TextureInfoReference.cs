﻿using System.Collections.Generic;
using System.IO;
using LanternExtractor.Infrastructure.Logger;

namespace LanternExtractor.EQ.Wld.Fragments
{
    /// <summary>
    /// 0x05 - TextureInfoReference
    /// This fragment contains a reference to a TextureInfo (0x04) fragment
    /// </summary>
    class TextureInfoReference : WldFragment
    {
        /// <summary>
        /// The reference to the texture info (0x04)
        /// </summary>
        public TextureInfo TextureInfo { get; private set; }

        public override void Initialize(int index, int id, int size, byte[] data,
            Dictionary<int, WldFragment> fragments,
            Dictionary<int, string> stringHash, ILogger logger)
        {
            base.Initialize(index, id, size, data, fragments, stringHash, logger);

            var reader = new BinaryReader(new MemoryStream(data));

            Name = stringHash[-reader.ReadInt32()];

            int reference = reader.ReadInt32();

            TextureInfo = fragments[reference - 1] as TextureInfo;
        }

        public override void OutputInfo(ILogger logger)
        {
            base.OutputInfo(logger);
            logger.LogInfo("-----");
            logger.LogInfo("0x05: Reference: " + (TextureInfo.Index + 1));
        }
    }
}