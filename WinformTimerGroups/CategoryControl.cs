using System;
using System.Drawing;
using System.Windows.Forms;

namespace WinformTimerGroups
{
    public class CategoryButton : RadioButton
    {
        public Category Data { get; private set; }
        public event Action<Category> CategoryClicked;

        public CategoryButton(Category category, ButtonStyle style)
        {
            this.Data = category;
            this.Text = category.Name;
            this.Width = style.Width;
            this.Height = style.Height;
            this.Font = new Font(style.FontName, style.FontSize);
            this.Margin = new Padding(5);
            this.Appearance = Appearance.Button;
            this.TextAlign = ContentAlignment.MiddleCenter;

            this.CheckedChanged += CategoryButton_CheckedChanged;
        }

        private void CategoryButton_CheckedChanged(object sender, EventArgs e)
        {
            if (this.Checked)
            {
                CategoryClicked?.Invoke(Data);
            }
        }
    }
}
