﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace SimpleSiteCrawler.Lib
{
    internal class SameDomainFilter : ISitePageFilter
    {
        private readonly Uri _root;

        public SameDomainFilter(Uri root)
        {
            _root = root;
        }

        public IEnumerable<SitePage> Apply(IEnumerable<SitePage> input)
        {
            var collected = input.Where(IsHostSame).ToArray();

            return collected;
        }

        private bool IsHostSame(SitePage sitePage)
        {
            return _root.Host == sitePage.Uri.Host;
        }
    }
}