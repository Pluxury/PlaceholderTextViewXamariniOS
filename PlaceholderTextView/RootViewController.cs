using System;

using Foundation;
using UIKit;

namespace PlaceholderTextView
{
    public partial class RootViewController : UIViewController
    {
        public RootViewController()
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            TextView.Placeholder = "Scrolling Enabled";

            SecondTextView.Placeholder = "A very long placeholder text in several lines and consisting of many letters and words";

            SecondTextView.PlaceholderColor = UIColor.Red;
        }
    }
}
