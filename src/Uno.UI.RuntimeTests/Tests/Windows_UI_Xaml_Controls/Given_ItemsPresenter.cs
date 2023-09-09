using System.Linq;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;
using FluentAssertions;
using MUXControlsTestApp.Utilities;
using static Private.Infrastructure.TestServices;
using Uno.UI.RuntimeTests.Extensions;
using Windows.UI.Xaml;
using Uno.Extensions;

namespace Uno.UI.RuntimeTests.Tests.Windows_UI_Xaml_Controls;

public static class AssertionExtensions
{
	public static AndConstraint<FluentAssertions.Collections.GenericCollectionAssertions<float>> BeEquivalentToWithTolerance<T>(this FluentAssertions.Collections.GenericCollectionAssertions<float> assertions, float[] expected, float tolerance)
	{
		assertions.Subject.Should().HaveCount(expected.Length);

		var subject = assertions.Subject.ToArray();

		for (var i = 0; i < subject.Length; i++)
		{
			var sub = subject[i];
			var actual = expected[i];
			sub.Should().BeApproximately(actual, tolerance);
		}

		return new AndConstraint<FluentAssertions.Collections.GenericCollectionAssertions<float>>(assertions);
	}
}

[TestClass]
public class Given_ItemsPresenter
{
	// Due to physical/logical pixel conversion on Android, measurements aren't exact
	private float Epsilon =>
#if XAMARIN
			0.5f;
#else
		0.0f;
#endif


	[TestMethod]
	[RunsOnUIThread]
	[RequiresFullWindow]
	public async Task When_Big_Elements_Horizontal_Many_Margins()
	{

		var grid = (Grid)XamlReader.Load("""
										 <Grid x:Name="g" Width="600" Height="700"
										 	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
										 	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
										 	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
										 	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
										 	mc:Ignorable="d"
										 	>
										 <Border BorderBrush="Bisque" BorderThickness="5">
										      <ItemsControl x:Name="ic">
										          <ItemsControl.Items>
										              <Border Width="400" Height="300" Margin="1">
										                  <Ellipse Fill="Red" />
										              </Border>
										              <Border Width="400" Height="300" Margin="3">
										                  <Ellipse Fill="Green" />
										              </Border>
										              <Border Width="400" Height="300" VerticalAlignment="Bottom"  Margin="9">
										                  <Ellipse Fill="Blue" />
										              </Border>
										          </ItemsControl.Items>
										          <ItemsControl.ItemsPanel>
										              <ItemsPanelTemplate>
										                  <StackPanel Margin="29" Orientation="Horizontal" />
										              </ItemsPanelTemplate>
										          </ItemsControl.ItemsPanel>
										          <ItemsControl.Template>
										              <ControlTemplate TargetType="ItemsControl">
										                  <ItemsPresenter Margin="47">
										                      <ItemsPresenter.Header>
										                          <Border Width="200" Height="200" VerticalAlignment="Bottom" Margin="59">
										                              <Ellipse Fill="Yellow" />
										                          </Border>
										                      </ItemsPresenter.Header>
										                      <ItemsPresenter.Footer>
										                          <Border Width="200" Height="200" VerticalAlignment="Top" Margin="39">
										                              <Ellipse Fill="Pink" />
										                          </Border>
										                      </ItemsPresenter.Footer>
										                  </ItemsPresenter>
										              </ControlTemplate>
										          </ItemsControl.Template>
										      </ItemsControl>
										 </Border>
										 </Grid>
										 """);

		WindowHelper.WindowContent = grid;
		await WindowHelper.WaitForLoaded(grid);
		await WindowHelper.WaitForIdle();

		var ic = (ItemsControl)grid.FindName("ic");
		var ip = grid.FindVisualChildByType<ItemsPresenter>();
		var header = (ContentControl)VisualTreeHelper.GetChild(ip, 0);
		var footer = (ContentControl)VisualTreeHelper.GetChild(ip, 2);
		var panel = grid.FindVisualChildByType<StackPanel>();
		var item1 = (FrameworkElement)panel.Children[0];
		var item2 = (FrameworkElement)panel.Children[1];
		var item3 = (FrameworkElement)panel.Children[2];

		LayoutInformation.GetLayoutSlot(ic).Should().Be(new Rect(5, 5, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(ip).Should().Be(new Rect(0, 0, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(header).Should().Be(new Rect(0, 0, 318, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(footer).Should().Be(new Rect(814, 0, 278, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(panel).Should().Be(new Rect(318, 0, 496, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(item1).Should().Be(new Rect(0, 0, 402, 538), Epsilon);
		LayoutInformation.GetLayoutSlot(item2).Should().Be(new Rect(402, 0, 406, 538), Epsilon);
		LayoutInformation.GetLayoutSlot(item3).Should().Be(new Rect(808, 0, 418, 538), Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Near).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Far).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Center).Should().BeNull();

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Near).ToList().Should();

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Near).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			0,
			318,
			749,
			1155,
			814
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Far).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			318,
			749,
			1155,
			1573,
			1092
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Center).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			159,
			548,
			952,
			1364,
			953
		}, Epsilon);
	}

	[TestMethod]
	[RunsOnUIThread]
	[RequiresFullWindow]
	public async Task When_Big_Elements_Vertical_Many_Margins()
	{
		var grid = (Grid)XamlReader.Load("""
										 <Grid x:Name="g" Width="600" Height="700"
										 	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
										 	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
										 	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
										 	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
										 	mc:Ignorable="d"
										 	>
										 <Border BorderBrush="Bisque" BorderThickness="5">
										      <ItemsControl x:Name="ic">
										          <ItemsControl.Items>
										              <Border Width="400" Height="300" Margin="1">
										                  <Ellipse Fill="Red" />
										              </Border>
										              <Border Width="400" Height="300" Margin="3">
										                  <Ellipse Fill="Green" />
										              </Border>
										              <Border Width="400" Height="300" VerticalAlignment="Bottom"  Margin="9">
										                  <Ellipse Fill="Blue" />
										              </Border>
										          </ItemsControl.Items>
										          <ItemsControl.ItemsPanel>
										              <ItemsPanelTemplate>
										                  <StackPanel Margin="29" Orientation="Vertical" />
										              </ItemsPanelTemplate>
										          </ItemsControl.ItemsPanel>
										          <ItemsControl.Template>
										              <ControlTemplate TargetType="ItemsControl">
										                  <ItemsPresenter Margin="47">
										                      <ItemsPresenter.Header>
										                          <Border Width="200" Height="200" VerticalAlignment="Bottom" Margin="59">
										                              <Ellipse Fill="Yellow" />
										                          </Border>
										                      </ItemsPresenter.Header>
										                      <ItemsPresenter.Footer>
										                          <Border Width="200" Height="200" VerticalAlignment="Top" Margin="39">
										                              <Ellipse Fill="Pink" />
										                          </Border>
										                      </ItemsPresenter.Footer>
										                  </ItemsPresenter>
										              </ControlTemplate>
										          </ItemsControl.Template>
										      </ItemsControl>
										 </Border>
										 </Grid>
										 """);

		WindowHelper.WindowContent = grid;
		await WindowHelper.WaitForLoaded(grid);
		await WindowHelper.WaitForIdle();

		var ic = (ItemsControl)grid.FindName("ic");
		var ip = grid.FindVisualChildByType<ItemsPresenter>();
		var header = (ContentControl)VisualTreeHelper.GetChild(ip, 0);
		var footer = (ContentControl)VisualTreeHelper.GetChild(ip, 2);
		var panel = grid.FindVisualChildByType<StackPanel>();
		var item1 = (FrameworkElement)panel.Children[0];
		var item2 = (FrameworkElement)panel.Children[1];
		var item3 = (FrameworkElement)panel.Children[2];

		LayoutInformation.GetLayoutSlot(ic).Should().Be(new Rect(5, 5, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(ip).Should().Be(new Rect(0, 0, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(header).Should().Be(new Rect(0, 0, 496, 318), Epsilon);
		LayoutInformation.GetLayoutSlot(footer).Should().Be(new Rect(0, 914, 496, 278), Epsilon);
		LayoutInformation.GetLayoutSlot(panel).Should().Be(new Rect(0, 318, 496, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(item1).Should().Be(new Rect(0, 0, 438, 302), Epsilon);
		LayoutInformation.GetLayoutSlot(item2).Should().Be(new Rect(0, 302, 438, 306), Epsilon);
		LayoutInformation.GetLayoutSlot(item3).Should().Be(new Rect(0, 608, 438, 318), Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Near).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Far).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Center).Should().BeNull();

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Near).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			0,
			318,
			649,
			955,
			914
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Far).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			318,
			649,
			955,
			1273,
			1192
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Center).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			159,
			498,
			802,
			1114,
			1053
		}, Epsilon);
	}

	[TestMethod]
	[RunsOnUIThread]
	[RequiresFullWindow]
	public async Task When_Small_Elements_Horizontal_Many_Margins()
	{
		var grid = (Grid)XamlReader.Load("""
										 <Grid x:Name="g" Width="600" Height="700"
										 	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
										 	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
										 	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
										 	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
										 	mc:Ignorable="d"
										 	>
										 <Border BorderBrush="Bisque" BorderThickness="5">
										      <ItemsControl x:Name="ic">
										          <ItemsControl.Items>
										              <Border Width="40" Height="30" Margin="1">
										                  <Ellipse Fill="Red" />
										              </Border>
										              <Border Width="40" Height="30" Margin="3">
										                  <Ellipse Fill="Green" />
										              </Border>
										              <Border Width="40" Height="30" VerticalAlignment="Bottom"  Margin="9">
										                  <Ellipse Fill="Blue" />
										              </Border>
										          </ItemsControl.Items>
										          <ItemsControl.ItemsPanel>
										              <ItemsPanelTemplate>
										                  <StackPanel Margin="29" Orientation="Horizontal" />
										              </ItemsPanelTemplate>
										          </ItemsControl.ItemsPanel>
										          <ItemsControl.Template>
										              <ControlTemplate TargetType="ItemsControl">
										                  <ItemsPresenter Margin="47">
										                      <ItemsPresenter.Header>
										                          <Border Width="20" Height="20" VerticalAlignment="Bottom" Margin="59">
										                              <Ellipse Fill="Yellow" />
										                          </Border>
										                      </ItemsPresenter.Header>
										                      <ItemsPresenter.Footer>
										                          <Border Width="20" Height="20" VerticalAlignment="Top" Margin="39">
										                              <Ellipse Fill="Pink" />
										                          </Border>
										                      </ItemsPresenter.Footer>
										                  </ItemsPresenter>
										              </ControlTemplate>
										          </ItemsControl.Template>
										      </ItemsControl>
										 </Border>
										 </Grid>
										 """);

		WindowHelper.WindowContent = grid;
		await WindowHelper.WaitForLoaded(grid);
		await WindowHelper.WaitForIdle();

		var ic = (ItemsControl)grid.FindName("ic");
		var ip = grid.FindVisualChildByType<ItemsPresenter>();
		var header = (ContentControl)VisualTreeHelper.GetChild(ip, 0);
		var footer = (ContentControl)VisualTreeHelper.GetChild(ip, 2);
		var panel = grid.FindVisualChildByType<StackPanel>();
		var item1 = (FrameworkElement)panel.Children[0];
		var item2 = (FrameworkElement)panel.Children[1];
		var item3 = (FrameworkElement)panel.Children[2];

		LayoutInformation.GetLayoutSlot(ic).Should().Be(new Rect(5, 5, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(ip).Should().Be(new Rect(0, 0, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(header).Should().Be(new Rect(0, 0, 138, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(footer).Should().Be(new Rect(398, 0, 98, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(panel).Should().Be(new Rect(138, 0, 260, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(item1).Should().Be(new Rect(0, 0, 42, 538), Epsilon);
		LayoutInformation.GetLayoutSlot(item2).Should().Be(new Rect(42, 0, 46, 538), Epsilon);
		LayoutInformation.GetLayoutSlot(item3).Should().Be(new Rect(88, 0, 58, 538), Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Near).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Far).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Center).Should().BeNull();

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Near).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			0,
			138,
			209,
			255,
			342
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Far).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			138,
			209,
			255,
			313,
			440
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Center).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			69,
			188,
			232,
			284,
			391
		}, Epsilon);
	}

	[TestMethod]
	[RunsOnUIThread]
	[RequiresFullWindow]
	public async Task When_Small_Elements_Vertical_Many_Margins()
	{
		var grid = (Grid)XamlReader.Load("""
										 <Grid x:Name="g" Width="600" Height="700"
										 	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
										 	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
										 	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
										 	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
										 	mc:Ignorable="d"
										 	>
										 <Border BorderBrush="Bisque" BorderThickness="5">
										      <ItemsControl x:Name="ic">
										          <ItemsControl.Items>
										              <Border Width="40" Height="30" Margin="1">
										                  <Ellipse Fill="Red" />
										              </Border>
										              <Border Width="40" Height="30" Margin="3">
										                  <Ellipse Fill="Green" />
										              </Border>
										              <Border Width="40" Height="30" VerticalAlignment="Bottom"  Margin="9">
										                  <Ellipse Fill="Blue" />
										              </Border>
										          </ItemsControl.Items>
										          <ItemsControl.ItemsPanel>
										              <ItemsPanelTemplate>
										                  <StackPanel Margin="29" Orientation="Vertical" />
										              </ItemsPanelTemplate>
										          </ItemsControl.ItemsPanel>
										          <ItemsControl.Template>
										              <ControlTemplate TargetType="ItemsControl">
										                  <ItemsPresenter Margin="47">
										                      <ItemsPresenter.Header>
										                          <Border Width="20" Height="20" VerticalAlignment="Bottom" Margin="59">
										                              <Ellipse Fill="Yellow" />
										                          </Border>
										                      </ItemsPresenter.Header>
										                      <ItemsPresenter.Footer>
										                          <Border Width="20" Height="20" VerticalAlignment="Top" Margin="39">
										                              <Ellipse Fill="Pink" />
										                          </Border>
										                      </ItemsPresenter.Footer>
										                  </ItemsPresenter>
										              </ControlTemplate>
										          </ItemsControl.Template>
										      </ItemsControl>
										 </Border>
										 </Grid>
										 """);

		WindowHelper.WindowContent = grid;
		await WindowHelper.WaitForLoaded(grid);
		await WindowHelper.WaitForIdle();

		var ic = (ItemsControl)grid.FindName("ic");
		var ip = grid.FindVisualChildByType<ItemsPresenter>();
		var header = (ContentControl)VisualTreeHelper.GetChild(ip, 0);
		var footer = (ContentControl)VisualTreeHelper.GetChild(ip, 2);
		var panel = grid.FindVisualChildByType<StackPanel>();
		var item1 = (FrameworkElement)panel.Children[0];
		var item2 = (FrameworkElement)panel.Children[1];
		var item3 = (FrameworkElement)panel.Children[2];

		LayoutInformation.GetLayoutSlot(ic).Should().Be(new Rect(5, 5, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(ip).Should().Be(new Rect(0, 0, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(header).Should().Be(new Rect(0, 0, 496, 138), Epsilon);
		LayoutInformation.GetLayoutSlot(footer).Should().Be(new Rect(0, 498, 496, 98), Epsilon);
		LayoutInformation.GetLayoutSlot(panel).Should().Be(new Rect(0, 138, 496, 360), Epsilon);
		LayoutInformation.GetLayoutSlot(item1).Should().Be(new Rect(0, 0, 438, 32), Epsilon);
		LayoutInformation.GetLayoutSlot(item2).Should().Be(new Rect(0, 32, 438, 36), Epsilon);
		LayoutInformation.GetLayoutSlot(item3).Should().Be(new Rect(0, 68, 438, 48), Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Near).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Far).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Center).Should().BeNull();

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Near).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			0,
			138,
			199,
			235,
			312
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Far).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			138,
			199,
			235,
			283,
			410
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Center).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			69,
			183,
			217,
			259,
			361
		}, Epsilon);
	}

	[TestMethod]
	[RunsOnUIThread]
	[RequiresFullWindow]
	public async Task When_Small_Elements_Horizontal_Many_Margins_No_Footer()
	{
		var grid = (Grid)XamlReader.Load("""
										 <Grid x:Name="g" Width="600" Height="700"
										 	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
										 	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
										 	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
										 	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
										 	mc:Ignorable="d"
										 	>
										 <Border BorderBrush="Bisque" BorderThickness="5">
										      <ItemsControl x:Name="ic">
										          <ItemsControl.Items>
										              <Border Width="40" Height="30" Margin="1">
										                  <Ellipse Fill="Red" />
										              </Border>
										              <Border Width="40" Height="30" Margin="3">
										                  <Ellipse Fill="Green" />
										              </Border>
										              <Border Width="40" Height="30" VerticalAlignment="Bottom"  Margin="9">
										                  <Ellipse Fill="Blue" />
										              </Border>
										          </ItemsControl.Items>
										          <ItemsControl.ItemsPanel>
										              <ItemsPanelTemplate>
										                  <StackPanel Margin="29" Orientation="Horizontal" />
										              </ItemsPanelTemplate>
										          </ItemsControl.ItemsPanel>
										          <ItemsControl.Template>
										              <ControlTemplate TargetType="ItemsControl">
										                  <ItemsPresenter Margin="47">
										                      <ItemsPresenter.Header>
										                          <Border Width="20" Height="20" VerticalAlignment="Bottom" Margin="59">
										                              <Ellipse Fill="Yellow" />
										                          </Border>
										                      </ItemsPresenter.Header>
										                  </ItemsPresenter>
										              </ControlTemplate>
										          </ItemsControl.Template>
										      </ItemsControl>
										 </Border>
										 </Grid>
										 """);

		WindowHelper.WindowContent = grid;
		await WindowHelper.WaitForLoaded(grid);
		await WindowHelper.WaitForIdle();

		var ic = (ItemsControl)grid.FindName("ic");
		var ip = grid.FindVisualChildByType<ItemsPresenter>();
		var header = (ContentControl)VisualTreeHelper.GetChild(ip, 0);
		var footer = (ContentControl)VisualTreeHelper.GetChild(ip, 2);
		var panel = grid.FindVisualChildByType<StackPanel>();
		var item1 = (FrameworkElement)panel.Children[0];
		var item2 = (FrameworkElement)panel.Children[1];
		var item3 = (FrameworkElement)panel.Children[2];

		LayoutInformation.GetLayoutSlot(ic).Should().Be(new Rect(5, 5, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(ip).Should().Be(new Rect(0, 0, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(header).Should().Be(new Rect(0, 0, 138, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(footer).Should().Be(new Rect(496, 0, 0, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(panel).Should().Be(new Rect(138, 0, 358, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(item1).Should().Be(new Rect(0, 0, 42, 538), Epsilon);
		LayoutInformation.GetLayoutSlot(item2).Should().Be(new Rect(42, 0, 46, 538), Epsilon);
		LayoutInformation.GetLayoutSlot(item3).Should().Be(new Rect(88, 0, 58, 538), Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Near).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Far).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Center).Should().BeNull();

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Near).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			0,
			138,
			209,
			255
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Far).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			138,
			209,
			255,
			313
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Center).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			69,
			188,
			232,
			284
		}, Epsilon);
	}

	[TestMethod]
	[RunsOnUIThread]
	[RequiresFullWindow]
	public async Task When_Small_Elements_Horizontal_Many_Margins_No_Header()
	{
		var grid = (Grid)XamlReader.Load("""
										 <Grid x:Name="g" Width="600" Height="700"
										 	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
										 	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
										 	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
										 	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
										 	mc:Ignorable="d"
										 	>
										 <Border BorderBrush="Bisque" BorderThickness="5">
										      <ItemsControl x:Name="ic">
										          <ItemsControl.Items>
										              <Border Width="40" Height="30" Margin="1">
										                  <Ellipse Fill="Red" />
										              </Border>
										              <Border Width="40" Height="30" Margin="3">
										                  <Ellipse Fill="Green" />
										              </Border>
										              <Border Width="40" Height="30" VerticalAlignment="Bottom"  Margin="9">
										                  <Ellipse Fill="Blue" />
										              </Border>
										          </ItemsControl.Items>
										          <ItemsControl.ItemsPanel>
										              <ItemsPanelTemplate>
										                  <StackPanel Margin="29" Orientation="Horizontal" />
										              </ItemsPanelTemplate>
										          </ItemsControl.ItemsPanel>
										          <ItemsControl.Template>
										              <ControlTemplate TargetType="ItemsControl">
										                  <ItemsPresenter Margin="47">
										                      <ItemsPresenter.Footer>
										                          <Border Width="20" Height="20" VerticalAlignment="Top" Margin="39">
										                              <Ellipse Fill="Pink" />
										                          </Border>
										                      </ItemsPresenter.Footer>
										                  </ItemsPresenter>
										              </ControlTemplate>
										          </ItemsControl.Template>
										      </ItemsControl>
										 </Border>
										 </Grid>
										 """);

		WindowHelper.WindowContent = grid;
		await WindowHelper.WaitForLoaded(grid);
		await WindowHelper.WaitForIdle();

		var ic = (ItemsControl)grid.FindName("ic");
		var ip = grid.FindVisualChildByType<ItemsPresenter>();
		var header = (ContentControl)VisualTreeHelper.GetChild(ip, 0);
		var footer = (ContentControl)VisualTreeHelper.GetChild(ip, 2);
		var panel = grid.FindVisualChildByType<StackPanel>();
		var item1 = (FrameworkElement)panel.Children[0];
		var item2 = (FrameworkElement)panel.Children[1];
		var item3 = (FrameworkElement)panel.Children[2];

		LayoutInformation.GetLayoutSlot(ic).Should().Be(new Rect(5, 5, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(ip).Should().Be(new Rect(0, 0, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(header).Should().Be(new Rect(0, 0, 0, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(footer).Should().Be(new Rect(398, 0, 98, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(panel).Should().Be(new Rect(0, 0, 398, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(item1).Should().Be(new Rect(0, 0, 42, 538), Epsilon);
		LayoutInformation.GetLayoutSlot(item2).Should().Be(new Rect(42, 0, 46, 538), Epsilon);
		LayoutInformation.GetLayoutSlot(item3).Should().Be(new Rect(88, 0, 58, 538), Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Near).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Far).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Center).Should().BeNull();

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Near).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			0,
			71,
			117,
			204
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Far).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			71,
			117,
			175,
			302
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Center).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			50,
			94,
			146,
			253
		}, Epsilon);
	}

	[TestMethod]
	[RunsOnUIThread]
	[RequiresFullWindow]
	public async Task When_Small_Elements_Horizontal_Many_Margins_No_Items()
	{
		var grid = (Grid)XamlReader.Load("""
										 <Grid x:Name="g" Width="600" Height="700"
										 	xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
										 	xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
										 	xmlns:d="http://schemas.microsoft.com/expression/blend/2008"
										 	xmlns:mc="http://schemas.openxmlformats.org/markup-compatibility/2006"
										 	mc:Ignorable="d"
										 	>
										 <Border BorderBrush="Bisque" BorderThickness="5">
										      <ItemsControl x:Name="ic">
										          <ItemsControl.ItemsPanel>
										              <ItemsPanelTemplate>
										                  <StackPanel Margin="29" Orientation="Horizontal" />
										              </ItemsPanelTemplate>
										          </ItemsControl.ItemsPanel>
										          <ItemsControl.Template>
										              <ControlTemplate TargetType="ItemsControl">
										                  <ItemsPresenter Margin="47">
										                      <ItemsPresenter.Header>
										                          <Border Width="20" Height="20" VerticalAlignment="Bottom" Margin="59">
										                              <Ellipse Fill="Yellow" />
										                          </Border>
										                      </ItemsPresenter.Header>
										                      <ItemsPresenter.Footer>
										                          <Border Width="20" Height="20" VerticalAlignment="Top" Margin="39">
										                              <Ellipse Fill="Pink" />
										                          </Border>
										                      </ItemsPresenter.Footer>
										                  </ItemsPresenter>
										              </ControlTemplate>
										          </ItemsControl.Template>
										      </ItemsControl>
										 </Border>
										 </Grid>
										 """);

		WindowHelper.WindowContent = grid;
		await WindowHelper.WaitForLoaded(grid);
		await WindowHelper.WaitForIdle();

		var ic = (ItemsControl)grid.FindName("ic");
		var ip = grid.FindVisualChildByType<ItemsPresenter>();
		var header = (ContentControl)VisualTreeHelper.GetChild(ip, 0);
		var footer = (ContentControl)VisualTreeHelper.GetChild(ip, 2);
		var panel = grid.FindVisualChildByType<StackPanel>();

		LayoutInformation.GetLayoutSlot(ic).Should().Be(new Rect(5, 5, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(ip).Should().Be(new Rect(0, 0, 590, 690), Epsilon);
		LayoutInformation.GetLayoutSlot(header).Should().Be(new Rect(0, 0, 138, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(footer).Should().Be(new Rect(398, 0, 98, 596), Epsilon);
		LayoutInformation.GetLayoutSlot(panel).Should().Be(new Rect(138, 0, 260, 596), Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Near).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Far).Should().BeNull();
		ip.GetIrregularSnapPoints(Orientation.Vertical, SnapPointsAlignment.Center).Should().BeNull();

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Near).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			0,
			196
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Far).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			138,
			294
		}, Epsilon);

		ip.GetIrregularSnapPoints(Orientation.Horizontal, SnapPointsAlignment.Center).ToList().Should().BeEquivalentToWithTolerance<float>(new float[]
		{
			69,
			245
		}, Epsilon);
	}
}
