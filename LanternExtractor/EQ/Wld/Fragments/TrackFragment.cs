﻿using System.Collections.Generic;
using System.IO;
using LanternExtractor.Infrastructure.Logger;

namespace LanternExtractor.EQ.Wld.Fragments
{
    /// <summary>
    /// 0x13 - Skeleton Piece Track Reference
    /// Refers to the skeleton piece fragment (0x12)
    /// </summary>
    public class TrackFragment : WldFragment
    {
        /// <summary>
        /// Reference to a skeleton piece
        /// </summary>
        public TrackDefFragment TrackDefFragment { get; set; }
        
        public bool IsProcessed { get; set; }

        public override void Initialize(int index, int id, int size, byte[] data,
            Dictionary<int, WldFragment> fragments,
            Dictionary<int, string> stringHash, bool isNewWldFormat, ILogger logger)
        {
            base.Initialize(index, id, size, data, fragments, stringHash, isNewWldFormat, logger);

            var reader = new BinaryReader(new MemoryStream(data));

            Name = stringHash[-reader.ReadInt32()];
            
            int reference = reader.ReadInt32();

            TrackDefFragment = fragments[reference - 1] as TrackDefFragment;
        }

        public override void OutputInfo(ILogger logger)
        {
            base.OutputInfo(logger);

            if (TrackDefFragment != null)
            {
                logger.LogInfo("-----");
                logger.LogInfo("0x13: Skeleton piece reference: " + TrackDefFragment.Index + 1);
            }
        }
    }
}