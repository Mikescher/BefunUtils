using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Rendering;
using ICSharpCode.AvalonEdit.Utils;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace BefunWrite.Controls
{
	//
	// Reference: https://github.com/icsharpcode/SharpDevelop/blob/master/src/AddIns/DisplayBindings/AvalonEdit.AddIn/Src/IconBarMargin.cs
	//
	public class IconBarMargin : AbstractMargin, IDisposable
	{
		private long errorLine = -1;
		private string errorpopup = "<NULL>";

		private ImageSource img_Error;
		readonly MouseHoverLogic hoverLogic;

		private ICSharpCode.AvalonEdit.TextEditor Editor;

		public IconBarMargin(ICSharpCode.AvalonEdit.TextEditor _editor)
		{
			this.Editor = _editor;

			BitmapImage b;

			b = new BitmapImage();
			b.BeginInit();
			b.UriSource = new Uri("pack://application:,,,/BefunWrite;component/icons/exclamation-diamond-frame.png");
			b.EndInit();
			img_Error = b;

			this.hoverLogic = new MouseHoverLogic(this);
			this.hoverLogic.MouseHover += (sender, e) => MouseHover(this, e);
			this.hoverLogic.MouseHoverStopped += (sender, e) => MouseHoverStopped(this, e);
			this.Unloaded += OnUnloaded;
		}

		protected override void OnTextViewChanged(TextView oldTextView, TextView newTextView)
		{
			if (oldTextView != null)
			{
				oldTextView.VisualLinesChanged -= OnRedrawRequested;
				oldTextView.MouseMove -= TextViewMouseMove;
			}
			base.OnTextViewChanged(oldTextView, newTextView);
			if (newTextView != null)
			{
				newTextView.VisualLinesChanged += OnRedrawRequested;
				newTextView.MouseMove += TextViewMouseMove;
			}

			InvalidateVisual();
		}

		private void OnRedrawRequested(object sender, EventArgs e)
		{
			if (this.TextView != null && this.TextView.VisualLinesValid)
			{
				InvalidateVisual();
			}
		}

		public virtual void Dispose()
		{
			this.TextView = null;
		}

		protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters)
		{
			return new PointHitTestResult(this, hitTestParameters.HitPoint);
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			return new Size(18, 0);
		}

		protected override void OnRender(DrawingContext drawingContext)
		{
			Size renderSize = this.RenderSize;
			drawingContext.DrawRectangle(SystemColors.ControlBrush, null,
			new Rect(0, 0, renderSize.Width, renderSize.Height));
			drawingContext.DrawLine(new Pen(SystemColors.ControlDarkBrush, 1),
			new Point(renderSize.Width - 0.5, 0),
			new Point(renderSize.Width - 0.5, renderSize.Height));

			TextView textView = this.TextView;
			if (textView != null && textView.VisualLinesValid)
			{
				Size pixelSize = PixelSnapHelpers.GetPixelSize(this);
				foreach (VisualLine line in textView.VisualLines)
				{
					int lineNumber = line.FirstDocumentLine.LineNumber;

					bool bp = errorLine == lineNumber;

					double lineMiddle = line.GetTextLineVisualYPosition(line.TextLines[0], VisualYPosition.TextMiddle) - textView.VerticalOffset;
					Rect rect = new Rect(0, PixelSnapHelpers.Round(lineMiddle - 8, pixelSize.Height), 16, 16);

					if (bp)
					{
						drawingContext.DrawImage(img_Error, rect);
					}
				}
			}
		}

		public void SetError(long p, string t)
		{
			errorLine = p;
			errorpopup = t;

			if (this.TextView != null && this.TextView.VisualLinesValid)
			{
				InvalidateVisual();
			}
		}

		public void MakeLineVisible(int line)
		{
			if (this.TextView != null)
			{
				try
				{
					int top = Math.Max(line - this.TextView.VisualLines.Count / 2, 0);

					double visualTop = this.TextView.GetVisualTopByDocumentLine(top);

					Editor.ScrollToVerticalOffset(visualTop);
				}
				catch
				{
					// Do nothing - can't scroll properly
				}
			}
		}

		#region Tooltip

		private ToolTip toolTip;
		private Popup popupToolTip;

		private int GetLineFromMousePosition(MouseEventArgs e)
		{
			TextView textView = this.TextView;
			if (textView == null)
				return 0;
			VisualLine vl = textView.GetVisualLineFromVisualTop(e.GetPosition(textView).Y + textView.ScrollOffset.Y);
			if (vl == null)
				return 0;
			return vl.FirstDocumentLine.LineNumber;
		}

		private void MouseHover(object sender, MouseEventArgs e)
		{
			Debug.Assert(sender == this);

			if (!TryCloseExistingPopup(false))
			{
				return;
			}

			int line = GetLineFromMousePosition(e);
			if (line < 1)
				return;
			if (line != errorLine)
				return;

			if (toolTip == null)
			{
				toolTip = new ToolTip();
				toolTip.Closed += ToolTipClosed;
			}

			toolTip.PlacementTarget = this;

			toolTip.Content = new TextBlock
			{
				Text = errorpopup,
				TextWrapping = TextWrapping.Wrap
			};

			e.Handled = true;
			toolTip.IsOpen = true;
		}

		private bool TryCloseExistingPopup(bool mouseClick)
		{
			if (popupToolTip != null)
			{
				popupToolTip.IsOpen = false;
				popupToolTip = null;
			}
			return true;
		}

		private static Point TransformFromDevice(Point point, Visual visual)
		{
			Matrix matrix = PresentationSource.FromVisual(visual).CompositionTarget.TransformFromDevice;
			return new Point(point.X * matrix.M11, point.Y * matrix.M22);
		}

		private Point GetPopupPosition(int line)
		{
			Point positionInPixels = TextView.PointToScreen(TextView.GetVisualPosition(new TextViewPosition(line, 1), VisualYPosition.LineBottom) - this.TextView.ScrollOffset);
			positionInPixels.X -= 50;

			return TransformFromDevice(positionInPixels, this);
		}

		private void MouseHoverStopped(object sender, MouseEventArgs e)
		{
			if (toolTip != null)
			{
				toolTip.IsOpen = false;
				e.Handled = true;
			}
		}

		private double distanceToPopupLimit;
		private const double MaxMovementAwayFromPopup = 5;

		private double GetDistanceToPopup(MouseEventArgs e)
		{
			Point p = popupToolTip.Child.PointFromScreen(PointToScreen(e.GetPosition(this)));
			Size size = popupToolTip.Child.RenderSize;
			double x = 0;
			if (p.X < 0)
				x = -p.X;
			else if (p.X > size.Width)
				x = p.X - size.Width;
			double y = 0;
			if (p.Y < 0)
				y = -p.Y;
			else if (p.Y > size.Height)
				y = p.Y - size.Height;
			return Math.Sqrt(x * x + y * y);
		}

		protected override void OnMouseLeave(MouseEventArgs e)
		{
			if (popupToolTip != null && !popupToolTip.IsMouseOver && GetDistanceToPopup(e) > 10)
			{
				TryCloseExistingPopup(false);
			}
		}

		private void TextViewMouseMove(object sender, MouseEventArgs e)
		{
			if (popupToolTip != null)
			{
				double distanceToPopup = GetDistanceToPopup(e);
				if (distanceToPopup > distanceToPopupLimit)
				{
					TryCloseExistingPopup(false);
				}
				else
				{
					distanceToPopupLimit = Math.Min(distanceToPopupLimit, distanceToPopup + MaxMovementAwayFromPopup);
				}
			}
		}

		private void OnUnloaded(object sender, EventArgs e)
		{
			TryCloseExistingPopup(true);
		}

		private void ToolTipClosed(object sender, EventArgs e)
		{
			if (toolTip == sender)
			{
				toolTip = null;
			}
			if (popupToolTip == sender)
			{
				popupToolTip.Closed -= ToolTipClosed;
				popupToolTip = null;
			}
		}
		#endregion
	}
}
