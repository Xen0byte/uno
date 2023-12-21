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
	[global::System.Runtime.CompilerServices.CreateNewOnMetadataUpdate]
	partial class MainPage : global::Microsoft.UI.Xaml.Controls.Page
	{
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		internal string __checksum() => "09e2aaf9274c57d3e9cf35f1ca51514a3a6916ed";
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		private const string __baseUri_prefix_MainPage_d6cd66944958ced0c513e0a04797b51d = "ms-appx:///TestProject/";
		[global::System.ComponentModel.EditorBrowsable(global::System.ComponentModel.EditorBrowsableState.Never)]
		private const string __baseUri_MainPage_d6cd66944958ced0c513e0a04797b51d = "ms-appx:///TestProject/";
		private global::Microsoft.UI.Xaml.NameScope __nameScope = new global::Microsoft.UI.Xaml.NameScope();
		private void InitializeComponent()
		{
			var __resourceLocator = new global::System.Uri("file:///C:/Project/0/MainPage.xaml");
			if(global::Uno.UI.ApplicationHelper.IsLoadableComponent(__resourceLocator))
			{
				global::Microsoft.UI.Xaml.Application.LoadComponent(this, __resourceLocator);
				return;
			}
			NameScope.SetNameScope(this, __nameScope);
			var __that = this;
			base.IsParsing = true;
			Resources[
			"ImportantNumber"
			] = 
			12d
			;
			Resources[
			"ImportantMessage"
			] = 
			"Do more testing"
			;
			Resources[
			"MyTextBlockResource"
			] = 
			new global::Microsoft.UI.Xaml.Controls.TextBlock
			{
				IsParsing = true,
				Text = "use me",
				// Source 0\MainPage.xaml (Line 9:3)
			}
			.GenericApply(__that, __nameScope, ((c0, __that, __nameScope) => 
			{
				global::Uno.UI.FrameworkElementHelper.SetBaseUri(c0, __baseUri_MainPage_d6cd66944958ced0c513e0a04797b51d, "file:///C:/Project/0/MainPage.xaml", 9, 3);
				c0.CreationComplete();
			}
			))
			;
			// Source 0\MainPage.xaml (Line 1:2)
			base.Content = 
			new global::Microsoft.UI.Xaml.Controls.TextBlock
			{
				IsParsing = true,
				Text = "Some content",
				// Source 0\MainPage.xaml (Line 11:4)
			}
			.GenericApply(__that, __nameScope, ((c1, __that, __nameScope) => 
			{
				global::Uno.UI.FrameworkElementHelper.SetBaseUri(c1, __baseUri_MainPage_d6cd66944958ced0c513e0a04797b51d, "file:///C:/Project/0/MainPage.xaml", 11, 4);
				c1.CreationComplete();
			}
			))
			;
			
			this
			.GenericApply(__that, __nameScope, ((c2, __that, __nameScope) => 
			{
				// Source 0\MainPage.xaml (Line 1:2)
				
				// WARNING Property c2.base does not exist on {http://schemas.microsoft.com/winfx/2006/xaml/presentation}Page, the namespace is http://www.w3.org/XML/1998/namespace. This error was considered irrelevant by the XamlFileGenerator
			}
			))
			.GenericApply(__that, __nameScope, ((c3, __that, __nameScope) => 
			{
				/* _isTopLevelDictionary:False */
				__that._component_0 = c3;
				// Class TestRepro.MainPage
				global::Uno.UI.ResourceResolverSingleton.Instance.ApplyResource(c3, global::Microsoft.UI.Xaml.Controls.Page.BackgroundProperty, "ApplicationPageBackgroundThemeBrush", isThemeResourceExtension: true, isHotReloadSupported: true, context: global::MyProject.GlobalStaticResources.__ParseContext_);
				global::Uno.UI.FrameworkElementHelper.SetBaseUri(c3, __baseUri_MainPage_d6cd66944958ced0c513e0a04797b51d, "file:///C:/Project/0/MainPage.xaml", 1, 2);
				c3.CreationComplete();
			}
			))
			;
			OnInitializeCompleted();

			Bindings = ((IMainPage_Bindings)global::Uno.UI.Helpers.TypeMappings.CreateInstance<MainPage_Bindings>(this));
			global::Uno.UI.Helpers.MarkupHelper.SetElementProperty(__that, "owner", __that);
			Loading += (s, e) => 
			{
				var __that = global::Uno.UI.Helpers.MarkupHelper.GetElementProperty<global::TestRepro.MainPage>(s, "owner");
				__that.Bindings.UpdateResources();
			}
			;
		}
		partial void OnInitializeCompleted();
		private global::Microsoft.UI.Xaml.Markup.ComponentHolder _component_0_Holder { get; } = new global::Microsoft.UI.Xaml.Markup.ComponentHolder(isWeak: true);
		private global::Microsoft.UI.Xaml.Controls.Page _component_0
		{
			get
			{
				return (global::Microsoft.UI.Xaml.Controls.Page)_component_0_Holder.Instance;
			}
			set
			{
				_component_0_Holder.Instance = value;
			}
		}
		private interface IMainPage_Bindings/*INTENTIONAL CHANGE TO VERIFY FAILURE IN CI*/
		{
			void Initialize();
			void Update();
			void UpdateResources();
			void StopTracking();
		}
		#pragma warning disable 0169 //  Suppress unused field warning in case Bindings is not used.
		private IMainPage_Bindings Bindings;
		#pragma warning restore 0169
		[global::System.Runtime.CompilerServices.CreateNewOnMetadataUpdate]
		[global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
		private class MainPage_Bindings : IMainPage_Bindings
		{
			#if UNO_HAS_UIELEMENT_IMPLICIT_PINNING
			private global::System.WeakReference _ownerReference;
			private global::TestRepro.MainPage Owner { get => (global::TestRepro.MainPage)_ownerReference?.Target; set => _ownerReference = new global::System.WeakReference(value); }
			#else
			private global::TestRepro.MainPage Owner { get; set; }
			#endif
			public MainPage_Bindings(global::TestRepro.MainPage owner)
			{
				Owner = owner;
			}
			void IMainPage_Bindings.Initialize()
			{
			}
			void IMainPage_Bindings.Update()
			{
				var owner = Owner;
			}
			void IMainPage_Bindings.UpdateResources()
			{
				var owner = Owner;
				owner._component_0.UpdateResourceBindings();
			}
			void IMainPage_Bindings.StopTracking()
			{
			}
		}
	}
}
