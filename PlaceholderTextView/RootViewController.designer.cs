// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace PlaceholderTextView
{
	[Register ("RootViewController")]
	partial class RootViewController
	{
		[Outlet]
		PlaceholderTextView.PlaceholderTextView SecondTextView { get; set; }

		[Outlet]
		PlaceholderTextView.PlaceholderTextView TextView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (TextView != null) {
				TextView.Dispose ();
				TextView = null;
			}

			if (SecondTextView != null) {
				SecondTextView.Dispose ();
				SecondTextView = null;
			}
		}
	}
}
