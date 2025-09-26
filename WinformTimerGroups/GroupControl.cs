using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinformTimerGroups
{
    public class GroupButton : RadioButton
    {
        public Group Data { get; private set; }
        public event Action<Group> GroupClicked;

        public GroupButton(Group group, ButtonStyle style)
        {
            this.Data = group;
            this.Text = group.Name;
            this.Width = style.Width;
            this.Height = style.Height;
            this.Font = new Font(style.FontName, style.FontSize);
            this.Margin = new Padding(5);
            this.Appearance = Appearance.Button;
            this.TextAlign = ContentAlignment.MiddleCenter;

            this.CheckedChanged += GroupButton_CheckedChanged;
        }

        private void GroupButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Checked)
            {
                GroupClicked?.Invoke(Data);
            }
        }
    }
}
