using System;
using UIKit;

namespace PlaceholderTextView.PlaceholderTextView
{
    public partial class PlaceholderTextView
    {
        public static class PlaceholderAppearance
        {
            public static UIColor PlaceholderColor { get; set; } = UIColor.FromRGB(0xA5, 0xA9, 0xB1);

            public static UIFont PlaceholderFont { get; set; } = UIFont.SystemFontOfSize(15);

            public static UITextAlignment PlaceholderAlignment { get; set; } = UITextAlignment.Left;

            public static UIEdgeInsets Insets { get; set; } = new UIEdgeInsets(8, 0, 8, 0);
        }
    }
}
