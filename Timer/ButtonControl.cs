using System;
using System.Drawing;
using System.Windows.Forms;

namespace TimerUtility
{
    public class ButtonControl<T> : RadioButton
    {
        public T Data { get; set; }

        public Action<T> ButtonClicked { get; set; }

        public ButtonControl(T data, string text)
        {
            Data = data;
            Text = text;

            Appearance = Appearance.Button;
            Font = new Font("SimHei", 11.25F);
            Margin = new Padding(1);
            Size = new Size(93, 32);
            TextAlign = ContentAlignment.MiddleCenter;

            Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            ButtonClicked?.Invoke(Data);
        }
    }
}