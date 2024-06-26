﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Input;
using Uno.UI.Xaml;

namespace Microsoft.UI.Xaml;

partial class UIElement
{
	partial void PrepareManagedKeyEventBubbling(RoutedEvent routedEvent, ref RoutedEventArgs args, ref BubblingMode bubblingMode)
	{
		var keyArgs = (KeyRoutedEventArgs)args;
		switch (routedEvent.Flag)
		{
			case RoutedEventFlag.KeyDown:
				OnKeyDown(keyArgs);
				break;
		}
	}
}
