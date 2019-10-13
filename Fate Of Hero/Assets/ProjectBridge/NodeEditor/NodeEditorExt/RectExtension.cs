using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RectExtensions
{
    public static Rect GetResizedRect(this Rect rect)
    {
        Vector2 position = GUI.matrix.MultiplyVector(new Vector2(rect.x, rect.y));
        Vector2 size = GUI.matrix.MultiplyVector(new Vector2(rect.width, rect.height));

        return new Rect(position.x, position.y, size.x, size.y);
    }
    public static Vector2 TopLeft(this Rect rect)
	{
		return new Vector2(rect.xMin, rect.yMin);
	}
	public static Rect ScaleSizeBy(this Rect rect, float scale)
	{
		return rect.ScaleSizeBy(scale, rect.center);
	}
	public static Rect ScaleSizeBy(this Rect rect, float scale, Vector2 pivotPoint)
	{
		Rect result = rect;
		result.x -= pivotPoint.x;
		result.y -= pivotPoint.y;
		result.xMin *= scale;
		result.xMax *= scale;
		result.yMin *= scale;
		result.yMax *= scale;
		result.x += pivotPoint.x;
		result.y += pivotPoint.y;
		return result;
	}
	public static Rect ScaleSizeBy(this Rect rect, Vector2 scale)
	{
		return rect.ScaleSizeBy(scale, rect.center);
	}
	public static Rect ScaleSizeBy(this Rect rect, Vector2 scale, Vector2 pivotPoint)
	{
		Rect result = rect;
		result.x -= pivotPoint.x;
		result.y -= pivotPoint.y;
		result.xMin *= scale.x;
		result.xMax *= scale.x;
		result.yMin *= scale.y;
		result.yMax *= scale.y;
		result.x += pivotPoint.x;
		result.y += pivotPoint.y;
		return result;
	}
}
