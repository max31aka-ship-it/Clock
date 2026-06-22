// AnalogClock.cs - Аналоговые часы на C# (WinForms)
using System;
using System.Drawing;
using System.Windows.Forms;

public class AnalogClock : Form
{
    private Timer timer;
    private int radius = 180;
    private Point center;
    private Color faceColor = Color.FromArgb(245,245,220);
    private Color handColor = Color.DarkGray;
    private Color secondColor = Color.Red;
    private bool useRoman = false;

    public AnalogClock()
    {
        Text = "🕐 ClockForge - C#";
        Size = new Size(450, 450);
        StartPosition = FormStartPosition.CenterScreen;
        DoubleBuffered = true;
        timer = new Timer();
        timer.Interval = 1000;
        timer.Tick += (s, e) => Invalidate();
        timer.Start();
        MouseClick += (s, e) => {
            faceColor = faceColor == Color.White ? Color.FromArgb(245,245,220) : Color.White;
            Invalidate();
        };
        // Кнопки управления
        Button bgBtn = new Button { Text = "Цвет фона", Location = new Point(20, 380), Size = new Size(100, 30) };
        bgBtn.Click += (s, e) => {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK) faceColor = cd.Color;
        };
        Controls.Add(bgBtn);
        Button handBtn = new Button { Text = "Цвет стрелок", Location = new Point(130, 380), Size = new Size(100, 30) };
        handBtn.Click += (s, e) => {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK) handColor = cd.Color;
        };
        Controls.Add(handBtn);
        Button romanBtn = new Button { Text = "Римские", Location = new Point(240, 380), Size = new Size(100, 30) };
        romanBtn.Click += (s, e) => { useRoman = !useRoman; Invalidate(); };
        Controls.Add(romanBtn);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);
        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        center = new Point(ClientSize.Width/2, ClientSize.Height/2 - 20);
        int cx = center.X, cy = center.Y;

        // Циферблат
        g.FillEllipse(new SolidBrush(faceColor), cx - radius, cy - radius, radius*2, radius*2);
        g.DrawEllipse(Pens.Black, cx - radius, cy - radius, radius*2, radius*2);

        DateTime now = DateTime.Now;
        int hours = now.Hour % 12;
        int minutes = now.Minute;
        int seconds = now.Second;

        for (int i = 0; i < 12; i++)
        {
            double angle = (i * 30 - 90) * Math.PI / 180;
            int x1 = cx + (int)((radius - 20) * Math.Cos(angle));
            int y1 = cy + (int)((radius - 20) * Math.Sin(angle));
            int x2 = cx + (int)((radius - 5) * Math.Cos(angle));
            int y2 = cy + (int)((radius - 5) * Math.Sin(angle));
            g.DrawLine(Pens.Black, x1, y1, x2, y2);
            string label;
            if (useRoman)
            {
                string[] roman = {"XII","I","II","III","IV","V","VI","VII","VIII","IX","X","XI"};
                label = roman[i];
            }
            else
            {
                label = (i+1) == 12 ? "12" : (i+1).ToString();
            }
            int tx = cx + (int)((radius - 40) * Math.Cos(angle));
            int ty = cy + (int)((radius - 40) * Math.Sin(angle));
            g.DrawString(label, new Font("Arial", 14, FontStyle.Bold), Brushes.Black, tx - 8, ty - 8);
        }

        // Стрелки
        DrawHand(g, (hours + minutes/60.0) * 30 - 90, radius*0.5, handColor, 6);
        DrawHand(g, (minutes + seconds/60.0) * 6 - 90, radius*0.7, handColor, 4);
        DrawHand(g, seconds * 6 - 90, radius*0.8, secondColor, 2);

        g.FillEllipse(Brushes.Black, cx - 8, cy - 8, 16, 16);
    }

    private void DrawHand(Graphics g, double angleDeg, double length, Color color, int width)
    {
        double angle = angleDeg * Math.PI / 180;
        int cx = center.X, cy = center.Y;
        int x = cx + (int)(length * Math.Cos(angle));
        int y = cy + (int)(length * Math.Sin(angle));
        using (Pen pen = new Pen(color, width)) { pen.StartCap = pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;
            g.DrawLine(pen, cx, cy, x, y); }
    }

    [STAThread]
    static void Main() { Application.EnableVisualStyles(); Application.Run(new AnalogClock()); }
}
