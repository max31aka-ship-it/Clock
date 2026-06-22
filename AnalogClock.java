// AnalogClock.java - Аналоговые часы на Java (Swing)
import javax.swing.*;
import java.awt.*;
import java.awt.event.*;
import java.time.LocalTime;

public class AnalogClock extends JPanel implements ActionListener {
    private Timer timer;
    private int radius = 180;
    private Point center;
    private Color faceColor = new Color(245, 245, 220);
    private Color handColor = Color.DARK_GRAY;
    private Color secondColor = Color.RED;
    private boolean useRoman = false;

    public AnalogClock() {
        setPreferredSize(new Dimension(400, 400));
        setBackground(faceColor);
        timer = new Timer(1000, this);
        timer.start();
        addMouseListener(new MouseAdapter() {
            public void mouseClicked(MouseEvent e) {
                // переключение цвета при клике
                faceColor = (faceColor == Color.WHITE) ? new Color(245,245,220) : Color.WHITE;
                repaint();
            }
        });
        // JFrame
        JFrame frame = new JFrame("🕐 ClockForge - Java");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        frame.setResizable(false);
        frame.add(this);
        frame.pack();
        frame.setLocationRelativeTo(null);
        frame.setVisible(true);
    }

    @Override
    protected void paintComponent(Graphics g) {
        super.paintComponent(g);
        Graphics2D g2 = (Graphics2D) g;
        g2.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
        center = new Point(getWidth()/2, getHeight()/2);
        int cx = center.x, cy = center.y;

        // Циферблат
        g2.setColor(faceColor);
        g2.fillOval(cx - radius, cy - radius, radius*2, radius*2);
        g2.setColor(Color.BLACK);
        g2.drawOval(cx - radius, cy - radius, radius*2, radius*2);

        // Метки
        LocalTime now = LocalTime.now();
        int hours = now.getHour() % 12;
        int minutes = now.getMinute();
        int seconds = now.getSecond();

        for (int i = 0; i < 12; i++) {
            double angle = Math.toRadians(i * 30 - 90);
            int x1 = cx + (int)((radius - 20) * Math.cos(angle));
            int y1 = cy + (int)((radius - 20) * Math.sin(angle));
            int x2 = cx + (int)((radius - 5) * Math.cos(angle));
            int y2 = cy + (int)((radius - 5) * Math.sin(angle));
            g2.drawLine(x1, y1, x2, y2);
            // Цифры
            String label;
            if (useRoman) {
                String[] roman = {"XII","I","II","III","IV","V","VI","VII","VIII","IX","X","XI"};
                label = roman[i];
            } else {
                label = (i+1) == 12 ? "12" : String.valueOf(i+1);
            }
            int tx = cx + (int)((radius - 40) * Math.cos(angle));
            int ty = cy + (int)((radius - 40) * Math.sin(angle));
            g2.setFont(new Font("Arial", Font.BOLD, 16));
            g2.setColor(Color.BLACK);
            g2.drawString(label, tx - 8, ty + 6);
        }

        // Стрелки
        drawHand(g2, Math.toRadians((hours + minutes/60.0) * 30 - 90), radius*0.5, handColor, 6);
        drawHand(g2, Math.toRadians((minutes + seconds/60.0) * 6 - 90), radius*0.7, handColor, 4);
        drawHand(g2, Math.toRadians(seconds * 6 - 90), radius*0.8, secondColor, 2);

        // Центр
        g2.setColor(Color.BLACK);
        g2.fillOval(cx - 8, cy - 8, 16, 16);
    }

    private void drawHand(Graphics2D g, double angle, double length, Color color, int width) {
        int cx = center.x, cy = center.y;
        int x = cx + (int)(length * Math.cos(angle));
        int y = cy + (int)(length * Math.sin(angle));
        g.setColor(color);
        g.setStroke(new BasicStroke(width, BasicStroke.CAP_ROUND, BasicStroke.JOIN_ROUND));
        g.drawLine(cx, cy, x, y);
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        repaint();
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(AnalogClock::new);
    }
}
