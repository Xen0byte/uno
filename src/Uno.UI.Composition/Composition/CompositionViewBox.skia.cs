#nullable enable
using System;
using System.Linq;
using SkiaSharp;
using Windows.Foundation;

namespace Windows.UI.Composition;

public partial class CompositionViewBox
{
	internal SKRect GetSKRect()
		=> new(
			left: Offset.X,
			top: Offset.Y,
			right: Offset.X + Size.X,
			bottom: Offset.Y + Size.Y);
}
