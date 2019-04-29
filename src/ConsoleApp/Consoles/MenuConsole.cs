using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Input;
using System.Linq;
using SadConsole.Controls;
using SadConsole.Themes;

namespace ConsoleApp.Consoles
{
    internal class MenuConsole : ControlsConsole
    {
        #region Properties

        Color[] backgroundcycle;
        int backIndex = 0;
        SadConsole.Timer progressTimer;

        #endregion Properties

        #region Construction

        public MenuConsole() : base(80, 23)
        {
            var prog1 = new ProgressBar(10, 1, HorizontalAlignment.Left);
            prog1.Position = new Point(16, 5);
            Add(prog1);

            var prog2 = new ProgressBar(1, 6, VerticalAlignment.Bottom);
            prog2.Position = new Point(18, 7);
            Add(prog2);

            progressTimer = new Timer(0.5, (timer, time) => { prog1.Progress = prog1.Progress >= 1f ? 0f : prog1.Progress + 0.1f; prog2.Progress = prog2.Progress >= 1f ? 0f : prog2.Progress + 0.1f; });

            var selButton = new SadConsole.Controls.SelectionButton(24, 1);
            selButton.Text = "Selection Button 1";
            selButton.Position = new Point(51, 3);
            Add(selButton);

            var selButton1 = new SadConsole.Controls.SelectionButton(24, 1);
            selButton1.Text = "Selection Button 2";
            selButton1.Position = new Point(51, 4);
            Add(selButton1);

            var selButton2 = new SadConsole.Controls.SelectionButton(24, 1);
            selButton2.Text = "Selection Button 3";
            selButton2.Position = new Point(51, 5);
            Add(selButton2);

            selButton.PreviousSelection = selButton2;
            selButton.NextSelection = selButton1;
            selButton1.PreviousSelection = selButton;
            selButton1.NextSelection = selButton2;
            selButton2.PreviousSelection = selButton1;
            selButton2.NextSelection = selButton;


            var button = new SadConsole.Controls.Button(11, 3)
            {
                Text = "Click",
                Position = new Point(1, 10),
                Theme = new ButtonLinesTheme()
            };
            Add(button);

            var checkbox = new SadConsole.Controls.CheckBox(13, 1)
            {
                Text = "Check box",
                Position = new Point(51, 13)
            };
            Add(checkbox);

            FocusedControl = null;
            //DisableControlFocusing = true;

            List<Tuple<Color, string>> colors = new List<Tuple<Color, string>>();
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.Red, "Red"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.RedDark, "DRed"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.Purple, "Prp"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.PurpleDark, "DPrp"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.Blue, "Blu"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.BlueDark, "DBlu"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.Cyan, "Cya"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.CyanDark, "DCya"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.Green, "Gre"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.GreenDark, "DGre"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.Yellow, "Yel"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.YellowDark, "DYel"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.Orange, "Ora"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.OrangeDark, "DOra"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.Brown, "Bro"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.BrownDark, "DBrow"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.Gray, "Gray"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.GrayDark, "DGray"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.White, "White"));
            colors.Add(new Tuple<Color, string>(Library.Default.Colors.Black, "Black"));

            backgroundcycle = colors.Select(i => i.Item1).ToArray();
            backIndex = 5;
        }

        #endregion Construction

        #region Methods

        #region ProcessKeyboard
        public override bool ProcessKeyboard(SadConsole.Input.Keyboard info)
        {
            if (info.IsKeyReleased(Keys.C))
            {
                backIndex++;

                if (backIndex == backgroundcycle.Length)
                    backIndex = 0;

                var theme = Theme.Clone();
                theme.Colors.ControlBack = backgroundcycle[backIndex];
                theme.Colors.RebuildAppearances();
                Theme = theme;
            }

            return base.ProcessKeyboard(info);
        }
        #endregion ProcessKeyboard

        #region ProcessMouse
        public override bool ProcessMouse(SadConsole.Input.MouseConsoleState state)
        {
            return base.ProcessMouse(state);
        }
        #endregion ProcessMouse

        #region Update
        public override void Update(TimeSpan time)
        {
            progressTimer.Update(time.TotalSeconds);
            base.Update(time);
        }
        #endregion Update

        #endregion Methods
    }
}
