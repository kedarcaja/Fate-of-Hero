using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GUIStylizer 
{
    public static class Colors
    {
        public static Color RED { get => Color.red; }
        public static Color GREEN { get => Color.green; }
        public static Color BLUE { get => Color.blue; }
        public static Color MAGENTA { get => Color.magenta; }
        public static Color GREY { get => Color.grey; }
        public static Color BLACK { get => Color.black; }
        public static Color CYAN { get => Color.cyan; }
        public static Color WHITE { get => Color.white; }
        public static Color YELLOW { get => Color.yellow; }
        public static Color ORANGE { get => new Color32(255, 140, 0, 255); }
        public static Color LIGHTSKYBLUE { get => new Color32(0, 233, 255, 255); }
        public static Color PURPLE { get => new Color32(119, 0, 255, 255); }
        public static Color REDPING { get => new Color32(242, 26, 91, 255); }



    }

 
    public static GUIStyle GetStyle(Color color,TextAnchor pos,int fontSize)
    {
        GUIStyle s = new GUIStyle();
        s.fontSize = fontSize;
       
        s.alignment = pos;
        s.normal.textColor = color;
        return s;
    }
    public static GUIStyle GetStyle(Color color, TextAnchor pos, int fontSize,int marginTop, int marginBottom, int marginLeft, int marginRight)
    {
        GUIStyle s = new GUIStyle();
        s.fontSize = fontSize;
        s.margin = new RectOffset(marginLeft,marginRight,marginTop,marginLeft);
        s.alignment = pos;
        s.normal.textColor = color;
        return s;
    }
    public static GUIStyle GetStyle(Color color, TextAnchor pos, int fontSize, int marginLeft, int marginRight, int marginTop, int marginBottom, int paddingLeft, int paddingRight, int paddingTop, int paddingBottom)
    {
        GUIStyle s = new GUIStyle();
        s.fontSize = fontSize;
        s.margin = new RectOffset(marginLeft, marginRight, marginTop, marginBottom);
        s.padding = new RectOffset(paddingLeft, paddingRight, paddingTop, paddingBottom);
        s.alignment = pos;
        s.normal.textColor = color;
        return s;
    }
    public static GUIStyle GetStyle(Color color, TextAnchor pos, int fontSize, int paddingLeft, int paddingRight, int paddingTop, float paddingBottom)
    {
        GUIStyle s = new GUIStyle();
        s.fontSize = fontSize;
        s.padding = new RectOffset(paddingLeft, paddingRight, paddingTop, (int)paddingBottom);
        s.alignment = pos;
        s.normal.textColor = color;
       
        return s;
    }
}
