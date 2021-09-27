using System.Collections.Generic;
using System;
using System.Linq;
using Game.Util;

namespace Game.UI
{
    public class RadioGroup : GridLayout, IUIElement
    {
        public Theme Theme { get; }
        public List<Button> Buttons { get; } = new List<Button>();
        public VariableContainer<int> Selection { get; } = new VariableContainer<int>{ State = 0 };
        
        public RadioGroup(int width, int height, List<string> items, Theme theme = null, params VariableContainer<int>.StateChangeHandler[] selectionHandlers) : base(width, height, 1, items.Count)
        {
            Theme = theme ?? Theme.CurrentTheme;

            foreach (var handler in selectionHandlers)
            {
                Selection.StateChangeEvent += handler;
            }
            Selection.StateChangeEvent += (obj, args) => Draw();

            foreach (var segment in YSegments)
            {
                segment.Length = 1;
                segment.IsDynamic = false;
            }
            CalculateDimensions();

            var y = 0;
            foreach (var item in items)
            {
                var index = y;
                var button = Add((width, height) => new GravityLayout(width, height), 0, y).Add((width, height) => new Button(ColoredString.From(item), () => {
                    Selection.State = index;
                }), 2, true, LayoutGravity.CENTER);
                Buttons.Add(button);
                button.Draw();

                y++;
            }

            if(Buttons.Count > 0) 
            {
                Draw();
            }
        }

        public void Draw()
        {
            Clear();

            var button = Buttons[Selection];
            var y = button.Parent.Position.Y;
            var x = ColoredString.GetLength(button.Text) + 1;
            Print(0, y, "<", Theme.MainColor);
            Print(x, y, ">", Theme.MainColor);
        }
    }
}