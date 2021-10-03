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
        public EnumerableContainer<string, List<string>> Items = new EnumerableContainer<string, List<string>>();
        
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

            Items.StateChangeEvent += (obj, args) => AddButtons(args.Current);
            Items.Set(items);
        }

        public void AddButtons(List<string> items)
        {
            Selection.Set(0);

            Buttons.ForEach(button => {
                button.Clear();
                button.IsVisible = false;
                button.Parent = null;
            });
            Buttons.Clear();

            var y = 0;
            foreach (var item in items)
            {
                var index = y;
                var button = Add((width, height) => new GravityLayout(width, height), 0, y).Add((width, height) => new Button(ColoredString.From(item), () => {
                    Selection.Set(index);
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

            if(Selection >= Buttons.Count)
            {
                return;
            }

            var button = Buttons[Selection];
            var y = button.Parent.Position.Y;
            var x = ColoredString.GetLength(button.Text) + 1;
            Print(0, y, "<", Theme.MainColor);
            Print(x, y, ">", Theme.MainColor);
        }
    }
}