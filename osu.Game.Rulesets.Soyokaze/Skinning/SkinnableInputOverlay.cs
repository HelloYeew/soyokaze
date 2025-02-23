﻿// Copyright (c) Alden Wu <aldenwu0@gmail.com>. Licensed under the MIT Licence.
// See the LICENSE file in the repository root for full licence text.

using osu.Framework.Allocation;
using osu.Framework.Bindables;
using osu.Framework.Graphics;
using osu.Framework.Graphics.Containers;
using osu.Game.Rulesets.Soyokaze.Configuration;
using osu.Game.Rulesets.Soyokaze.Extensions;
using osu.Game.Rulesets.Soyokaze.Skinning.Defaults;
using osu.Game.Rulesets.Soyokaze.UI;
using osu.Game.Skinning;
using osuTK;

namespace osu.Game.Rulesets.Soyokaze.Skinning
{
    public class SkinnableInputOverlay : Container
    {
        private SkinnableDrawable[] inputOverlayKeys = new SkinnableDrawable[8];
        private SkinnableDrawable[] inputOverlayBackgrounds = new SkinnableDrawable[2];

        private Bindable<int> screenCenterDistanceBindable = new Bindable<int>();
        private Bindable<int> gapBindable = new Bindable<int>();
        private Bindable<bool> showBindable = new Bindable<bool>();

        private int screenCenterDistance => screenCenterDistanceBindable.Value;
        private int gap => gapBindable.Value;

        private const double press_duration = 60d;
        private const float press_scale = 0.6f;

        public SkinnableInputOverlay()
        {
            for (int i = 0; i < 8; i++)
            {
                inputOverlayKeys[i] = new SkinnableDrawable(
                    new SoyokazeSkinComponent(SoyokazeSkinComponents.InputOverlayKey),
                    _ => new DefaultInputOverlayKey()
                )
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                };
            }

            for (int i = 0; i < 2; i++)
            {
                inputOverlayBackgrounds[i] = new SkinnableDrawable(
                    new SoyokazeSkinComponent(SoyokazeSkinComponents.InputOverlayBackground),
                    _ => new DefaultInputOverlayBackground()
                )
                {
                    Anchor = Anchor.Centre,
                    Origin = Anchor.Centre,
                };
            }
        }

        [BackgroundDependencyLoader(true)]
        private void load(SoyokazeConfigManager cm)
        {
            AddRangeInternal(inputOverlayKeys);
            AddRangeInternal(inputOverlayBackgrounds);

            cm?.BindWith(SoyokazeConfig.InputOverlayScreenCenterDistance, screenCenterDistanceBindable);
            cm?.BindWith(SoyokazeConfig.InputOverlayGap, gapBindable);
            cm?.BindWith(SoyokazeConfig.ShowInputOverlay, showBindable);

            screenCenterDistanceBindable.BindValueChanged(_ => updatePositions(), true);
            gapBindable.BindValueChanged(_ => updatePositions(), true);
            showBindable.BindValueChanged(valueChanged => Alpha = valueChanged.NewValue ? 1f : 0f, true);
        }

        private void updatePositions()
        {
            Vector2[] positions = PositionExtensions.GetPositions(screenCenterDistance, gap, true, Anchor.Centre);
            for (int i = 0; i < 8; i++)
                inputOverlayKeys[i].Position = positions[i];

            inputOverlayBackgrounds[0].Position = new Vector2(-screenCenterDistance, 0);
            inputOverlayBackgrounds[1].Position = new Vector2(screenCenterDistance, 0);
        }

        public void PressKey(SoyokazeAction button)
        {
            inputOverlayKeys[(int)button].ScaleTo(press_scale, press_duration);
        }

        public void UnpressKey(SoyokazeAction button)
        {
            inputOverlayKeys[(int)button].ScaleTo(1f, press_duration);
        }
    }
}
