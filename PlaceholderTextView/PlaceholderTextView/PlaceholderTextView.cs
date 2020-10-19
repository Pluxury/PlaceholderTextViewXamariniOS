using System;
using System.ComponentModel;
using Base.UI.iOS.Extensions;
using CoreGraphics;
using Foundation;
using Core.Base.Extensions;
using UIKit;
using System.Collections.Generic;

namespace Base.UI.iOS.Controls.PlaceholderTextView
{
    [Register("PlaceholderTextView"), DesignTimeVisible(true)]
    public partial class PlaceholderTextView : UITextView
    {
        #region Fields

        private UILabel _placeholderLabel;

        private List<NSLayoutConstraint> _constraints;

        #endregion

        #region Properties

        private UIColor _placeholderColor = PlaceholderAppearance.PlaceholderColor;
        /// <summary>
        /// Used to set the Color for the placeholder
        /// </summary>
        /// <value>The default value is <see cref="UIColor"/>.FromRGB(165, 169, 177)</value>
        [Export("PlaceholderColor"), Browsable(true)]
        public UIColor PlaceholderColor
        {
            get => _placeholderColor;
            set
            {
                _placeholderColor = value;
                UpdatePlaceholder();
            }
        }

        private UIFont _placeholderFont = PlaceholderAppearance.PlaceholderFont;
        /// <summary>
        /// Used to set the Font for the placeholder
        /// </summary>
        /// <value>The default value is <see cref="UIFont"/>.SystemFontOfSize(15)</value>
        public UIFont PlaceholderFont
        {
            get => _placeholderFont;
            set
            {
                _placeholderFont = value;
                UpdatePlaceholder();
            }
        }

        private UITextAlignment _placeholderAlignment = PlaceholderAppearance.PlaceholderAlignment;
        /// <summary>
        /// Used to set the text aligment for the placeholder
        /// </summary>
        /// <value>The default value is <see cref="UITextAlignment"/>.Left</value>
        public UITextAlignment PlaceholderAlignment
        {
            get => _placeholderAlignment;
            set
            {
                _placeholderAlignment = value;
                UpdatePlaceholder();
            }
        }

        private NSAttributedString? _attributedPlaceholder;
        public NSAttributedString? AttributedPlaceholder
        {
            get => _attributedPlaceholder;
            set
            {
                _attributedPlaceholder = value;
                UpdatePlaceholder();
            }
        }

        private string _placeholder;
        /// <summary>
        /// Used to set the text of the placeholder
        /// </summary>
        public string Placeholder
        {
            get => _placeholder;
            set
            {
                _placeholder = value;
                UpdatePlaceholder();
            }
        }

        public override string Text
        {
            set
            {
                base.Text = value;

                _placeholderLabel.Hidden = !base.Text.IsNullOrEmtpy();
            }
        }

        private UIEdgeInsets _placeholderInsets = PlaceholderAppearance.Insets;

        public UIEdgeInsets PlaceholderInsets
        {
            get => _placeholderInsets;

            set
            {
                _placeholderInsets = value;

                UpdatePlaceholderConstraints();
            }
        }

        #endregion

        #region Constructor

        public PlaceholderTextView()
        {
            Initialize();
        }

        public PlaceholderTextView(CGRect frame) : base(frame)
        {
            Initialize();
        }

        public PlaceholderTextView(NSCoder coder) : base(coder)
        {
            Initialize();
        }

        public PlaceholderTextView(IntPtr handler) : base(handler)
        {
            Initialize();
        }

        #endregion

        #region Private

        private void Initialize()
        {
            SetupPlaceholder();

            NSNotificationCenter.DefaultCenter.AddObserver(this, new ObjCRuntime.Selector("OnTextChangedNotification:"), TextDidChangeNotification, this);
        }

        private void SetupPlaceholder()
        {
            var label = new UILabel()
                .WithDisabledAutoresizingMask()
                .WithLinesNumber(0);

            label.Layer.ZPosition = -1;

            _placeholderLabel = label;

            AddSubview(_placeholderLabel);
            UpdatePlaceholderConstraints();
        }

        private void UpdatePlaceholderConstraints()
        {
            var newConstraints = new List<NSLayoutConstraint>();

            newConstraints.Add(_placeholderLabel.LeadingAnchor.ConstraintEqualTo(LeadingAnchor, PlaceholderInsets.Left + TextContainer.LineFragmentPadding));

            newConstraints.Add(_placeholderLabel.TopAnchor.ConstraintEqualTo(TopAnchor, PlaceholderInsets.Top));

            newConstraints.Add(HeightAnchor.ConstraintGreaterThanOrEqualTo(_placeholderLabel.HeightAnchor, 1, PlaceholderInsets.Top + PlaceholderInsets.Bottom));

            newConstraints.Add(_placeholderLabel.WidthAnchor.ConstraintEqualTo(WidthAnchor, 1, -(PlaceholderInsets.Left + PlaceholderInsets.Right + TextContainer.LineFragmentPadding)));

            if (_constraints != null)
                NSLayoutConstraint.DeactivateConstraints(_constraints.ToArray());

            NSLayoutConstraint.ActivateConstraints(newConstraints.ToArray());

            _constraints = newConstraints;
        }

        private void UpdatePlaceholder()
        {
            _placeholderLabel.AttributedText = AttributedPlaceholder ?? new NSAttributedString(Placeholder, new UIStringAttributes { Font = PlaceholderFont, ForegroundColor = PlaceholderColor });
            _placeholderLabel.TextAlignment = PlaceholderAlignment;
        }

        #endregion

        #region Protected

        protected override void Dispose(bool disposing)
        {
            NSNotificationCenter.DefaultCenter.RemoveObserver(this);

            base.Dispose(disposing);
        }

        #endregion

        #region Public

        [Export("OnTextChangedNotification:")]
        public void OnTextChangedNotification(NSNotification notification)
           => _placeholderLabel.Hidden = !Text.IsNullOrEmtpy();

        #endregion
    }
}
