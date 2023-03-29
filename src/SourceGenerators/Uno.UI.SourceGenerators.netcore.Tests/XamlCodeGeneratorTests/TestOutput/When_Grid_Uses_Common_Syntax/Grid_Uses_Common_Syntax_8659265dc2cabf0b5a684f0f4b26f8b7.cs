﻿// <autogenerated />
#pragma warning disable CS0114
#pragma warning disable CS0108
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Uno.UI;
using Uno.UI.Xaml;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Shapes;
using Windows.UI.Text;
using Uno.Extensions;
using Uno;
using Uno.UI.Helpers.Xaml;
using MyProject;

#if __ANDROID__
using _View = Android.Views.View;
#elif __IOS__
using _View = UIKit.UIView;
#elif __MACOS__
using _View = AppKit.NSView;
#elif UNO_REFERENCE_API || NET461
using _View = Windows.UI.Xaml.UIElement;
#endif

namespace Uno.UI.Tests.Windows_UI_XAML_Controls.GridTests.Controls
{
	partial class Grid_Uses_Common_Syntax : global::Windows.UI.Xaml.Controls.Page
	{
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		private const string __baseUri_prefix_Grid_Uses_Common_Syntax_8659265dc2cabf0b5a684f0f4b26f8b7 = "ms-appx:///TestProject/";
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		private const string __baseUri_Grid_Uses_Common_Syntax_8659265dc2cabf0b5a684f0f4b26f8b7 = "ms-appx:///TestProject/";
				global::Windows.UI.Xaml.NameScope __nameScope = new global::Windows.UI.Xaml.NameScope();
		private void InitializeComponent()
		{
			InitializeComponent_1A305259();
		}
		private void InitializeComponent_1A305259()
		{
			NameScope.SetNameScope(this, __nameScope);
			var __that = this;
			base.IsParsing = true;
			// Source 0\Grid_Uses_Common_Syntax.xaml (Line 1:2)
			base.Content = 
			new global::Windows.UI.Xaml.Controls.Grid
			{
				IsParsing = true,
				Name = "grid",
				ColumnDefinitions = 
				{
					new global::Windows.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Windows.UI.Xaml.GridLength(1f, global::Windows.UI.Xaml.GridUnitType.Star),
						// Source 0\Grid_Uses_Common_Syntax.xaml (Line 15:14)
					}
					,
					new global::Windows.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Windows.UI.Xaml.GridLength(2f, global::Windows.UI.Xaml.GridUnitType.Star),
						// Source 0\Grid_Uses_Common_Syntax.xaml (Line 16:14)
					}
					,
					new global::Windows.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Windows.UI.Xaml.GridLength(1f, global::Windows.UI.Xaml.GridUnitType.Auto),
						// Source 0\Grid_Uses_Common_Syntax.xaml (Line 17:14)
					}
					,
					new global::Windows.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Windows.UI.Xaml.GridLength(1f, global::Windows.UI.Xaml.GridUnitType.Star),
						// Source 0\Grid_Uses_Common_Syntax.xaml (Line 18:14)
					}
					,
					new global::Windows.UI.Xaml.Controls.ColumnDefinition
					{
						Width = new global::Windows.UI.Xaml.GridLength(300f, global::Windows.UI.Xaml.GridUnitType.Pixel),
						// Source 0\Grid_Uses_Common_Syntax.xaml (Line 19:14)
					}
					,
				}
				,
				RowDefinitions = 
				{
					new global::Windows.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Windows.UI.Xaml.GridLength(1f, global::Windows.UI.Xaml.GridUnitType.Star),
						// Source 0\Grid_Uses_Common_Syntax.xaml (Line 22:14)
					}
					,
					new global::Windows.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Windows.UI.Xaml.GridLength(1f, global::Windows.UI.Xaml.GridUnitType.Auto),
						// Source 0\Grid_Uses_Common_Syntax.xaml (Line 23:14)
					}
					,
					new global::Windows.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Windows.UI.Xaml.GridLength(25f, global::Windows.UI.Xaml.GridUnitType.Pixel),
						// Source 0\Grid_Uses_Common_Syntax.xaml (Line 24:14)
					}
					,
					new global::Windows.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Windows.UI.Xaml.GridLength(14f, global::Windows.UI.Xaml.GridUnitType.Pixel),
						// Source 0\Grid_Uses_Common_Syntax.xaml (Line 25:14)
					}
					,
					new global::Windows.UI.Xaml.Controls.RowDefinition
					{
						Height = new global::Windows.UI.Xaml.GridLength(20f, global::Windows.UI.Xaml.GridUnitType.Pixel),
						// Source 0\Grid_Uses_Common_Syntax.xaml (Line 26:14)
					}
					,
				}
				,
				// Source 0\Grid_Uses_Common_Syntax.xaml (Line 10:6)
			}
			.Grid_Uses_Common_Syntax_8659265dc2cabf0b5a684f0f4b26f8b7_XamlApply((Grid_Uses_Common_Syntax_8659265dc2cabf0b5a684f0f4b26f8b7XamlApplyExtensions.XamlApplyHandler2)(c10 => 
			{
				__nameScope.RegisterName("grid", c10);
				__that.grid = c10;
				// FieldModifier public
				global::Uno.UI.FrameworkElementHelper.SetBaseUri(c10, __baseUri_Grid_Uses_Common_Syntax_8659265dc2cabf0b5a684f0f4b26f8b7);
				c10.CreationComplete();
			}
			))
			;
			
			this
			.GenericApply(((c11) => 
			{
				// Source 0\Grid_Uses_Common_Syntax.xaml (Line 1:2)
				
				// WARNING Property c11.base does not exist on {http://schemas.microsoft.com/winfx/2006/xaml/presentation}Page, the namespace is http://www.w3.org/XML/1998/namespace. This error was considered irrelevant by the XamlFileGenerator
			}
			))
			.GenericApply(((c12) => 
			{
				// Class Uno.UI.Tests.Windows_UI_XAML_Controls.GridTests.Controls.Grid_Uses_Common_Syntax
				global::Uno.UI.FrameworkElementHelper.SetBaseUri(c12, __baseUri_Grid_Uses_Common_Syntax_8659265dc2cabf0b5a684f0f4b26f8b7);
				c12.CreationComplete();
			}
			))
			;
			OnInitializeCompleted();

		}
		partial void OnInitializeCompleted();
		private global::Windows.UI.Xaml.Data.ElementNameSubject _gridSubject = new global::Windows.UI.Xaml.Data.ElementNameSubject();
		public global::Windows.UI.Xaml.Controls.Grid grid
		{
			get
			{
				return (global::Windows.UI.Xaml.Controls.Grid)_gridSubject.ElementInstance;
			}
			set
			{
				_gridSubject.ElementInstance = value;
			}
		}

	}
}
namespace MyProject
{
	static class Grid_Uses_Common_Syntax_8659265dc2cabf0b5a684f0f4b26f8b7XamlApplyExtensions
	{
		public delegate void XamlApplyHandler0(global::Windows.UI.Xaml.Controls.ColumnDefinition instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Windows.UI.Xaml.Controls.ColumnDefinition Grid_Uses_Common_Syntax_8659265dc2cabf0b5a684f0f4b26f8b7_XamlApply(this global::Windows.UI.Xaml.Controls.ColumnDefinition instance, XamlApplyHandler0 handler)
		{
			handler(instance);
			return instance;
		}
		public delegate void XamlApplyHandler1(global::Windows.UI.Xaml.Controls.RowDefinition instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Windows.UI.Xaml.Controls.RowDefinition Grid_Uses_Common_Syntax_8659265dc2cabf0b5a684f0f4b26f8b7_XamlApply(this global::Windows.UI.Xaml.Controls.RowDefinition instance, XamlApplyHandler1 handler)
		{
			handler(instance);
			return instance;
		}
		public delegate void XamlApplyHandler2(global::Windows.UI.Xaml.Controls.Grid instance);
		[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
		public static global::Windows.UI.Xaml.Controls.Grid Grid_Uses_Common_Syntax_8659265dc2cabf0b5a684f0f4b26f8b7_XamlApply(this global::Windows.UI.Xaml.Controls.Grid instance, XamlApplyHandler2 handler)
		{
			handler(instance);
			return instance;
		}
	}
}
