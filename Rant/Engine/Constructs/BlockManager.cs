﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rant.Engine.Syntax;

namespace Rant.Engine.Constructs
{
	internal class BlockManager
	{
        private Dictionary<BlockAttribs, List<RABlock>> _blockList;

        private BlockAttribs _prevAttribs;
        private int _prevCount = 0;

		public BlockManager()
		{
            _blockList = new Dictionary<BlockAttribs, List<RABlock>>();
		}

        public BlockAttribs SetPrevAttribs(BlockAttribs attribs)
        {
            _prevAttribs = attribs;
            return attribs;
        }

        public BlockAttribs GetPrevious(int repeatMaximum)
        {
            if (repeatMaximum < 0)
                return _prevAttribs;

            if (_prevCount < repeatMaximum - 1)
            {
                _prevCount += 1;
                return _prevAttribs;
            }
            else
            {
                _prevCount = 0;
                return new BlockAttribs();
            }
        }

        public void Add(BlockAttribs attribs, RABlock block)
        {
            if (!_blockList.ContainsKey(attribs))
                _blockList.Add(attribs, new List<RABlock>() { block });
            else
                _blockList[attribs].Add(block);
        }

        public void RemoveAttribs(BlockAttribs attribs)
        {
            if (!_blockList.ContainsKey(attribs))
                throw new InvalidOperationException("Attribs don't exist");

            _blockList.Remove(attribs);
        }

        public void RemoveBlock(BlockAttribs attribs, RABlock block)
        {
            if (!_blockList.ContainsKey(attribs))
                throw new InvalidOperationException("Attribs don't exist");

            if (!_blockList[attribs].Contains(block))
                throw new InvalidOperationException("Block doesn't exist");

            _blockList[attribs].Remove(block);
        }

        public BlockAttribs GetAttribs(RABlock block) => _blockList.Where(p => p.Value.Contains(block)).Single().Key;
	}
}