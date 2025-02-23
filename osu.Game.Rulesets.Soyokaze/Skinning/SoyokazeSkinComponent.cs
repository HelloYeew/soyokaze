﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Game.Skinning;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    public class SoyokazeSkinComponent : GameplaySkinComponent<SoyokazeSkinComponents>
    {
        public SoyokazeSkinComponent(SoyokazeSkinComponents component)
            : base(component)
        {
        }

        protected override string RulesetPrefix => SoyokazeRuleset.SHORT_NAME;

        protected override string ComponentName => Component.ToString().ToLower();
    }
}
