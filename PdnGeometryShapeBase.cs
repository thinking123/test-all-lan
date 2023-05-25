// paintdotnet, Version=5.1.8421.12454, Culture=neutral, PublicKeyToken=null
// PaintDotNet.Shapes.PdnGeometryShapeBase
using System.Collections.Generic;
using PaintDotNet;
using PaintDotNet.Collections;
using PaintDotNet.Rendering;
using PaintDotNet.Settings.App;
using PaintDotNet.Shapes;
using PaintDotNet.UI.Media;

internal abstract class PdnGeometryShapeBase : PdnShapeBase
{
	protected PdnGeometryShapeBase(string displayName, ShapeCategory category)
		: base(displayName, category)
	{
	}

	protected override IEnumerable<string> OnGetRenderSettingPaths()
	{
		return base.OnGetRenderSettingPaths().Concat(ToolSettings.Null.Brush.Size.Path, ToolSettings.Null.Pen.DashStyle.Path, ToolSettings.Null.Shapes.DrawType.Path);
	}

	protected sealed override ShapeRenderData OnCreateRenderData(ShapeRenderParameters renderParams)
	{
		ShapeDrawType num = (ShapeDrawType)renderParams.SettingValues[ToolSettings.Null.Shapes.DrawType.Path];
		RectDouble bounds = RectDouble.FromCorners(renderParams.StartPoint, renderParams.EndPoint);
		Geometry guideGeometry = OnCreateGuideGeometry(bounds, renderParams.SettingValues);
		Geometry interiorFillGeometry = null;
		if ((num & ShapeDrawType.Interior) == ShapeDrawType.Interior)
		{
			interiorFillGeometry = OnCreateInteriorFillGeometry(bounds, renderParams.SettingValues);
		}
		Geometry outlineDrawGeometry = null;
		Geometry outlineFillGeometry = null;
		if ((num & ShapeDrawType.Outline) == ShapeDrawType.Outline)
		{
			outlineDrawGeometry = OnCreateOutlineDrawGeometry(bounds, renderParams.SettingValues);
			outlineFillGeometry = OnCreateOutlineFillGeometry(bounds, renderParams.SettingValues);
		}
		return new ShapeRenderData(guideGeometry, interiorFillGeometry, outlineDrawGeometry, outlineFillGeometry);
	}

	protected abstract Geometry OnCreateGuideGeometry(RectDouble bounds, IDictionary<string, object> settingValues);

	protected virtual Geometry OnCreateInteriorFillGeometry(RectDouble bounds, IDictionary<string, object> settingValues)
	{
		return OnCreateGuideGeometry(bounds, settingValues);
	}

	protected virtual Geometry OnCreateOutlineDrawGeometry(RectDouble bounds, IDictionary<string, object> settingValues)
	{
		return OnCreateGuideGeometry(bounds, settingValues);
	}

	protected virtual Geometry OnCreateOutlineFillGeometry(RectDouble bounds, IDictionary<string, object> settingValues)
	{
		return null;
	}

	protected sealed override ShapeRenderData OnCreateImageRenderData(ShapeRenderParameters renderParams)
	{
		RectDouble bounds = RectDouble.FromCorners(renderParams.StartPoint, renderParams.EndPoint);
		return new ShapeRenderData(OnCreateImageGeometry(bounds, renderParams.SettingValues));
	}

	protected virtual Geometry OnCreateImageGeometry(RectDouble bounds, IDictionary<string, object> settingValues)
	{
		return OnCreateGuideGeometry(bounds, settingValues);
	}
}
