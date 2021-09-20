using System.Collections.Generic;
using System;
using System.Linq;

namespace Game.UI
{
    public class RadioGroup : GridLayout, IUIElement
    {
        public Theme Theme { get; }
        public List<Button> Buttons { get; } = new List<Button>();
        private int selectedIndex;
        public int SelectedIndex 
        { 
            get
            {
                return selectedIndex;
            } 
            set
            {
                selectedIndex = value;
                OnNewSelectionActions.ForEach(action => action(selectedIndex));
                Draw();
            }
        }
        public List<Action<int>> OnNewSelectionActions { get; }
        
        public RadioGroup(int width, int height, List<ColoredString> items, Theme theme = null, params Action<int>[] onNewSelectionActions) : base(width, height, 1, items.Count)
        {
            Theme = theme ?? Theme.CurrentTheme;
            OnNewSelectionActions = new List<Action<int>>(onNewSelectionActions);

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
                var button = Add((width, height) => new GravityLayout(width, height), 0, y).Add((width, height) => new Button(item, () => {
                    SelectedIndex = index;
                }, width, height), 2, true, LayoutGravity.CENTER);
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

            var button = Buttons[SelectedIndex];
            var y = button.Parent.Position.Y;
            var x = button.Text.Length + 1;
            Print(0, y, "<", Theme.MainColor);
            Print(x, y, ">", Theme.MainColor);
        }
    }
}