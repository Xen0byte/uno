﻿// <autogenerated />
#pragma warning disable CS0114
#pragma warning disable CS0108
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Uno.UI;
using Uno.UI.Xaml;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Shapes;
using Windows.UI.Text;
using Uno.Extensions;
using Uno;
using Uno.UI.Helpers;
using Uno.UI.Helpers.Xaml;
using MyProject;

#if __ANDROID__
using _View = Android.Views.View;
#elif __IOS__
using _View = UIKit.UIView;
#elif __MACOS__
using _View = AppKit.NSView;
#else
using _View = Microsoft.UI.Xaml.UIElement;
#endif

namespace TestRepro
{
	partial class MainPage : global::Microsoft.UI.Xaml.Controls.Page
	{
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		private const string __baseUri_prefix_MainPage_d6cd66944958ced0c513e0a04797b51d = "ms-appx:///TestProject/";
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		private const string __baseUri_MainPage_d6cd66944958ced0c513e0a04797b51d = "ms-appx:///TestProject/";
		private global::Microsoft.UI.Xaml.NameScope __nameScope = new global::Microsoft.UI.Xaml.NameScope();
		private void InitializeComponent()
		{
			NameScope.SetNameScope(this, __nameScope);
			var __that = this;
			base.IsParsing = true;
			// Source 0\MainPage.xaml (Line 1:2)
			base.Content = 
			new global::Microsoft.UI.Xaml.Controls.Grid
			{
				IsParsing = true,
				// Source 0\MainPage.xaml (Line 8:3)
				Children = 
				{
					new global::Microsoft.UI.Xaml.Controls.TextBlock
					{
						IsParsing = true,
						Text = "Hello, world!",
						Margin = new global::Microsoft.UI.Xaml.Thickness(20),
						FontSize = 30d,
						// Source 0\MainPage.xaml (Line 9:4)
					}
					.MainPage_d6cd66944958ced0c513e0a04797b51d_XamlApply((MainPage_d6cd66944958ced0c513e0a04797b51dXamlApplyExtensions.XamlApplyHandler0)(__p1 => 
					{
					global::Uno.UI.FrameworkElementHelper.SetBaseUri(__p1, __baseUri_MainPage_d6cd66944958ced0c513e0a04797b51d);
					__p1.CreationComplete();
					}
					))
					,
				}
			}
			.MainPage_d6cd66944958ced0c513e0a04797b51d_XamlApply((MainPage_d6cd66944958ced0c513e0a04797b51dXamlApplyExtensions.XamlApplyHandler1)(__p1 => 
			{
			global::Uno.UI.Toolkit.VisibleBoundsPadding.SetPaddingMask(__p1, global::Uno.UI.Toolkit.VisibleBoundsPadding.PaddingMask.Top);
			global::Uno.UI.FrameworkElementHelper.SetBaseUri(__p1, __baseUri_MainPage_d6cd66944958ced0c513e0a04797b51d);
			__p1.CreationComplete();
			}
			))
			;
			
			this
			.MainPage_d6cd66944958ced0c513e0a04797b51d_XamlApply((MainPage_d6cd66944958ced0c513e0a04797b51dXamlApplyExtensions.XamlApplyHandler2)(__p1 => 
			{
			// Source 0\MainPage.xaml (Line 1:2)
			
			// WARNING Property __p1.base does not exist on {http://schemas.microsoft.com/winfx/2006/xaml/presentation}Page, the namespace is http://www.w3.org/XML/1998/namespace. This error was considered irrelevant by the XamlFileGenerator
			}
			))
			.MainPage_d6cd66944958ced0c513e0a04797b51d_XamlApply((MainPage_d6cd66944958ced0c513e0a04797b51dXamlApplyExtensions.XamlApplyHandler2)(__p1 => 
			{
			// Class TestRepro.MainPage
			global::Uno.UI.FrameworkElementHelper.SetBaseUri(__p1, __baseUri_MainPage_d6cd66944958ced0c513e0a04797b51d);
			__p1.CreationComplete();
			}
			))
			;
			OnInitializeCompleted();

		}
		partial void OnInitializeCompleted();
	}
}
namespace MyProject
{
	static class MainPage_d6cd66944958ced0c513e0a04797b51dXamlApplyExtensions
	{
		public delegate void XamlApplyHandler0(global::Microsoft.UI.Xaml.Controls.TextBlock instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Microsoft.UI.Xaml.Controls.TextBlock MainPage_d6cd66944958ced0c513e0a04797b51d_XamlApply(this global::Microsoft.UI.Xaml.Controls.TextBlock instance, XamlApplyHandler0 handler)
		{
			handler(instance);
			return instance;
		}
		public delegate void XamlApplyHandler1(global::Microsoft.UI.Xaml.Controls.Grid instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Microsoft.UI.Xaml.Controls.Grid MainPage_d6cd66944958ced0c513e0a04797b51d_XamlApply(this global::Microsoft.UI.Xaml.Controls.Grid instance, XamlApplyHandler1 handler)
		{
			handler(instance);
			return instance;
		}
		public delegate void XamlApplyHandler2(global::Microsoft.UI.Xaml.Controls.Page instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Microsoft.UI.Xaml.Controls.Page MainPage_d6cd66944958ced0c513e0a04797b51d_XamlApply(this global::Microsoft.UI.Xaml.Controls.Page instance, XamlApplyHandler2 handler)
		{
			handler(instance);
			return instance;
		}
	}
}
